import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class MembershipService {
  private readonly base = `${environment.apiUrl}/membership`

  getAllMembershipsWithFeatures() {
    return this.httpClient.get(`${this.base}/get-all-memberships`)
  }

  constructor(private httpClient:HttpClient){}
}
