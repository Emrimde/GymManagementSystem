import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { ResetPasswordRequest } from '../../../dto/AuthDto/reset-password-request';
import { ConfirmResetPasswordRequest } from '../../../dto/AuthDto/confirm-reset-password-request';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../services-api/auth-service';
import { HttpErrorResponse } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-reset-client-password',
  imports: [ReactiveFormsModule,CommonModule, RouterLink],
  templateUrl: './reset-client-password.html',
  styleUrl: './reset-client-password.css',
})
export class ResetClientPassword implements OnInit {
  resetPasswordForm!: FormGroup;
  backendErrors: string[] = [];
  isSubmitting = false;

  private userId!: string;
  private token!: string;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.readQueryParams();
    this.resetPasswordForm = this.buildForm();
  }

  private readQueryParams(): void {
    this.userId = this.route.snapshot.queryParamMap.get('userId') ?? '';
    this.token = this.route.snapshot.queryParamMap.get('token') ?? '';

    if (!this.userId || !this.token) {
      this.backendErrors = ['Invalid password reset link'];
      this.cdr.detectChanges();
    }
  }

  buildForm(): FormGroup {
    return this.fb.group(
      {
        newPassword: ['', [Validators.required, Validators.minLength(6)]],
        confirmPassword: ['', Validators.required],
      },
      { validators: [this.passwordMatchValidator] }
    );
  }

  submit(): void {
    this.backendErrors = [];

    if (this.resetPasswordForm.invalid || !this.userId || !this.token) {
      this.resetPasswordForm.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;

    const dto: ConfirmResetPasswordRequest = {
      userId: this.userId,
      token: this.token,
      newPassword: this.resetPasswordForm.value.newPassword
    };

    this.authService.confirmResetPassword(dto).subscribe({
      next: () => {
        this.router.navigate(['/login-client']);
      },
      error: (err: any) => {
        const apiError = err.error;

        if (apiError?.errors) {
          this.backendErrors = Object.values(apiError.errors).flat() as string[];
          this.isSubmitting = false;
          this.cdr.detectChanges();
          return;
        }

        if (apiError?.detail) {
          this.backendErrors = [apiError.detail];
          this.isSubmitting = false;
          this.cdr.detectChanges();
          return;
        }

        this.backendErrors = ['Unable to reset password'];
        this.isSubmitting = false;
        this.cdr.detectChanges();
      }
    });
  }

  passwordMatchValidator(group: FormGroup): ValidationErrors | null {
    const password = group.get('newPassword')?.value;
    const confirm = group.get('confirmPassword')?.value;
    return password === confirm ? null : { passwordMismatch: true };
  }
}