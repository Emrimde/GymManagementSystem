import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ScheduledClassService {
  constructor(private httpClient: HttpClient) {}
  private readonly base = "http://localhost:5105/api/scheduledclass";
  getScheduledClasses(gymClassId : string){
    return this.httpClient.get(`${this.base}/scheduledclasses/${gymClassId}`);
  }
}
