import { Injectable } from '@angular/core';
import { SignInDto } from '../dto/sign-in-dto';
import { HttpClient } from '@angular/common/http';
import { AuthenticationResponse } from '../dto/AuthDto/authentication-response';
import { ChangePasswordRequest } from '../dto/AuthDto/change-password-request';
import { ResetPasswordRequest } from '../dto/AuthDto/reset-password-request';
import { ConfirmResetPasswordRequest } from '../dto/AuthDto/confirm-reset-password-request';
import { ActivateAccountRequest } from '../dto/AuthDto/activate-account-request';
import { map, tap } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private httpClient:HttpClient){}
  private readonly base = `${environment.apiUrl}/Auth`

 refreshToken() {
  const refreshToken = localStorage.getItem('refreshToken');

  return this.httpClient
    .post<any>(`${this.base}/refresh`, { refreshToken })
    .pipe(
      tap(res => {
        localStorage.setItem('token', res.token);
        localStorage.setItem('refreshToken', res.refreshToken);
      }),
      map(res => res.token)
    );
}
  
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
  confirmResetPassword(confirmResetPasswordRequest: ConfirmResetPasswordRequest) {
    return this.httpClient.post(`${this.base}/reset-password-confirm`, confirmResetPasswordRequest)
  }
  
}
