import { Component, OnInit, signal, ChangeDetectorRef } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

import { TrainerService } from '../../../services-api/trainer-service';
import { PersonalBookingService } from '../../../services-api/personal-booking-service';

import { TrainerRateSelectResponse } from '../../../dto/TrainerRate/trainer-rate-select-response';
import { TrainerInfoResponse } from '../../../dto/Trainer/trainer-info-response';
import { PersonalBookingAddRequest } from '../../../dto/PersonalBooking/personal-booking-add-request';

@Component({
  selector: 'app-add-personal-booking',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './add-personal-booking.html',
  styleUrl: './add-personal-booking.css',
})
export class AddPersonalBooking implements OnInit {

  backendErrors: string[] = [];

  personalBookingForm!: FormGroup;

  trainerRateSelect = signal<TrainerRateSelectResponse[]>([]);
  trainerInfoResponse = signal<TrainerInfoResponse[]>([]);
  timeSlots: string[] = [];

  constructor(
    private trainerService: TrainerService,
    private personalBookingService: PersonalBookingService,
    private fb: FormBuilder,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.generateTimeSlots();
    this.personalBookingForm = this.buildPersonalBookingForm();

    this.trainerService.getAllPersonalTrainers().subscribe({
      next: (response: any) => {
        this.trainerInfoResponse.set(response);
      },
      error: (err) => {
        console.error('Error fetching trainers', err);
      }
    });
  }

  buildPersonalBookingForm(): FormGroup {
    return this.fb.group({
      startDate: ['', Validators.required],
      startHour: ['', Validators.required],
      trainerId: ['', Validators.required],
      trainerRateId: ['', Validators.required],
    });
  }

  generateTimeSlots(): void {
    const start = 7 * 60;
    const end = 22 * 60;
    const step = 15;

    for (let m = start; m <= end; m += step) {
      const h = Math.floor(m / 60);
      const min = m % 60;

      const hh = h.toString().padStart(2, '0');
      const mm = min.toString().padStart(2, '0');

      this.timeSlots.push(`${hh}:${mm}:00`);
    }
  }

  loadTrainerRates(event: Event): void {
    const select = event.target as HTMLSelectElement;
    const trainerId = select.value;

    this.personalBookingForm.get('trainerRateId')?.setValue('');
    this.trainerRateSelect.set([]);

    if (!trainerId) return;

    this.trainerService.getTrainerRatesById(trainerId).subscribe({
      next: (response: any) => {
        this.trainerRateSelect.set(response);
      },
      error: (err) => {
        console.error('Error loading trainer rates', err);
      }
    });
  }

  submit(): void {
    this.backendErrors = [];

    if (this.personalBookingForm.invalid) {
      this.personalBookingForm.markAllAsTouched();
      return;
    }

    const formData = this.personalBookingForm.value;

    const request: PersonalBookingAddRequest = {
      trainerId: formData.trainerId,
      trainerRateId: formData.trainerRateId,
      startDay: formData.startDate,
      startHour: formData.startHour,
      isClientReservation: true
    };

    this.personalBookingService.createPersonalBooking(request).subscribe({
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
}
