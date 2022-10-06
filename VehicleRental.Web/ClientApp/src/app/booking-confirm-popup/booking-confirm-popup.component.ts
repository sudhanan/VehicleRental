import { Component, Input,Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BookingResults } from '../models/BookingResults';

@Component({
  selector: 'booking-confirm-popup-component',
  templateUrl: 'booking-confirm-popup.component.html',

})
export class BookingConfirmPopup {

  constructor(
    public dialogRef: MatDialogRef<BookingConfirmPopup>,
    @Inject(MAT_DIALOG_DATA) public data: BookingResults,
  ) {
    console.log(data);
  }

  Cancel() {
    this.dialogRef.close(new BookingResults());
  }

}
