import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class TrainerService {
  private readonly base = `${environment.apiUrl}/Trainer`
  constructor(private httpClient: HttpClient) {}
  getAllPersonalTrainers() {
    return this.httpClient.get(`${this.base}/personal-trainers`);
  }

  getTrainerRatesById(id: string) {
    return this.httpClient.get(`${this.base}/trainer-rates-select/${id}`);
  }
}
