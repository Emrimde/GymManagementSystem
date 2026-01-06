import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ClientAddRequest } from '../dto/Client/client-add-request';

@Injectable({
  providedIn: 'root',
})
export class ClientService {
  constructor(private httpClient: HttpClient){}
  private readonly base = "http://localhost:5105/api/Client"
  
  getClientDetails() {
    return this.httpClient.get(`${this.base}/get-client-details`)
  }

  createClient(dto: ClientAddRequest){
    return this.httpClient.post(`${this.base}/create-account`, dto)
  }
}
