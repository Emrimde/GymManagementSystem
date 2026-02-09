import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TrainerServiceClient {
  private readonly base = "http://localhost:5105/api/client/trainer"
  constructor(private httpClient: HttpClient) {}
  getMyGymClasses() {
    return this.httpClient.get(`${this.base}/gym-classes`);
  }
  getTrainerPanelInfo() {
    return this.httpClient.get(`${this.base}/get-panel-info`);
  }
  getTrainerRatesById(id: string) {
    return this.httpClient.get(`${this.base}/trainer-rates-select/${id}`);
  }
}
