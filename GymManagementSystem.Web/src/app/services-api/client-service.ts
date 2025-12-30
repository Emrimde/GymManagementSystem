import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ClientService {
  constructor(private httpClient: HttpClient){}
  private readonly base = "http://localhost:5105/api/Client"
  
  getClientDetails() {
    return this.httpClient.get(`${this.base}/get-client-details`)
  }
}
