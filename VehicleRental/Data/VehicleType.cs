using System;
using System.Collections.Generic;

namespace VehicleRental.DAL.Data
{
    public partial class VehicleType
    {
        public VehicleType()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        public int Id { get; set; }
        public string? Type { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
