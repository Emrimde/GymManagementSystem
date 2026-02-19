import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterOutlet } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, ValidationErrors, Validators, ɵInternalFormsSharedModule } from "@angular/forms";
import { AuthService } from '../../../services-api/auth-service';
import { ActivateAccountRequest } from '../../../dto/AuthDto/activate-account-request';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-activate-account',
  imports: [ReactiveFormsModule],
  templateUrl: './activate-account.html',
  styleUrl: './activate-account.css',
})
export class ActivateAccount implements OnInit {

  activateAccountForm!: FormGroup;
  backendErrors: string[] = [];
  isSubmitting = false;

  private userId!: string;
  private token!: string;
  isActivated = false;

  constructor(
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.userId = params['userId'];
      this.token = params['token'];

      if (!this.userId || !this.token) {
        this.backendErrors = ['Invalid activation link'];
        this.cdr.detectChanges();
        return;
      }

      this.activateAccountForm = this.buildActivateAccountForm();
    });
  }

  submit(): void {
    this.backendErrors = [];

    if (this.activateAccountForm.invalid) {
      this.activateAccountForm.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;

    const dto: ActivateAccountRequest = {
      userId: this.userId,
      token: this.token,
      newPassword: this.activateAccountForm.value.newPassword
    };

    this.authService.activateAccount(dto).subscribe({
      next: () => {
  this.isSubmitting = false;
  this.isActivated = true;
  this.activateAccountForm.disable();
},
      error: (err: HttpErrorResponse) => {
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

        this.backendErrors = ['Unable to activate account'];
        this.isSubmitting = false;
        this.cdr.detectChanges();
      }
    });
  }

  private buildActivateAccountForm(): FormGroup {
    return this.formBuilder.group(
      {
        newPassword: ['', [Validators.required, Validators.minLength(6)]],
        confirmPassword: ['', Validators.required]
      },
      { validators: [this.passwordMatchValidator] }
    );
  }

  private passwordMatchValidator(group: FormGroup): ValidationErrors | null {
    const password = group.get('newPassword')?.value;
    const confirm = group.get('confirmPassword')?.value;
    return password === confirm ? null : { passwordsNotMatch: true };
  }
}
