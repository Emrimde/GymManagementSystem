import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ClassBookingAddRequest } from '../dto/ClassBooking/class-booking-add-request';

@Injectable({
  providedIn: 'root',
})
export class ClassBookingService {
  private readonly base = 'http://localhost:5105/api/classBooking';
  constructor(private httpClient: HttpClient) {}
  getClientClasses() {
    return this.httpClient.get(`${this.base}/getAll`);
  }
  createClassBooking(classBookingData: ClassBookingAddRequest) {
    return this.httpClient.post(`${this.base}`, classBookingData);
  }
  deleteClassBooking(classBookingId: string) {
    return this.httpClient.delete(`${this.base}/${classBookingId}`);
  }
}
