using Microsoft.Extensions.Configuration;
using System.Security.AccessControl;
using VehicleRental.Business.UnitTests.Infrastructure;
using VehicleRental.DAL.Data;
using VehicleRental.DAL.Models;

namespace VehicleRental.Business.UnitTests.Tests
{
    public class SearchLogictTests : TestBase
    {
        private readonly IConfigurationRoot configuration;

        public SearchLogictTests()
        {
            configuration = new ConfigurationBuilder()
                .AddJsonFile(@"C:\Users\nmadh\source\repos\NewSolution\VehicleRental\VehicleRental.Web\appsettings.json")
                .Build();
        }

        [Fact]
        public async Task GetCountries_ReturnsAllCountries()
        {
            var searchLogic = new SearchLogic(_masterContext, configuration);

            var result = await searchLogic.GetCountries();

            Assert.Equal(8, result.Count);
        }

        [Fact]
        public async Task GetVehicleTypes_ReturnsAllVehicleTypes()
        {
            var searchLogic = new SearchLogic(_masterContext, configuration);

            var result = await searchLogic.GetVehiclestype();

            Assert.Equal(5, result.Count);
        }

        [Fact]
        public async Task GetVehicles_ReturnsAllVehicles()
        {
            var searchLogic = new SearchLogic(_masterContext, configuration);
            var bookingInputs = new BookingInputs
            {
                FirstName = "Jhon",
                LastName = "Smith",
                Email = "Jhon.Smith@email.com",
                RentalCountryCode = "Italy",
                StartDate = new DateTime(2022, 08, 01),
                EndDate = new DateTime(2022, 08, 01),
                VehicleType = "",
                NumberPassengerSeats = 0
            };

            var result = await searchLogic.GetVehicles(bookingInputs);

            Assert.Equal(11, result.VehicleDetails.Count);
        }


        [Fact]
        public async Task GetVehicles_ReturnsVehicles_GivenVehicleType()
        {
            var searchLogic = new SearchLogic(_masterContext, configuration);
            var bookingInputs = new BookingInputs
            {
                FirstName = "Jhon",
                LastName = "Smith",
                Email = "Jhon.Smith@email.com",
                RentalCountryCode = "UK",
                StartDate = new DateTime(2022, 08, 01),
                EndDate = new DateTime(2022, 08, 01),
                VehicleType = "Hatchback",
                NumberPassengerSeats = 0
            };

            var result = await searchLogic.GetVehicles(bookingInputs);

            Assert.Equal(4, result.VehicleDetails.Count);
        }

        [Fact]
        public async Task GetVehicles_ReturnsVehicles_GivenVehicleTypeNumberPassengerSeats()
        {
            var searchLogic = new SearchLogic(_masterContext, configuration);
            var bookingInputs = new BookingInputs
            {
                FirstName = "Jhon",
                LastName = "Smith",
                Email = "Jhon.Smith@email.com",
                RentalCountryCode = "Italy",
                StartDate = new DateTime(2022, 08, 01),
                EndDate = new DateTime(2022, 08, 01),
                VehicleType = "Minivan",
                NumberPassengerSeats = 2
            };

            var result = await searchLogic.GetVehicles(bookingInputs);

            Assert.Equal(2, result.VehicleDetails.Count);
        }

        [Fact]
        public async Task GetHolidays_GivenCountryUK()
        {
            var searchLogic = new SearchLogic(_masterContext, configuration);

            List<DateTime> dates = new List<DateTime> {
                new DateTime(2022,12,25),
                new DateTime(2022,12,26),
                new DateTime(2022,12,27),
                new DateTime(2022,12,27),
                new DateTime(2022,12,28),
                new DateTime(2022,12,29),
                new DateTime(2022,12,30),
                new DateTime(2022,12,31),
                new DateTime(2023,01,01),
                new DateTime(2023,01,02)
            };

            var result = await searchLogic.GetHolidays("GB", dates);

            Assert.Equal(5, result.Count);
        }

        [Fact]
        public async Task GetVehicles_ReturnsTotalCost_GivenOnlyWorkingDays()
        {
            var searchLogic = new SearchLogic(_masterContext, configuration);
            var bookingInputs = new BookingInputs
            {
                FirstName = "Jhon",
                LastName = "Smith",
                Email = "Jhon.Smith@email.com",
                RentalCountryCode = "Italy",
                StartDate = new DateTime(2023, 08, 01),
                EndDate = new DateTime(2023, 08, 10),
                VehicleType = "Hatchback",
                NumberPassengerSeats = 2
            };

            var result = await searchLogic.GetVehicles(bookingInputs);

            Assert.Equal(1308.8, result.VehicleDetails.Where(x => x.Make == "Fiat" && x.Model == "500")
                                        .Take(1)
                                        .First()
                                        .TotalCost);
        }



        [Fact]
        public async Task GetVehicles_ReturnsTotalCost_GivenOnlyHolidays()
        {
            var searchLogic = new SearchLogic(_masterContext, configuration);
            var bookingInputs = new BookingInputs
            {
                FirstName = "Jhon",
                LastName = "Smith",
                Email = "Jhon.Smith@email.com",
                RentalCountryCode = "GB",
                StartDate = new DateTime(2022, 12, 25),
                EndDate = new DateTime(2022, 12, 26),
                VehicleType = "Hatchback",
                NumberPassengerSeats = 2
            };

            var result = await searchLogic.GetVehicles(bookingInputs);

            Assert.Equal(291.4, result.VehicleDetails.Where(x => x.Make == "Fiat" && x.Model == "500")
                                        .Take(1)
                                        .First()
                                        .TotalCost);
        }

        [Fact]
        public async Task GetVehicles_ReturnsTotalCost_GivenBothWorkingDaysAndHolidays()
        {
            var searchLogic = new SearchLogic(_masterContext, configuration);
            var bookingInputs = new BookingInputs
            {
                FirstName = "Jhon",
                LastName = "Smith",
                Email = "Jhon.Smith@email.com",
                RentalCountryCode = "GB",
                StartDate = new DateTime(2022, 12, 23),
                EndDate = new DateTime(2022, 12, 26),
                VehicleType = "Hatchback",
                NumberPassengerSeats = 2
            };

            var result = await searchLogic.GetVehicles(bookingInputs);

            Assert.Equal(553.16, result.VehicleDetails.Where(x => x.Make == "Fiat" && x.Model == "500")
                                        .Take(1)
                                        .First()
                                        .TotalCost);
        }

        public void Dispose() {

            base.Dispose();
        }
    }
}