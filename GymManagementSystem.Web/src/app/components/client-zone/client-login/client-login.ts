import { Component, OnInit } from '@angular/core';
import { Form, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../services-api/auth-service';
import { SignInDto } from '../../../dto/sign-in-dto';

@Component({
  selector: 'app-client-login',
  imports: [ReactiveFormsModule],
  templateUrl: './client-login.html',
  styleUrl: './client-login.css',
})
export class ClientLogin implements OnInit {
  
  constructor(private authService: AuthService, private fb:FormBuilder){}

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
    try{
      const response:any = await this.authService.signIn(dto).subscribe();
      console.log(response)
    }
    catch(err){
      
    }
  }
}




