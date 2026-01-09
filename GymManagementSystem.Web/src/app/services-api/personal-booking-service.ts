import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class PersonalBookingService {
  getPersonalBookingsForClient() {
    return this.httpClient.get("http://localhost:5105/api/PersonalBooking");
  }
  constructor(private httpClient: HttpClient) {}
  private readonly base = "http://localhost:5105/api/PersonalBooking"

}
