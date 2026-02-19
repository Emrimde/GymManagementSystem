import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ClassBookingAddRequest } from '../dto/ClassBooking/class-booking-add-request';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ClassBookingService {
  private readonly base = `${environment.apiUrl}/classBooking`
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
