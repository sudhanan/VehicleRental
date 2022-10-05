using System;
using System.Collections.Generic;

namespace VehicleRental.DAL.Data
{
    public partial class Renter
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailId { get; set; }
    }
}
