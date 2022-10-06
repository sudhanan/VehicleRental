
namespace VehicleRental.DAL.Models
{
    public class BookingResults
    {
        public int BookingRefId { get; set; }

        public string BookingStatus { get; set; }

        public BookingInputs bookingInputs { get; set; }

    }
}
