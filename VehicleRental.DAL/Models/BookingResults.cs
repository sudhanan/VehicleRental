
namespace VehicleRental.DAL.Models
{
    public class BookingResults
    {
        public int BookingRefId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? TotalCost { get; set; }
        public string BookingStatus { get; set; }

    }
}
