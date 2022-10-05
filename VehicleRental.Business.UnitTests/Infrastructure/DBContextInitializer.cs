
using VehicleRental.DAL.Data;

namespace VehicleRental.Business.UnitTests.Infrastructure
{
    public class DBContextInitializer
    {
        public static void Intialize(masterContext context) {
            if (context.Vehicles.Any()) {
                return;
            }

            Seed(context);

        }

        public static void Seed(masterContext context)
        {
            SeedCountry(context);
            SeedVehicle(context);
            SeedBookings(context);  
        }

        public static void SeedVehicle(masterContext context) {

            var vehicleTypes = new[] {

                new VehicleType { Type = "Hatchback"  },
                new VehicleType { Type = "SVU"  },
                new VehicleType { Type = "4 -Wheel Drive"  },
                new VehicleType { Type = "Minivan"  },
                new VehicleType { Type = "Convertible"  },
            };

            context.VehicleTypes.AddRange(vehicleTypes);
            context.SaveChanges();

            var vehicles = new[] {

                new Vehicle { Make= "Fiat", Model = "500", NumberPassengerSeats= 3, VehicleTypeId = 1, StandardPerDayRate = 130.88, PeakPerDayRate = 145.7, FleetQuantity = 5 },
                new Vehicle { Make= "Vauxhall", Model = "Crossland", NumberPassengerSeats= 4, VehicleTypeId = 2, StandardPerDayRate = 165.3, PeakPerDayRate = 180.3, FleetQuantity = 2 },
                new Vehicle { Make= "Range Rover", Model = "Evoque", NumberPassengerSeats= 4, VehicleTypeId = 3, StandardPerDayRate = 250, PeakPerDayRate = 275.5, FleetQuantity = 2 },
                new Vehicle { Make= "Mercedes - Benz", Model = "E300 Cabriolet", NumberPassengerSeats= 3, VehicleTypeId = 5, StandardPerDayRate = 270.22, PeakPerDayRate = 300.65, FleetQuantity = 1 },
                new Vehicle { Make= "Mercedes - Benz", Model = "V220d Sport MPV", NumberPassengerSeats= 7, VehicleTypeId = 4, StandardPerDayRate = 362.3, PeakPerDayRate = 401.05, FleetQuantity = 2 },
                new Vehicle { Make= "Range Rover", Model = "Velar D300 R", NumberPassengerSeats= 4, VehicleTypeId = 3, StandardPerDayRate = 350.99, PeakPerDayRate = 380, FleetQuantity = 3 },
                new Vehicle { Make= "Citroen", Model = "Grand Picasso", NumberPassengerSeats= 6, VehicleTypeId = 4, StandardPerDayRate = 345.17, PeakPerDayRate = 355.5, FleetQuantity = 2 },
                new Vehicle { Make= "Volkswagen", Model = "Golf", NumberPassengerSeats= 4, VehicleTypeId = 1, StandardPerDayRate = 180.04, PeakPerDayRate = 200.12, FleetQuantity = 3 },
                new Vehicle { Make= "Mercedes - Benz", Model = "A Class", NumberPassengerSeats= 4, VehicleTypeId = 1, StandardPerDayRate = 270.31, PeakPerDayRate = 282.99, FleetQuantity = 3 },
                new Vehicle { Make= "Skoda,Octavia", Model = "Octavia", NumberPassengerSeats= 4, VehicleTypeId = 1, StandardPerDayRate = 272.42, PeakPerDayRate = 283.12, FleetQuantity = 2 },
                new Vehicle { Make= "MG", Model = "ZS Auto", NumberPassengerSeats= 4, VehicleTypeId = 2, StandardPerDayRate = 245.72, PeakPerDayRate = 250.81, FleetQuantity = 1 }
            };

            context.Vehicles.AddRange(vehicles);
            context.SaveChanges();

        }


        public static void SeedCountry(masterContext context) {

            var countries = new[] {

                new Country { CountryName = "UK", Isocode = "GB"},
                new Country { CountryName = "Ireland", Isocode = "IE"},
                new Country { CountryName = "US", Isocode = "US"},
                new Country { CountryName = "France", Isocode = "FR"},
                new Country { CountryName = "Germany", Isocode = "DE"},
                new Country { CountryName = "Canada", Isocode = "CA"},
                new Country { CountryName = "Sapin", Isocode = "ES"},
                new Country { CountryName = "Italy", Isocode = "IT"}

            };

            context.Countries.AddRange(countries);
            context.SaveChanges();
        }


        public static void SeedBookings(masterContext context)
        {

            var bookingStatuses = new[] {

                new BookingStatus { Status = "Reserved"},
                new BookingStatus { Status = "Confirmed"},

            };
            context.BookingStatuses.AddRange(bookingStatuses);
            context.SaveChanges();

            var bookings = new[] {

                new Bookings { VehicleId = 1, RenterId = 1,StartDate= new DateTime(2022,09,28), EndDate=new DateTime(2022,09,29),TotalCose=261.76,BookingStatusId = 2},
                new Bookings { VehicleId = 1, RenterId = 1,StartDate=new DateTime(2022,09,27), EndDate=new DateTime(2022,09,29),TotalCose=392.64,BookingStatusId = 2},
                new Bookings { VehicleId = 1, RenterId = 1,StartDate=new DateTime(2022,09,27), EndDate=new DateTime(2022,10,01),TotalCose=654.4,BookingStatusId = 2},
                new Bookings { VehicleId = 1, RenterId = 1,StartDate=new DateTime(2022,09,27), EndDate=new DateTime(2022,10,03),TotalCose=654.4,BookingStatusId = 2},
                new Bookings { VehicleId = 1, RenterId = 1,StartDate=new DateTime(2022,09,27), EndDate=new DateTime(2022,10,02),TotalCose=785.28,BookingStatusId = 2},
                new Bookings { VehicleId = 1, RenterId = 1,StartDate=new DateTime(2022,09,28), EndDate=new DateTime(2022,09,29),TotalCose=261.76,BookingStatusId = 2},
                new Bookings { VehicleId = 2, RenterId = 2,StartDate=new DateTime(2022,09,28), EndDate=new DateTime(2022,09,29),TotalCose=261.76,BookingStatusId = 1},

            };

            context.Bookings.AddRange(bookings);
            context.SaveChanges();
            
        }
    }
}
