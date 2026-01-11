import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class GymClassService {
  constructor(private http: HttpClient) {}
  private readonly base = "https://localhost:5105/api/gymclass/";
  getGymClasses(){
    return this.http.get(`${this.base}/select-gymclasses`);
  }
}
