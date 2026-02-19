import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class GymClassService {
  constructor(private http: HttpClient) {}
  private readonly base = `${environment.apiUrl}/gymclass`;
  getGymClasses(){
    return this.http.get(`${this.base}/select-gymclasses`);
  }
}
