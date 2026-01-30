import { Component } from '@angular/core';
import { ActivatedRoute, Router, RouterOutlet } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators, ɵInternalFormsSharedModule } from "@angular/forms";
import { AuthService } from '../../../services-api/auth-service';
import { ActivateAccountRequest } from '../../../dto/AuthDto/activate-account-request';

@Component({
  selector: 'app-activate-account',
  imports: [ReactiveFormsModule],
  templateUrl: './activate-account.html',
  styleUrl: './activate-account.css',
})
export class ActivateAccount {
  
   activateAccountForm!: FormGroup;
  private userId!: string;
  private token!: string;

  constructor(
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.userId = params['userId'];
      this.token = params['token'];

      this.activateAccountForm = this.buildActivateAccountForm();
    });
  }

  submit(): void {
    if (this.activateAccountForm.invalid) {
      return;
    }

    if (this.activateAccountForm.value.newPassword !== this.activateAccountForm.value.confirmPassword) {
      this.activateAccountForm.setErrors({ passwordsNotMatch: true });
      return;
    }

    const dto: ActivateAccountRequest = {
      userId: this.userId,
      token: this.token,
      newPassword: this.activateAccountForm.value.newPassword
    };

    this.authService.activateAccount(dto).subscribe({
      next: () => {
        this.router.navigate(['/client-main-page']);
      },
      error: err => {
        console.error(err);
      }
    });
  }

  private buildActivateAccountForm(): FormGroup {
    return this.formBuilder.group({
      newPassword: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required]]
    });
  }
}
