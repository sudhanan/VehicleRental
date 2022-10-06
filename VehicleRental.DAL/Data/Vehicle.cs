using System;
using System.Collections.Generic;

namespace VehicleRental.DAL.Data
{
    public partial class Vehicle
    {
        public int Id { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public int? NumberPassengerSeats { get; set; }
        public int? VehicleTypeId { get; set; }
        public double? StandardPerDayRate { get; set; }
        public double? PeakPerDayRate { get; set; }
        public int? FleetQuantity { get; set; }

        public virtual VehicleType? VehicleType { get; set; }
    }
}
