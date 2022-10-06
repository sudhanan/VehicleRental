using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRental.DAL.Data;

namespace VehicleRental.DAL.Models
{
    public class SearchResults
    {
        public BookingInputs Inputs { get; set; }

        public List<VehicleDetails> VehicleDetails { get; set; }    
    }
}
