import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { SearchComponent } from './search/search.component';
import { SearchService } from './services/search.service';
import { BookingComponent } from './booking/booking.component';
import { BookingService } from './services/booking.service';
import { BookingConfirmPopup } from './booking-confirm-popup/booking-confirm-popup.component';
import { MatTableModule } from '@angular/material/table';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    SearchComponent,
    BookingComponent,
    BookingConfirmPopup
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    MatDialogModule,
    MatTableModule,
    RouterModule.forRoot([
      { path: 'search', component: SearchComponent },
    ])
  ],
  providers: [
    SearchService,
    BookingService,
    //{
    //  provide: MatDialogRef,
    //  useValue: []
    //},
    //{
    //  provide: 'MAT_DIALOG_DATA',
    //  useValue: []
    //}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
