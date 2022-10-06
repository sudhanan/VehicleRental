using System;
using System.Collections.Generic;

namespace VehicleRental.DAL.Data
{
    public partial class Country
    {
        public int Id { get; set; }
        public string? CountryName { get; set; }
        public string? Isocode { get; set; }
    }
}
