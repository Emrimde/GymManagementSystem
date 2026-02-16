import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { ClientAddRequest } from '../../../dto/Client/client-add-request';
import { AuthService } from '../../../services-api/auth-service';
import { ClientService } from '../../../services-api/client-service';
import { FormBuilder, FormGroup, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { email } from '@angular/forms/signals';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-client-register',
  imports: [RouterLink,ReactiveFormsModule],
  templateUrl: './client-register.html',
  styleUrl: './client-register.css',
})
export class ClientRegister implements OnInit {

  backendErrors: string[] = [];
  clientRegisterForm!: FormGroup;

  constructor(
    private clientService: ClientService,
    private fb: FormBuilder,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.clientRegisterForm = this.buildRegisterForm();
  }

  buildRegisterForm(): FormGroup {
    return this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required]
    }, { validators: [this.passwordMatchValidator] });
  }

  passwordMatchValidator(group: FormGroup): ValidationErrors | null {
    const password = group.get('password')?.value;
    const confirmPassword = group.get('confirmPassword')?.value;
    return password === confirmPassword ? null : { passwordMismatch: true };
  }

  submit() {
    this.backendErrors = [];

    if (this.clientRegisterForm.invalid) {
      this.clientRegisterForm.markAllAsTouched();
      return;
    }

    const dto: ClientAddRequest = this.clientRegisterForm.getRawValue();

    this.clientService.createClient(dto).subscribe({
      next: () => {
        this.router.navigate(['/login-client']);
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

        this.backendErrors = ['Registration failed'];
        this.cdr.detectChanges();
      }
    });
  }
}
