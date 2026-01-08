import { Injectable } from '@angular/core';
import { SignInDto } from '../dto/sign-in-dto';
import { HttpClient } from '@angular/common/http';
import { AuthenticationResponse } from '../dto/AuthDto/authentication-response';
import { ChangePasswordRequest } from '../dto/AuthDto/change-password-request';

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  constructor(private httpClient:HttpClient){}
  private readonly base = "http://localhost:5105/api/Auth"
  signIn(signInDto: SignInDto) {
    return this.httpClient.post(`${this.base}/login`, signInDto)
  }
  
  changePassword(changePasswordRequest: ChangePasswordRequest) {
    return this.httpClient.post(`${this.base}/change-password`, changePasswordRequest)
  }
}
