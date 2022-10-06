using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using VehicleRental.Business;
using VehicleRental.DAL.Data;
using VehicleRental.DAL.Models;

namespace VehicleRental.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly masterContext _masterContext;
        private readonly IConfiguration _configuration;
        private readonly BookingLogic _bookingLogic;

        public BookingController(masterContext context, IConfiguration configuration) { 
            _masterContext = context;
            _configuration = configuration;
            _bookingLogic = new BookingLogic(_masterContext, _configuration);
        }

        [Route("ReserveVehicle")]
        [HttpPost]
        public async Task<BookingResults> ReserveVehicle(BookingInputs bookingInputs)
        {
            var reservedBooking = new BookingResults();
            try
            {
                reservedBooking = await _bookingLogic.ReserveVehicle(bookingInputs);
                return reservedBooking;
            }
            catch
            {
                return reservedBooking;
            }
        }


        [Route("ConfirmBooking")]
        [HttpPost]
        public async Task<bool> ConfirmBooking(BookingResults bookingResults)
        {
            try
            {
                bool result = await _bookingLogic.ConfirmBooking(bookingResults);
                return result;
            }
            catch
            {
                return false;
            }
        }
    }
}
