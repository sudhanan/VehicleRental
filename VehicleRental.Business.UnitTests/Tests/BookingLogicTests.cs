using Microsoft.Extensions.Configuration;
using VehicleRental.Business.UnitTests.Infrastructure;
using VehicleRental.DAL.Data;
using VehicleRental.DAL.Models;

namespace VehicleRental.Business.UnitTests.Tests
{
    public class BookingLogicTests : TestBase
    {
        private readonly IConfigurationRoot configuration;

        public BookingLogicTests()
        {
            configuration = new ConfigurationBuilder()
                .AddJsonFile(@"C:\Users\nmadh\source\repos\NewSolution\VehicleRental\VehicleRental.Web\appsettings.json")
                .Build();
        }

        [Fact]
        public async Task IsVehicleAvailable_ReturnsTrueIfVehicleAvailable()
        {
            var bookingLogic = new BookingLogic(_masterContext, configuration);
            var bookingInputs = new BookingInputs
            {
                FirstName = "Jhon",
                LastName = "Smith",
                Email = "Jhon.Smith@email.com",
                RentalCountryCode = "Italy",
                StartDate = new DateTime(2022, 09, 29),
                EndDate = new DateTime(2022, 09, 29),
                VehicleType = "",
                NumberPassengerSeats = 0,
                SelectedVehicle = new VehicleDetails {
                    ID = 11,
                    Make = "MG",
                    Model = "ZS Auto",
                    VehicleType = "SUV",
                    FleetQuantity = 1,
                    TotalCost = 245.72
                }
            };

            var result = await bookingLogic.IsVehicleAvailable(bookingInputs);

            Assert.True(result);
        }


        [Fact]
        public async Task IsVehicleAvailable_ReturnsFalseIfVehicleUnAvailable()
        {
            var bookingLogic = new BookingLogic(_masterContext, configuration);
            var bookingInputs = new BookingInputs
            {
                FirstName = "Jhon",
                LastName = "Smith",
                Email = "Jhon.Smith@email.com",
                RentalCountryCode = "Italy",
                StartDate = new DateTime(2022, 09, 29),
                EndDate = new DateTime(2022, 09, 29),
                VehicleType = "",
                NumberPassengerSeats = 0,
                SelectedVehicle = new VehicleDetails
                {
                    ID = 1,
                    Make = "Fiat",
                    Model = "500",
                    VehicleType = "Hatchback",
                    FleetQuantity = 5,
                    TotalCost = 130.88
                }
            };

            var result = await bookingLogic.IsVehicleAvailable(bookingInputs);

            Assert.False(result);
        }

        [Fact]
        public async Task GetBookingStatusId_ReturnsBookingStatusId()
        {
            var bookingLogic = new BookingLogic(_masterContext, configuration);

            var result = await bookingLogic.GetBookingStatusId("Reserved");

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task AddRenter_ReturnsRenterId()
        {
            var bookingLogic = new BookingLogic(_masterContext, configuration);
            var bookingInputs = new BookingInputs
            {
                FirstName = "Jhon",
                LastName = "Smith",
                Email = "Jhon.Smith@email.com",
                RentalCountryCode = "Italy",
                StartDate = new DateTime(2022, 09, 29),
                EndDate = new DateTime(2022, 09, 29),
                VehicleType = "",
                NumberPassengerSeats = 0,
                SelectedVehicle = new VehicleDetails
                {
                    ID = 1,
                    Make = "Fiat",
                    Model = "500",
                    VehicleType = "Hatchback",
                    FleetQuantity = 5,
                    TotalCost = 130.88
                }
            };

            var result = await bookingLogic.AddRenter(bookingInputs);

            Assert.Equal(1, result);
        }


        [Fact]
        public async Task AddBooking_ReturnsBookingReferenceId_GivenBookingInputs()
        {
            var bookingLogic = new BookingLogic(_masterContext, configuration);
            DateTime startDate = new DateTime(2022,10,06);
            DateTime endDate = new DateTime(2022,10,07);
            int renterId = 1;
            int bookingStatusId = 1;

            VehicleDetails vehicle = new VehicleDetails
            {
                ID = 4,
                Make = "Mercedes-Benz",
                Model = "E300 Cabriolet",
                VehicleType = "Convertible",
                FleetQuantity = 2,
                TotalCost = 540.44
            };

            int expected = _masterContext.Bookings.Select(x => x.Id) 
                .OrderByDescending(x=> x)
                .First();

            expected += 1;

            var result = await bookingLogic.AddBooking(renterId, bookingStatusId, vehicle, startDate, endDate);

            Assert.Equal(expected , result);
        }

        [Fact]
        public async Task ReserveVehicle_ReturnsTrue_GivenVehicleSavedAsReserved()
        {
            var bookingLogic = new BookingLogic(_masterContext, configuration);

            DateTime startDate = new DateTime(2022, 11, 06);
            DateTime endDate = new DateTime(2022, 11, 10);
            int renterId = 1;
            int bookingStatusId = 1;
            VehicleDetails vehicle = new VehicleDetails
            {
                ID = 7,
                Make = "Citroen",
                Model = "Grand Picasso",
                VehicleType = "Minivan",
                FleetQuantity = 2,
                TotalCost = 345.17
            };

            var bookingId = await bookingLogic.AddBooking(renterId, bookingStatusId, vehicle, startDate, endDate);

            var result = _masterContext.Bookings.Where(x => x.Id == bookingId)
                                                .Select(x => x.BookingStatusId)
                                                .First();

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task ConfirmBooking_ReturnsTrue_GivenValidBookingRefId()
        {
            var bookingLogic = new BookingLogic(_masterContext, configuration);
            DateTime startDate = new DateTime(2022, 11, 06);
            DateTime endDate = new DateTime(2022, 11, 10);
            int renterId = 1;
            int bookingStatusId = 1;
            VehicleDetails vehicle = new VehicleDetails
            {
                ID = 7,
                Make = "Citroen",
                Model = "Grand Picasso",
                VehicleType = "Minivan",
                FleetQuantity = 2,
                TotalCost = 345.17
            };

            var bookingId = await bookingLogic.AddBooking(renterId, bookingStatusId, vehicle, startDate, endDate);
            
            var result = await bookingLogic.ConfirmBooking(bookingId);

            Assert.True(result);

        }


        [Fact]
        public async Task ConfirmBooking_ReturnsFalse_GivenInValidBookingRefId()
        {
            var bookingLogic = new BookingLogic(_masterContext, configuration);


            var result = await bookingLogic.ConfirmBooking(-1);

            Assert.False(result);

        }

        public void Dispose()
        {

            base.Dispose();
        }
    }
}
