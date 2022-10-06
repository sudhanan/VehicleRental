import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BookingInputs } from '../models/BookingInputs';
import { Countries } from '../models/Countries';
import { VehicleSearchResults, VehiclesType } from '../models/Vehicles';
import { SearchService } from '../services/search.service';

function calculateDateDiff(endDate: Date, startDate: Date) {
  var Date2: any = new Date(endDate);
  var Date1: any = new Date(startDate);
  var diff = Math.floor(Date2 - Date1) / (1000 * 60 * 60 * 24);
  return diff as number;
}

@Component({
  selector: 'search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent {

  //Declarations
  baseUrl: string = '';
  countries: Countries[] = [];
  vehicleTypes: VehiclesType[] = [];
  bookingInputs: BookingInputs = new BookingInputs();
  vehicles: any;
  isVehiclesEmpty: boolean = true;
  isBookingConfirmed: boolean = false;

  public vehicleSearchForm: FormGroup;
  public get firstName() { return this.vehicleSearchForm.get('firstName'); }
  public get lastName() { return this.vehicleSearchForm.get('lastName'); }
  public get email() { return this.vehicleSearchForm.get('email'); }
  public get startDate() { return this.vehicleSearchForm.get('startDate'); }
  public get endDate() { return this.vehicleSearchForm.get('endDate'); }
  public get rentalCountry() { return this.vehicleSearchForm.get('rentalCountry'); }
  public get vehicleCategory() { return this.vehicleSearchForm.get('vehicleCategory'); }
  public get numberPassengerSeats() { return this.vehicleSearchForm.get('numberPassengerSeats'); }

  //Validators
  dateValidator(control: FormGroup) {
    const _startDate: Date = control.get('startDate').value;
    const _endDate: Date = control.get('endDate').value;
    if (_startDate !== null && _endDate !== null) {
      if (_startDate > _endDate)
        return { invalidDate: true };
      let diff = calculateDateDiff(_endDate, _startDate);
      if (_startDate < _endDate && diff >= 21)
        return { invalidSearchRange: true };
    }
    return null;
  }


  //Class methods
  constructor(
    @Inject('BASE_URL') baseUrl: string,
    private http: HttpClient,
    private formBuilder: FormBuilder,
    private searchService: SearchService) {
    this.baseUrl = baseUrl;
    this.BuildForm(formBuilder);
  }

  ngOnInit() {

    this.GetVehiclesType();
    this.GetCountries();
  }

  BuildForm(formBuilder: FormBuilder) {
    this.vehicleSearchForm = formBuilder.group({
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      startDate: new FormControl('', [Validators.required]),
      endDate: new FormControl('', [Validators.required]),
      rentalCountry: new FormControl('', Validators.required),
      vehicleCategory: new FormControl(''),
      numberPassengerSeats: new FormControl(0, Validators.min(0)),

    }, { validators: this.dateValidator });
  }


  public BookingInputs() {
    
    this.bookingInputs.firstName = this.firstName.value;
    this.bookingInputs.lastName = this.lastName.value;
    this.bookingInputs.email = this.email.value;
    this.bookingInputs.startDate = this.startDate.value;
    this.bookingInputs.endDate = this.endDate.value;
    this.bookingInputs.rentalCountryCode = this.rentalCountry.value == undefined ? this.countries[0].isocode : this.countries.find(x => x.countryName = this.rentalCountry.value).isocode;
    this.bookingInputs.vehicleType = this.vehicleCategory.value == undefined ? '' : this.vehicleCategory.value;
    this.bookingInputs.numberPassengerSeats = this.numberPassengerSeats.value;
    
    return this.bookingInputs;
  }


  //Service calls
  public GetVehicles() {

    let bookingInputs = this.BookingInputs();

    this.searchService.GetVehicles(bookingInputs).subscribe(result => {
      this.vehicles = result.vehicleDetails;
      if (this.vehicles.length > 0)
        this.isVehiclesEmpty = false;
    })
  }

  public GetVehiclesType() {
    this.searchService.GetVehiclesType().subscribe(result => {
      this.vehicleTypes = result;
      this.vehicleCategory.setValue('');
    })
  }

  public GetCountries() {
    this.searchService.GetCountries().subscribe(result => {
      this.countries = result;
      this.rentalCountry.setValue(this.countries[0].countryName);
    })
  }


  //Event methods
  public Search() {
    this.GetVehicles();
  }

  public Reset() {

    //hide serach results
    this.isVehiclesEmpty = true;
    this.isBookingConfirmed = false;

    //reset form
    this.vehicleSearchForm.reset();
    //setdefualts 
    this.numberPassengerSeats.setValue(0);
    this.rentalCountry.setValue(this.countries[0].countryName);
  }

  onConfirmed(data) {
    this.isBookingConfirmed = data;
  }

}
