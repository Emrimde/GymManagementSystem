import { Component, OnInit } from '@angular/core';
import { Form, FormBuilder, FormGroup, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../services-api/auth-service';
import { ValidationError } from '@angular/forms/signals';
import { ChangePasswordRequest } from '../../../dto/AuthDto/change-password-request';

@Component({
  selector: 'app-change-password',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './change-password.html',
  styleUrl: './change-password.css',
})
export class ChangePassword implements OnInit {
  async submit() {
    if(this.changePasswordForm.invalid){
      console.error('Form is invalid');
      return;
    }

    const changePasswordRequest: ChangePasswordRequest = {
      currentPassword: this.changePasswordForm.value.oldPassword,
      newPassword: this.changePasswordForm.value.newPassword,
    };
    
    this.authService.changePassword(changePasswordRequest).subscribe({
      next: (response) => {
        console.log('Password changed successfully:', response);
        this.router.navigate(['/client-main-page']);
      },
      error: (error) => {
        console.error('Error changing password:', error);
      }
    });
  }
  constructor(private router: Router, private fb: FormBuilder, private authService: AuthService) { }
  changePasswordForm!: FormGroup
  ngOnInit(): void {
    this.changePasswordForm = this.buildPasswordForm()
  }

  buildPasswordForm() {
    return this.fb.group({
      oldPassword: ['', Validators.required],
      newPassword: ['', Validators.required],
      confirmPassword: ['', [Validators.required]],
     
    }, { validators: [ this.passwordMatchValidator ] });
  }

  passwordMatchValidator(group: FormGroup): ValidationErrors | null {
    const newPassword = group.get('newPassword')?.value;
    const confirmPassword = group.get('confirmPassword')?.value;
    return newPassword === confirmPassword ? null : { passwordMismatch: true };
  }

}
