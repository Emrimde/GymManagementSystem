import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ScheduledClassService {
  constructor(private httpClient: HttpClient) {}
  private readonly base = `${environment.apiUrl}/scheduledclass`
  getScheduledClasses(gymClassId : string){
    return this.httpClient.get(`${this.base}/scheduledclasses/${gymClassId}`);
  }
}
