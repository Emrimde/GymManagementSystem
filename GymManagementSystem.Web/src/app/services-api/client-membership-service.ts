import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ClientMembershipService {
  private readonly base = "http://localhost:5105/api/clientMemberships"

  getClientMembershipInfo() {
    return this.httpClient.get(`${this.base}/get-client-membership-info`)
  }
 
  constructor(private httpClient:HttpClient){}
}
