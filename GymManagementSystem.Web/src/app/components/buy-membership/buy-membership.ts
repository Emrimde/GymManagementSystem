import { ChangeDetectorRef, Component, OnInit, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators, ɵInternalFormsSharedModule } from "@angular/forms";
import { ClientDetails } from '../../dto/Client/client-details';
import { ClientService } from '../../services-api/client-service';
import { ClientMembershipService } from '../../services-api/client-membership-service';
import { email } from '@angular/forms/signals';
import { ClientMembershipPreviewResponse } from '../../dto/ClientMembership/client-membership-preview-response';
import { ClientUpdateRequest } from '../../dto/Client/client-update-request';
import { ClientMembershipAddRequest } from '../../dto/ClientMembership/client-membership-add-request';

@Component({
  selector: 'app-buy-membership',
  imports: [ɵInternalFormsSharedModule,ReactiveFormsModule],
  templateUrl: './buy-membership.html',
  styleUrl: './buy-membership.css',
})
export class BuyMembership implements OnInit {
  clientMembershipForm!: FormGroup;
  id!: string | null;
  clientDetails = signal<ClientMembershipPreviewResponse | null>(null);

  successMessage: string | null = null;
  backendErrors: string[] = [];
  backendFieldErrors: Record<string, string[]> = {};
  isSubmitting = false;

  constructor(
    private route: ActivatedRoute,
    private clientService: ClientService,
    private clientMembershipService: ClientMembershipService,
    private fb: FormBuilder,
    private cdr: ChangeDetectorRef,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    this.clientMembershipForm = this.buildClientMembershipForm();

    if (!this.id) return;

    this.clientMembershipService.getClientMembershipPreview(this.id).subscribe({
      next: (response: any) => {
        response.dateOfBirth = response.dateOfBirth?.slice(0, 10) ?? null;
        this.clientDetails.set(response);
        this.clientMembershipForm.patchValue(response);
        this.cdr.detectChanges();
      },
      error: (err: any) => {
        console.error('Preview fetch error', err);
      }
    });
  }

  buildClientMembershipForm(): FormGroup {
    return this.fb.group({
      firstName: [{ value: '', disabled: true }],
      lastName: [{ value: '', disabled: true }],
      email: [{ value: '', disabled: true }],
      membershipName: [{ value: '', disabled: true }],

      phoneNumber: ['', [Validators.required]],
      dateOfBirth: ['', [Validators.required, this.pastDateValidator()]],
      street: ['', [Validators.required]],
      city: ['', [Validators.required]],
      acceptTerms: [false, [Validators.requiredTrue]]
    });
  }

  submit() {
    this.successMessage = null;
    this.backendErrors = [];
    this.backendFieldErrors = {};

    if (!this.clientMembershipForm) return;

    this.clientMembershipForm.markAllAsTouched();

    if (this.clientMembershipForm.invalid) {
      return;
    }

    this.isSubmitting = true;

    const dto: ClientUpdateRequest = {
      city: this.clientMembershipForm.get('city')!.value,
      street: this.clientMembershipForm.get('street')!.value,
      phoneNumber: this.clientMembershipForm.get('phoneNumber')!.value,
      dateOfBirth: this.clientMembershipForm.get('dateOfBirth')!.value
    };

    this.clientService.updateClient(dto).subscribe({
      next: () => {
        const clientMembershipAddRequest: ClientMembershipAddRequest = {
          membershipId: this.id!,
          isFromWeb: true
        };

        this.clientMembershipService.createClientMembership(clientMembershipAddRequest).subscribe({
          next: () => {
            this.successMessage = 'Membership purchased successfully.';
            this.isSubmitting = false;
            this.router.navigate(['/client-main-page']);
            this.cdr.detectChanges();
          },
          error: (err: any) => {
            this.isSubmitting = false;
            this.handleApiError(err);
          }
        });
      },
      error: (err: any) => {
        this.isSubmitting = false;
        this.handleApiError(err);
      }
    });
  }

  private handleApiError(err: any) {
    console.error('API error', err);
    const apiError = err?.error;

    if (apiError?.errors && typeof apiError.errors === 'object') {
      this.backendFieldErrors = apiError.errors;
      this.backendErrors = Object.values(apiError.errors).flat() as string[];
    } else if (apiError?.detail) {
      this.backendErrors = [apiError.detail];
    } else if (apiError?.message) {
      this.backendErrors = [apiError.message];
    } else {
      this.backendErrors = ['An unexpected error occurred.'];
    }

    this.cdr.detectChanges();
  }

  private pastDateValidator() {
  return (control: any) => {
    if (!control.value) return null;

    const selectedDate = new Date(control.value);
    const today = new Date();

    today.setHours(0, 0, 0, 0);

    if (selectedDate >= today) {
      return { futureDate: true };
    }

    return null;
  };
}
}