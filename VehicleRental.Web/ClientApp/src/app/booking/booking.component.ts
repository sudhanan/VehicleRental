import { Component, EventEmitter, Inject, Input, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { BookingConfirmPopup } from '../booking-confirm-popup/booking-confirm-popup.component';
import { BookingInputs } from '../models/BookingInputs';
import { BookingResults } from '../models/BookingResults';
import { VehicleSearchResults} from '../models/Vehicles';
import { BookingService } from '../services/booking.service';


@Component({
  selector: 'booking',
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.css']
})
export class BookingComponent {

  @Input() vehicles: VehicleSearchResults[] = null;
  @Input() bookingInputs: BookingInputs = null;
  @Input() isVehiclesEmpty: boolean = true;

  @Output() bookingSuccessful = new EventEmitter<boolean>();

  baseUrl: string = '';
  bookingRefId: number = -1;
  reservedBooking: any = {};
  displayedColumns: string[] = [];

  constructor(
    @Inject('BASE_URL') baseUrl: string,
    private bookingService: BookingService,
    private dialog: MatDialog) {
    this.baseUrl = baseUrl;
  }

  public ReserveVehicle(vehicle: VehicleSearchResults) {

    //add selected vehicle to booking inputs
    this.bookingInputs.selectedVehicle = vehicle;

    this.bookingService.ReserveVehicle(this.bookingInputs).subscribe(result => {
      if (result) {
        this.openDialog(result);
      }
    });

  }

  openDialog(reservedBooking: BookingResults) {

    const dialogRef = this.dialog.open(BookingConfirmPopup, {
      width: '1200px',
      height: '300px',
      data: reservedBooking

    });

    dialogRef.afterClosed().subscribe(confirmedBookingId => {
      if (confirmedBookingId <= 0)
        return;
      this.bookingService.ConfirmBooking(confirmedBookingId).subscribe(result => {
        if (result) {
          this.isVehiclesEmpty = true;
          this.bookingSuccessful.emit(true);
        }
      });
    });
  }

}

