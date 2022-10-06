using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using VehicleRental.DAL.Data;
using VehicleRental.DAL.Models;

namespace VehicleRental.Business
{
    public class BookingLogic
    {
        public readonly masterContext _masterContext;
        public readonly IConfiguration _configuration;

        public BookingLogic(masterContext masterContext, IConfiguration configuration)
        {
            _masterContext = masterContext;
            _configuration = configuration;
        }

        public async Task<int> GetBookingStatusId(string status)
        {
            try
            {
                int bookingStatusId = await (from bs in _masterContext.BookingStatuses
                                             where bs.Status == status
                                             select bs.Id
                                       ).FirstOrDefaultAsync();

                return bookingStatusId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> IsVehicleAvailable(BookingInputs bookingInputs)
        {

            var vehicle = bookingInputs.SelectedVehicle;

            try
            {

                //Get booking overlaps (only confirmed) to check for availability
                var bookinOverlaps = (from b in _masterContext.Bookings
                                      join bs in _masterContext.BookingStatuses on b.BookingStatusId equals bs.Id
                                      where b.VehicleId == vehicle.ID
                                          && bs.Status == "Confirmed"
                                          && (bookingInputs.StartDate >= b.StartDate && bookingInputs.StartDate <= b.EndDate  ||
                                               bookingInputs.EndDate >= b.StartDate && bookingInputs.EndDate <= b.EndDate)
                                      select 1
                             ).Sum();

                //Get fleet quantity
                var fleetQuantity = (from v in _masterContext.Vehicles
                                     where v.Id == vehicle.ID
                                     select v.FleetQuantity).FirstOrDefault();

                //if fleetQuantity is more than the overlaps found then vehicle is available
                if (bookinOverlaps < fleetQuantity)
                    return true;
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> AddRenter(BookingInputs bookingInputs)
        {
            int renterIdIfAvailable = -1;

            try
            {
                var renter = new Renter
                {
                    FirstName = bookingInputs.FirstName,
                    LastName = bookingInputs.LastName,
                    EmailId = bookingInputs.Email
                };

                renterIdIfAvailable = (from r in _masterContext.Renters
                                       where r.FirstName == renter.FirstName
                                                  && r.LastName == renter.LastName
                                                  && r.EmailId == renter.EmailId
                                       select r.Id).FirstOrDefault();

                if (renterIdIfAvailable <= 0)
                {
                    _masterContext.Add<Renter>(renter);
                    await _masterContext.SaveChangesAsync();

                    return renter.Id;
                }
                return renterIdIfAvailable;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> AddBooking(int renterId, int bookingStatusId, VehicleDetails vehicle, DateTime startDate, DateTime endDate)
        {
            try
            {
                var booking = new Bookings
                {
                    VehicleId = vehicle.ID,
                    RenterId = renterId,
                    StartDate = startDate,
                    EndDate = endDate,
                    TotalCose = vehicle.TotalCost,
                    BookingStatusId = bookingStatusId
                };

                _masterContext.Add<Bookings>(booking);
                await _masterContext.SaveChangesAsync();

                return booking.Id;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<BookingResults> ReserveVehicle(BookingInputs bookingInputs)
        {
            int bookingId = -1;
            var reservedBooking = new BookingResults();
            var vehicle = bookingInputs.SelectedVehicle;

            try
            {

                //check if vehicle is available for selected dates
                bool isAvailable = await IsVehicleAvailable(bookingInputs);

                if (isAvailable)
                {

                    //Get booking stauts Id
                    int bookingStatusId = await GetBookingStatusId("Reserved");

                    //Add Renter details, if not available
                    int renterId = await AddRenter(bookingInputs);

                    //create a booking record with reserve status
                    bookingId = await AddBooking(renterId, bookingStatusId, vehicle, bookingInputs.StartDate, bookingInputs.EndDate);


                    reservedBooking = (from b in _masterContext.Bookings
                                       join ve in _masterContext.Vehicles on b.VehicleId equals ve.Id
                                       join r in _masterContext.Renters on b.RenterId equals r.Id
                                       join bs in _masterContext.BookingStatuses on b.BookingStatusId equals bs.Id
                                       where b.Id == bookingId
                                       select new BookingResults
                                       {

                                           BookingRefId = b.Id,
                                           bookingInputs = bookingInputs,
                                           BookingStatus = bs.Status
                                       }).FirstOrDefault();

                }

                return reservedBooking;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> ConfirmBooking(BookingResults bookingResults)
        {
            bool isSuccess = false;
            try
            {
                bool isAvailable = await IsVehicleAvailable(bookingResults.bookingInputs);

                if (isAvailable)
                {

                    var booking = await (from b in _masterContext.Bookings
                                         join bs in _masterContext.BookingStatuses on b.BookingStatusId equals bs.Id
                                         where b.Id == bookingResults.BookingRefId && bs.Status == "Reserved"
                                         select b)
                                            .FirstOrDefaultAsync();

                    if (booking != null)
                    {
                        booking.BookingStatusId = await GetBookingStatusId("Confirmed");

                        _masterContext.Update<Bookings>(booking);
                        await _masterContext.SaveChangesAsync();

                        isSuccess = true;
                    }
                }

                return isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
