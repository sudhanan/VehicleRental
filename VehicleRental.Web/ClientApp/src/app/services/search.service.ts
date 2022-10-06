import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs/internal/Observable";
import { map } from 'rxjs/operators';
import { BookingInputs } from "../models/BookingInputs";
import { Countries } from "../models/Countries";

@Injectable()
export class SearchService {

  baseUrl: string = '';
  httpClient: any;

  constructor(
    @Inject('BASE_URL') baseUrl: string,
    private http: HttpClient) {

    this.baseUrl = baseUrl;
    this.httpClient = http;
  }

  public GetVehicles(bookingInputs: BookingInputs) {
    let url = this.baseUrl + 'Search/GetVehicles';

    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    let options = { header: headers };

    return this.httpClient.post(url, bookingInputs, options).pipe(
      map(result => {
        return result
      }));
  }


  public GetVehiclesType() {

    let url = this.baseUrl + 'Search/GetVehiclesType';
    return this.httpClient.get(url).pipe(map(result => {
      return result;
    }));

  }


  public GetCountries() {
    let url = this.baseUrl + 'Search/Getcountries';
    return this.httpClient.get(url).pipe(map((result: Countries[]) => {
      return result;
    }));
  }
}
