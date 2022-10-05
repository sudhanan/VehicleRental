using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRental.DAL.Models;

namespace VehicleRental.Business.Helpers
{
    public static class SearchHelper
    {
        //public static async Task<bool> ValidateInputs(BookingInputs bookingInputs)
        //{
        //    try
        //    {
        //        bool isValid = !string.IsNullOrEmpty(bookingInputs.FirstName)
        //                    && !string.IsNullOrEmpty(bookingInputs.LastName)
        //                    && !string.IsNullOrEmpty(bookingInputs.FirstName)

        //        return true;
        //    }
        //    catch
        //    {

        //        return false;
        //    }
        //}

        public static List<DateTime> GetDates(DateTime startDate, DateTime endDate) {
            try
            {
                if (endDate < startDate) {
                    throw new InvalidDataException("StartDate must be before EndDate");
                }
                var dates = Enumerable.Range(0, endDate.Subtract(startDate).Days)
                                                .Select(offset => startDate.AddDays(offset))
                                                .ToList();
                if (dates.Count == 0)
                    dates.Add(startDate);
                else
                    dates.Add(endDate);

                return dates;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
