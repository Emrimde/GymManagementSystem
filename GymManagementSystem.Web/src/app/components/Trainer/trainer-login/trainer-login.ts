import { HttpErrorResponse } from '@angular/common/http';
import { ChangeDetectorRef, Component } from '@angular/core';
import { AuthenticationResponse } from '../../../dto/AuthDto/authentication-response';
import { SignInDto } from '../../../dto/sign-in-dto';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../services-api/auth-service';
import { AuthStateService } from '../../../services-api/auth-state-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-trainer-login',
  imports: [ReactiveFormsModule],
  templateUrl: './trainer-login.html',
  styleUrl: './trainer-login.css',
})
export class TrainerLogin  {
  loginForm!: FormGroup;
  backendErrors: string[] = [];

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private authStateService: AuthStateService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
  }

  submit(): void {
    this.backendErrors = [];

    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    const dto: SignInDto = {
      email: this.loginForm.value.email,
      password: this.loginForm.value.password,
    };

    this.authService.signIn(dto).subscribe({
      next: (response: any) => {
        this.authStateService.setToken(response.token);
        localStorage.setItem('expirationDate', response.expirationDate);

        if (
          this.authStateService.isTrainer() ||
          this.authStateService.isGroupInstructor()
        ) {
          this.router.navigate(['/trainer']);
          return;
        }

        this.authStateService.logout();
        this.backendErrors = ['Access denied. Trainer account required.'];
        this.cdr.detectChanges();
      },
      error: (err: HttpErrorResponse) => {
        const apiError = err.error;

        if (apiError?.errors) {
          this.backendErrors = Object.values(apiError.errors).flat() as string[];
        } else if (apiError?.detail) {
          this.backendErrors = [apiError.detail];
        } else {
          this.backendErrors = ['Login failed'];
        }

        this.cdr.detectChanges();
      },
    });
  }
}
