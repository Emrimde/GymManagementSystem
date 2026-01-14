import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ClientUpdateRequest } from '../dto/Client/client-update-request';
import { ClientMembershipAddRequest } from '../dto/ClientMembership/client-membership-add-request';

@Injectable({
  providedIn: 'root',
})
export class ClientMembershipService {
  createClientMembership(addDto: ClientMembershipAddRequest) {
    return this.httpClient.post(`${this.base}`, addDto)
  }
  private readonly base = "http://localhost:5105/api/client/client-membership"

  getClientMembershipInfo() {
    return this.httpClient.get(`${this.base}/get-client-membership-info`)
  }

  getClientMembershipPreview(membershipId : string){
    return this.httpClient.get(`${this.base}/get-client-membership-preview/${membershipId}`)
  }
 
  constructor(private httpClient:HttpClient){}
}
