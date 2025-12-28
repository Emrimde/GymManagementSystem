import { Injectable } from '@angular/core';
import { SignInDto } from '../dto/sign-in-dto';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  constructor(private httpClient:HttpClient){}
  private readonly base = "http://localhost:5105/api/Auth"
  signIn(dto: SignInDto) {
    return this.httpClient.post(`${this.base}/login`, dto)
  }
  
}
