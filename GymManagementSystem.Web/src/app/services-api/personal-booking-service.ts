import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PersonalBookingAddRequest } from '../dto/PersonalBooking/personal-booking-add-request';

@Injectable({
  providedIn: 'root',
})
export class PersonalBookingService {
  constructor(private httpClient: HttpClient) {}
  private readonly base = "http://localhost:5105/api/PersonalBooking"
  
  getPersonalBookingsForClient() {
    return this.httpClient.get(`${this.base}`);
  }

  createPersonalBooking(request: PersonalBookingAddRequest) {
    return this.httpClient.post(`${this.base}`, request);
  }
}
