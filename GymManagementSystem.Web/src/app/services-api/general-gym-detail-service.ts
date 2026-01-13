import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GeneralPublicProfileResponse } from '../dto/GeneralGymDetail/general-public-profile-response';
import { AboutUsResponse } from '../dto/GeneralGymDetail/about-us-response';

@Injectable({
  providedIn: 'root',
})
export class GeneralGymDetailService {
  constructor(private httpClient: HttpClient) {}
  private readonly base = 'http://localhost:5105/api/public/general-gym-detail';
  getPublicGymProfile(){
    return this.httpClient.get<GeneralPublicProfileResponse>(`${this.base}/get-profile`);
  }

  getAboutUs(){
    return this.httpClient.get<AboutUsResponse>(`${this.base}/get-about-us`);
  }
}
