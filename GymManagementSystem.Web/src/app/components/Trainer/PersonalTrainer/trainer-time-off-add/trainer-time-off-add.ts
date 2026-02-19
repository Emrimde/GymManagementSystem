import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { TrainerServiceClient } from '../../../../services-api/trainer-service-client';
import { HttpErrorResponse } from '@angular/common/http';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-trainer-time-off-add',
  imports: [ReactiveFormsModule, CommonModule,RouterLink],
  templateUrl: './trainer-time-off-add.html',
  styleUrl: './trainer-time-off-add.css',
})
export class TrainerTimeOffAdd implements OnInit {

  form!: FormGroup;
  backendErrors: string[] = [];
  success = false;
  isSubmitting = false;

  timeSlots: string[] = [];

  constructor(
    private fb: FormBuilder,
    private trainerService: TrainerServiceClient,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.generateTimeSlots();
    this.form = this.buildForm();
  }

  buildForm(): FormGroup {
    return this.fb.group(
      {
        reason: [''],
        date: ['', Validators.required],
        startTime: ['', Validators.required],
        endTime: ['', Validators.required]
      },
      { validators: [this.endAfterStartValidator] }
    );
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

      this.timeSlots.push(`${hh}:${mm}`);
    }
  }

  endAfterStartValidator(group: FormGroup): ValidationErrors | null {
    const date = group.get('date')?.value;
    const start = group.get('startTime')?.value;
    const end = group.get('endTime')?.value;

    if (!date || !start || !end) return null;

    const startDate = new Date(`${date}T${start}`);
    const endDate = new Date(`${date}T${end}`);

    return endDate > startDate ? null : { endBeforeStart: true };
  }

  submit(): void {
  this.backendErrors = [];
  this.success = false;

  if (this.form.invalid) {
    this.form.markAllAsTouched();
    return;
  }

  this.isSubmitting = true;

  const { date, startTime, endTime, reason } = this.form.value;

  const dto = {
    start: `${date}T${startTime}`,
    end: `${date}T${endTime}`,
    reason: reason
  };

  this.trainerService.createTrainerTimeOff(dto).subscribe({
    next: () => {
      this.success = true;
      this.form.reset();
      this.isSubmitting = false;
      this.cdr.detectChanges();
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

      if (apiError?.message) {
        this.backendErrors = [apiError.message];
        this.isSubmitting = false;
        this.cdr.detectChanges();
        return;
      }

      this.backendErrors = ['Unknown error'];
      this.isSubmitting = false;
      this.cdr.detectChanges();
    }
  });
}

}