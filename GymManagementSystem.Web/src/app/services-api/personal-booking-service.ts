import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PersonalBookingAddRequest } from '../dto/PersonalBooking/personal-booking-add-request';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class PersonalBookingService {
  constructor(private httpClient: HttpClient) {}
  private readonly base = `${environment.apiUrl}/PersonalBooking`
  
  getPersonalBookingsForClient() {
    return this.httpClient.get(`${this.base}`);
  }

  createPersonalBooking(request: PersonalBookingAddRequest) {
    return this.httpClient.post(`${this.base}`, request);
  }
}
