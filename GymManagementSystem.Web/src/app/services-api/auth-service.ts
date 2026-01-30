import { Injectable } from '@angular/core';
import { SignInDto } from '../dto/sign-in-dto';
import { HttpClient } from '@angular/common/http';
import { AuthenticationResponse } from '../dto/AuthDto/authentication-response';
import { ChangePasswordRequest } from '../dto/AuthDto/change-password-request';
import { ResetPasswordRequest } from '../dto/AuthDto/reset-password-request';
import { ActivateAccountRequest } from '../dto/AuthDto/activate-account-request';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private httpClient:HttpClient){}
  private readonly base = "http://localhost:5105/api/Auth"
  
  resetPasswordAsync(resetPasswordRequest: ResetPasswordRequest) {
    return this.httpClient.post(`${this.base}/reset-password`, resetPasswordRequest);
  }

  signIn(signInDto: SignInDto) {
    return this.httpClient.post(`${this.base}/login`, signInDto)
  }
  
  changePassword(changePasswordRequest: ChangePasswordRequest) {
    return this.httpClient.post(`${this.base}/change-password`, changePasswordRequest)
  }

  activateAccount(changePasswordRequest: ActivateAccountRequest) {
    return this.httpClient.post(`${this.base}/activate-client-account`, changePasswordRequest)
  }
}
