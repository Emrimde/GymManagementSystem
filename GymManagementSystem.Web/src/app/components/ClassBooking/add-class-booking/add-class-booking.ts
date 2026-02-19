import { Component, OnInit, signal, ChangeDetectorRef } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

import { GymClassResponse } from '../../../dto/GymClass/gym-class-response';
import { ScheduledClassResponse } from '../../../dto/ScheduledClass/scheduled-class-response';
import { ClassBookingAddRequest } from '../../../dto/ClassBooking/class-booking-add-request';

import { GymClassService } from '../../../services-api/gym-class-service';
import { ScheduledClassService } from '../../../services-api/scheduled-class-service';
import { ClassBookingService } from '../../../services-api/class-booking-service';

@Component({
  selector: 'app-add-class-booking',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './add-class-booking.html',
  styleUrl: './add-class-booking.css',
})
export class AddClassBooking implements OnInit {

  backendErrors: string[] = [];

  gymClasses = signal<GymClassResponse[]>([]);
  scheduledClasses = signal<ScheduledClassResponse[]>([]);

  gymClassForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private gymClassService: GymClassService,
    private scheduledClassService: ScheduledClassService,
    private classBookingService: ClassBookingService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.gymClassForm = this.fb.group({
      gymClassId: ['', Validators.required],
      scheduledClassId: ['', Validators.required]
    });

    this.loadGymClasses();
  }

  private loadGymClasses(): void {
    this.gymClassService.getGymClasses().subscribe({
      next: (response: any) => {
        this.gymClasses.set(response);
      },
      error: (err) => {
        console.error('Error fetching gym classes', err);
      }
    });
  }

  gymClassChange(event: Event): void {
    const select = event.target as HTMLSelectElement;
    const gymClassId = select.value;

    this.gymClassForm.get('scheduledClassId')?.setValue('');
    this.scheduledClasses.set([]);

    if (!gymClassId) return;

    this.scheduledClassService.getScheduledClasses(gymClassId).subscribe({
      next: (response: any) => {
        this.scheduledClasses.set(response);
      },
      error: (err) => {
        console.error('Error fetching scheduled classes', err);
      }
    });
  }

  submit(): void {
    this.backendErrors = [];

    if (this.gymClassForm.invalid) {
      this.gymClassForm.markAllAsTouched();
      return;
    }

    const formValue = this.gymClassForm.value;

    const request: ClassBookingAddRequest = {
      scheduledClassId: formValue.scheduledClassId,
      isRequestFromWeb: true
    };

    this.classBookingService.createClassBooking(request).subscribe({
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
