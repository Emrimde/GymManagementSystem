import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class MembershipService {
  private readonly base = "http://localhost:5105/api/membership"

  getAllMembershipsWithFeatures() {
    return this.httpClient.get(`${this.base}/get-all-memberships`)
  }

  constructor(private httpClient:HttpClient){}
}
