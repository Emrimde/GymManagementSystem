import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Form, FormBuilder, FormGroup, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../services-api/auth-service';
import { ValidationError } from '@angular/forms/signals';
import { ChangePasswordRequest } from '../../../dto/AuthDto/change-password-request';
import { HttpErrorResponse } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-change-password',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './change-password.html',
  styleUrl: './change-password.css',
})
export class ChangePassword implements OnInit {
  backendErrors: string[] = [];

async submit() {
  this.backendErrors = [];

  const req: ChangePasswordRequest = {
    currentPassword: this.changePasswordForm.value.oldPassword,
    newPassword: this.changePasswordForm.value.newPassword,
  };

  this.authService.changePassword(req).subscribe({
    next: () => {
      this.router.navigate(['/client-main-page']);
    },
   error: (err: HttpErrorResponse) => {
  const apiError = err.error;

  if (apiError?.errors) {
    this.backendErrors = Object.values(apiError.errors).flat() as string[];
    this.cdr.detectChanges(); 
    return;
  }

  if (apiError?.detail) {
    this.backendErrors = [apiError.detail];
    this.cdr.detectChanges(); 
    return;
  }

  this.backendErrors = ['Unknown error'];
  this.cdr.detectChanges(); 
}
  });
}

  constructor(private router: Router, private fb: FormBuilder, private authService: AuthService,private cdr: ChangeDetectorRef) { }
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
