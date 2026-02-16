import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
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
export class ForgotPassword implements OnInit {

  resetPasswordForm!: FormGroup;
  successMessage: string | null = null;
  backendErrors: string[] = [];

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.resetPasswordForm = this.buildResetPasswordForm();
  }

  buildResetPasswordForm(): FormGroup {
    return this.fb.group({
      email: ['', [Validators.required, Validators.email]],
    });
  }

  submit() {
    this.successMessage = null;
    this.backendErrors = [];

    if (this.resetPasswordForm.invalid) {
      this.resetPasswordForm.markAllAsTouched();
      return;
    }

    const request: ResetPasswordRequest = {
      email: this.resetPasswordForm.value.email
    };

    this.authService.resetPasswordAsync(request).subscribe({
      next: () => {
        this.successMessage = 'Password reset email sent successfully.';
        this.resetPasswordForm.reset();
        this.cdr.detectChanges();
      },
      error: (err: any) => {

        const apiError = err.error;

        if (apiError?.errors) {
          this.backendErrors = Object.values(apiError.errors).flat() as string[];
        } else if (apiError?.detail) {
          this.backendErrors = [apiError.detail];
        } else {
          this.backendErrors = ['Failed to send reset email.'];
        }

        this.cdr.detectChanges();
      }
    });
  }
}
