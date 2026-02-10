import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { TrainerServiceClient } from '../../../../services-api/trainer-service-client';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-trainer-time-off-add',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './trainer-time-off-add.html',
  styleUrl: './trainer-time-off-add.css',
})
export class TrainerTimeOffAdd implements OnInit {
  form!: FormGroup;
  isSubmitting = false;
  success = false;
  backendErrors: string[] = [];

  constructor(
    private fb: FormBuilder,
    private trainerService: TrainerServiceClient,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.form = this.buildForm();
  }

  buildForm(): FormGroup {
    return this.fb.group(
      {
        start: ['', [Validators.required, this.quarterHourValidator]],
        end: ['', [Validators.required, this.quarterHourValidator]],
        reason: ['']
      },
      { validators: [this.endAfterStartValidator] }
    );
  }

  submit(): void {
    this.backendErrors = [];
    this.success = false;

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;

    const start = new Date(this.form.value.start);
    const end = new Date(this.form.value.end);

    const dto = {
      start: start.toISOString(),
      end: end.toISOString(),
      reason: this.form.value.reason
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

        // ValidationProblemDetails style: { errors: { field1: [...], field2: [...] } }
        if (apiError?.errors) {
          try {
            this.backendErrors = Object.values(apiError.errors).flat() as string[];
          } catch {
            this.backendErrors = ['Validation error'];
          }
          this.isSubmitting = false;
          this.cdr.detectChanges();
          return;
        }

        // RFC-like detail field
        if (apiError?.detail) {
          this.backendErrors = [apiError.detail];
          this.isSubmitting = false;
          this.cdr.detectChanges();
          return;
        }

        // fallback to message or status text
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

  quarterHourValidator(control: any) {
    if (!control?.value) return null;
    const date = new Date(control.value);
    return date.getMinutes() % 15 === 0 ? null : { quarterHour: true };
  }

  endAfterStartValidator(group: FormGroup): ValidationErrors | null {
    const start = group.get('start')?.value;
    const end = group.get('end')?.value;
    if (!start || !end) return null;
    return new Date(end) > new Date(start) ? null : { endBeforeStart: true };
  }
}