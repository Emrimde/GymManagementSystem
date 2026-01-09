import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../services-api/auth-service';
import { ResetPasswordRequest } from '../../../dto/AuthDto/reset-password-request';

@Component({
  selector: 'app-forgot-password',
  imports: [ReactiveFormsModule],
  templateUrl: './forgot-password.html',
  styleUrl: './forgot-password.css',
})
export class ForgotPassword implements OnInit{
  resetPasswordForm!: FormGroup;
  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {}
  ngOnInit(): void {
    this.resetPasswordForm = this.buildResetPasswordForm(); 
  }

  buildResetPasswordForm(): FormGroup {
    return this.fb.group({
      email: ['', [Validators.required, Validators.email]],
    });
  }
  
 async submit() {
    if(this.resetPasswordForm.invalid) {
      return;
    }

      const resetPasswordRequest : ResetPasswordRequest = {
        email: this.resetPasswordForm.value.email
      }

      this.authService.resetPasswordAsync(resetPasswordRequest).subscribe({
        next: (response:any) => {
          console.log('Password reset email sent successfully:', response);
          this.router.navigate(['/client-login']);
        },
        error: (error:any) => {
          console.error('Error sending password reset email:', error);
        }
      });
      
  }

}
