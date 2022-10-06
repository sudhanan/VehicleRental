import { BookingInputs } from "./BookingInputs";

export class SearchResults{
  bookingInputs: BookingInputs;
  vehicleDetails: VehicleDetails[];
 }

export class VehiclesType {
  id: number;
  type: string;
}


export class VehicleDetails {

  id: number;
  make: string;
  model: string;
  vehicleType: string;
  fleetQuantity: string;
  totalCost: number;
}
