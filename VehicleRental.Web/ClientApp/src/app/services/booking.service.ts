import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { map } from 'rxjs/operators';
import { BookingInputs } from "../models/BookingInputs";
import { BookingResults } from "../models/BookingResults";

@Injectable()
export class BookingService {

  baseUrl: string = '';
  httpClient: any;

  constructor(
    @Inject('BASE_URL') baseUrl: string,
    private http: HttpClient) {

    this.baseUrl = baseUrl;
    this.httpClient = http;
  }

  public ReserveVehicle(bookingInputs: BookingInputs) {
    let url = this.baseUrl + 'Booking/ReserveVehicle';

    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    let options = { header: headers };

    return this.httpClient.post(url, bookingInputs, options).pipe(
      map(result => {
        return result
      }, error => console.error(error)));
  }

  public ConfirmBooking(bookingRefId: number) {
    let url = this.baseUrl + 'Booking/ConfirmBooking/' + bookingRefId;

    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    let options = { header: headers };

    return this.httpClient.post(url, options).pipe(
      map(result => {
        return result;
      }, error => console.error(error)));
  }
  
}
