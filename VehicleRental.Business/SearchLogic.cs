using Microsoft.EntityFrameworkCore;
using VehicleRental.DAL.Data;
using VehicleRental.DAL.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VehicleRental.Business.Helpers;


namespace VehicleRental.Business
{
    public class SearchLogic
    {
        private readonly masterContext _masterContext;
        private readonly IConfiguration _configuration;

        public SearchLogic(masterContext masterContext, IConfiguration configuration)
        {
            _masterContext = masterContext;
            _configuration = configuration;
        }

        public async Task<List<Country>> GetCountries()
        {
            try
            {
                return await _masterContext.Countries.ToListAsync();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<string?>> GetVehiclestype()
        {
            try { 
            return await _masterContext.VehicleTypes.Select(x => x.Type).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DateTime>> GetHolidays(string country, List<DateTime> dates)
        {
            string calendarBaseUrl = _configuration["CalendarApi"];
            List<DateTime> holidays = new List<DateTime>();

            List<DateTime> distinctYearMonth = dates.Select(d => new DateTime(d.Year, d.Month, 1))
                                        .Distinct()
                                        .ToList();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    foreach (DateTime date in distinctYearMonth)
                    {
                        string url = calendarBaseUrl
                            + "&type=national"
                            + "&country=" + country
                            + "&year=" + date.Year
                            + "&month=" + date.Month;
                        var repsonse = await client.GetAsync(url);
                        var responseJson = JsonConvert.DeserializeObject<JObject>(await repsonse.Content.ReadAsStringAsync());

                        var holidayObj = responseJson["response"]["holidays"].ToList();
                        if (holidayObj.Count > 0)
                        {
                            foreach (JToken hday in holidayObj) {
                                holidays.Add((DateTime)hday["date"]["iso"]);
                            }
                        }
                    }
                }

                return holidays.Where(x => dates.Contains(x))
                                .Distinct()
                                .ToList();
            }

            catch (Exception ex) {
                throw ex;
            }

        }

        public async Task<SearchResults> GetVehicles(BookingInputs bookingInputs)
        {
            List<DateTime> dates = new List<DateTime>();
            List<VehicleDetails> vehicleDetails = new List<VehicleDetails>();
            try
            {
                var type = bookingInputs.VehicleType;
                var numberPassengerSeats = bookingInputs.NumberPassengerSeats;

                dates = SearchHelper.GetDates(bookingInputs.StartDate, bookingInputs.EndDate);

                var holidays = await GetHolidays(bookingInputs.RentalCountryCode, dates);
                dates.RemoveAll(x => holidays.Contains(x));

                int standardDayCount = dates.Count();
                int holidayDayCount = holidays.Count;

                vehicleDetails = (from v in _masterContext.Vehicles
                            join vt in _masterContext.VehicleTypes
                            on v.VehicleTypeId equals vt.Id
                            where v.NumberPassengerSeats >= Convert.ToInt32(numberPassengerSeats) && (type == "" ? vt.Type : type) == vt.Type
                            let TotalCost = Math.Round(v.StandardPerDayRate.Value * standardDayCount + (v.PeakPerDayRate.Value * holidayDayCount), 2)
                            select new VehicleDetails
                            {
                                ID = v.Id,
                                Make = v.Make,
                                Model = v.Model,
                                VehicleType = vt.Type,
                                FleetQuantity = v.FleetQuantity ?? 0,
                                TotalCost = TotalCost,
                            }).ToList();

                return new SearchResults { 
                    Inputs = bookingInputs,
                    VehicleDetails = vehicleDetails,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
    }

}
