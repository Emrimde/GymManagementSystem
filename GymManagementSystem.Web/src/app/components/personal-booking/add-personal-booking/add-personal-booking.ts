import { Component, OnInit, signal } from '@angular/core';
import { TrainerService } from '../../../services-api/trainer-service';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { TrainerRateSelectResponse } from '../../../dto/TrainerRate/trainer-rate-select-response';
import { TrainerInfoResponse } from '../../../dto/Trainer/trainer-info-response';
import { PersonalBookingAddRequest } from '../../../dto/PersonalBooking/personal-booking-add-request';
import { NotExpr } from '@angular/compiler';
import { Router } from '@angular/router';
import { PersonalBookingService } from '../../../services-api/personal-booking-service';

@Component({
  selector: 'app-add-personal-booking',
  imports: [ReactiveFormsModule],
  templateUrl: './add-personal-booking.html',
  styleUrl: './add-personal-booking.css',
})
export class AddPersonalBooking implements OnInit {
async submit() {
  const formData = this.personalBookingForm.value;
  this.personalBookingAddRequest = {
    trainerId: formData.trainerId,
    trainerRateId: formData.trainerRateId,
    startDay: formData.startDate,
    startHour: formData.startHour,
    isClientReservation: true
  };
  console.log(this.personalBookingAddRequest);
  this.personalBookingService.createPersonalBooking(this.personalBookingAddRequest).subscribe({
    next: () => {
      
      this.router.navigate(['/client-main-page']);
    },
    error: (err) => {
      console.error(err);
    }
  })
}



// changeDate($event: Event) {
//   const input = $event.target as HTMLInputElement;
//   const selectedDate = input.value;
//   this.personalBookingAddRequest.startDate = selectedDate;
// }
// changeTime($event: Event) {
//   const select = $event.target as HTMLSelectElement;
//   const selectedHour = select.value;
//   this.personalBookingAddRequest.startHour = selectedHour;
// }

// changeTrainerRateSelect($event: Event) {
//   const select = $event.target as HTMLSelectElement;
//   const selectedTrainerRateId = select.value;
//   // this.personalBookingAddRequest.trainerRateId = selectedTrainerRateId;
// }

loadTrainerRates($event: Event) {
    const select = $event.target as HTMLSelectElement;
    const selectedTrainerId = select.value;
    this.trainerService.getTrainerRatesById(selectedTrainerId).subscribe({
      next: (response: any) => {
        this.trainerRateSelect.set(response);
        // this.personalBookingAddRequest.trainerId = selectedTrainerId;
      }
    });
}

  constructor(private trainerService: TrainerService, private fb: FormBuilder, private router:Router, private personalBookingService:PersonalBookingService ) {}
  personalBookingForm!: FormGroup;
  trainerRateSelect = signal<TrainerRateSelectResponse[]>([]);
  trainerInfoResponse = signal<TrainerInfoResponse[]>([]);
  personalBookingAddRequest!: PersonalBookingAddRequest;
  timeSlots: string[] = [];
  ngOnInit(): void {
    this.generateTimeSlots();
    this.personalBookingForm = this.buildPersonalBookingForm();
    this.trainerService.getAllPersonalTrainers().subscribe({
      next: (response: any) => {
        this.trainerInfoResponse.set(response);
      }
    })
  }
  generateTimeSlots() {
    const start = 7 * 60;   
    const end   = 22 * 60;  
    const step  = 15;

    for (let m = start; m <= end; m += step) {
      const h = Math.floor(m / 60);
      const min = m % 60;

      const hh = h.toString().padStart(2, '0');
      const mm = min.toString().padStart(2, '0');

      this.timeSlots.push(`${hh}:${mm}:00`);
  }
}

  buildPersonalBookingForm(): FormGroup {
    return this.fb.group({
      trainerId: [''],
      trainerRateId: [''],
      startDate: [''],
      startHour: [''],
    });
  }
}
