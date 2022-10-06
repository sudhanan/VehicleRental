using System;
using System.Collections.Generic;

namespace VehicleRental.DAL.Data
{
    public partial class Bookings
    {
        public int Id { get; set; }
        public int? VehicleId { get; set; }
        public int? RenterId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? TotalCose { get; set; }
        public int? BookingStatusId { get; set; }

        public virtual BookingStatus? BookingStatus { get; set; }
        public virtual Renter? Renter { get; set; }
        public virtual Vehicle? Vehicle { get; set; }
    }
}
