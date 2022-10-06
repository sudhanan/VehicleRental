namespace VehicleRental.DAL.Models
{
    public class BookingInputs
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? VehicleType { get; set; }
        public string? RentalCountryCode { get; set; }
        public int NumberPassengerSeats { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public VehicleDetails? SelectedVehicle { get; set; }
    }
}
