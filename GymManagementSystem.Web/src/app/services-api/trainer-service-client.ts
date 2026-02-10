import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TrainerTimeOffAdd } from '../components/Trainer/PersonalTrainer/trainer-time-off-add/trainer-time-off-add';
import { TrainerTimeOffAddRequest } from '../dto/TrainerTImeOff/trainer-time-off-add-request';

@Injectable({
  providedIn: 'root',
})
export class TrainerServiceClient {
  private readonly base = "http://localhost:5105/api/client/trainer"
  constructor(private httpClient: HttpClient) {}
  getGroupInstructorPanelInfo() {
    return this.httpClient.get(`${this.base}/get-group-instructor-panel`);
  }
  getMyGymClasses() {
    return this.httpClient.get(`${this.base}/gym-classes`);
  }
  getTrainerPanelInfo() {
    return this.httpClient.get(`${this.base}/get-panel-info`);
  }
  getTrainerRatesById(id: string) {
    return this.httpClient.get(`${this.base}/trainer-rates-select/${id}`);
  }
  createTrainerTimeOff(request: TrainerTimeOffAddRequest) {
    return this.httpClient.post(`${this.base}/create-trainer-time-off`, request);
  }
}
