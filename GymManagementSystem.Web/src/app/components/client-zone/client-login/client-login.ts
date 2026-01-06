import { Component, OnInit } from '@angular/core';
import { Form, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../services-api/auth-service';
import { SignInDto } from '../../../dto/sign-in-dto';
import { AuthenticationResponse } from '../../../dto/AuthDto/authentication-response';
import { AuthStateService } from '../../../services-api/auth-state-service';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-client-login',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './client-login.html',
  styleUrl: './client-login.css',
})
export class ClientLogin implements OnInit {
  
  constructor(private authService: AuthService, private fb:FormBuilder, private authStateService: AuthStateService, private router: Router){}

  loginForm!: FormGroup

  ngOnInit(): void {
    this.loginForm = this.buildLoginForm();
  }

  buildLoginForm(): FormGroup {
    return this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    })
  }

  async submit(){
    if(this.loginForm.invalid){
      return;
    }
    
    const dto: SignInDto = {
      email: this.loginForm.value.email,
      password: this.loginForm.value.password
    }
   
      this.authService.signIn(dto).subscribe({
        next: (response:any) =>{
          const result: AuthenticationResponse = response
          localStorage.setItem("token", result.token)
          localStorage.setItem("expirationDate", result.expirationDate)
          this.authStateService.setLoggedIn(true)
          this.router.navigate(['/client-main-page']);
          console.log(result)
        },
        error: (err) =>{
          console.error(err);
        }
      });
    }

  }





