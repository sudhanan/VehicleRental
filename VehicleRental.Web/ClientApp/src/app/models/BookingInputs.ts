import { VehicleDetails } from "./Vehicles";

export class BookingInputs{
  firstName: string;
  lastName: string;
  email: string;
  vehicleType: string;
  rentalCountryCode: string;
  numberPassengerSeats: number;
  startDate: Date;
  endDate: Date;

  selectedVehicle: VehicleDetails;
}
