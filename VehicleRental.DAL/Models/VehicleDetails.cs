using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRental.DAL.Models
{
    public class VehicleDetails
    {
        public int ID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string VehicleType { get; set; }
        public int FleetQuantity { get; set; }
        public double TotalCost { get; set; }
    }
}
