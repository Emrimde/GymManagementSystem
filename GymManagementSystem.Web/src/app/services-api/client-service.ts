import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ClientAddRequest } from '../dto/Client/client-add-request';
import { ClientUpdateRequest } from '../dto/Client/client-update-request';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ClientService {
  constructor(private httpClient: HttpClient){}
  private readonly base = `${environment.apiUrl}/client/client`
  updateClient(dto: ClientUpdateRequest) {
    return this.httpClient.put(`${this.base}/update-client`, dto)
  }
  getClientDetails() {
    return this.httpClient.get(`${this.base}/get-client-details`)
  }

  createClient(dto: ClientAddRequest){
    return this.httpClient.post(`${this.base}/create-account`, dto)
  }

  getClientContext(){
    return this.httpClient.get(`${this.base}/get-client-context`)
  }
}
