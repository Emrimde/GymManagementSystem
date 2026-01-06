import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { ClientAddRequest } from '../../../dto/Client/client-add-request';
import { AuthService } from '../../../services-api/auth-service';
import { ClientService } from '../../../services-api/client-service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { email } from '@angular/forms/signals';

@Component({
  selector: 'app-client-register',
  imports: [RouterLink,ReactiveFormsModule],
  templateUrl: './client-register.html',
  styleUrl: './client-register.css',
})
export class ClientRegister implements OnInit {
  constructor(private clientService: ClientService, private fb:FormBuilder, private router:Router){}
  clientRegisterForm!: FormGroup
  ngOnInit(): void {
    this.clientRegisterForm = this.buildRegisterForm();
  }
  buildRegisterForm(): FormGroup {
    return this.fb.group({
      email:['',[Validators.required, Validators.email]],
      phoneNumber:['',[Validators.required]],
      firstName:['',[Validators.required]],
      lastName:['',[Validators.required]],
      password:['',[Validators.required]],
      confirmPassword:['',[Validators.required]]
    })
  }

  async submit(){
    if(this.clientRegisterForm.invalid){
      return
    }

    const dto: ClientAddRequest = {
      email: this.clientRegisterForm.value.email,
      password: this.clientRegisterForm.value.password,
      firstName: this.clientRegisterForm.value.firstName,
      lastName: this.clientRegisterForm.value.lastName,
      confirmPassword: this.clientRegisterForm.value.confirmPassword,
      phoneNumber:  this.clientRegisterForm.value.phoneNumber,

    }

    this.clientService.createClient(dto).subscribe({
      next: (response:any) => {
        this.router.navigate(['/login-client']);
      },
      error: (error:any) => {
        console.error(error)
      }
    })
  }
}
