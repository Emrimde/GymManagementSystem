import { Component, OnInit, signal } from '@angular/core';
import { GymClassResponse } from '../../../dto/GymClass/gym-class-response';
import { Form, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { GymClassService } from '../../../services-api/gym-class-service';
import { Router } from '@angular/router';
import { ScheduledClassService } from '../../../services-api/scheduled-class-service';
import { ScheduledClassResponse } from '../../../dto/ScheduledClass/scheduled-class-response';
import { ClassBookingAddRequest } from '../../../dto/ClassBooking/class-booking-add-request';
import { ClassBookingService } from '../../../services-api/class-booking-service';

@Component({
  selector: 'app-add-class-booking',
  imports: [ReactiveFormsModule],
  templateUrl: './add-class-booking.html',
  styleUrl: './add-class-booking.css',
})
export class AddClassBooking implements OnInit {
gymClassChange($event: Event) {
 const selectElement = $event.target as HTMLSelectElement;
 const gymClassId = selectElement.value;

 this.scheduledClassService.getScheduledClasses(gymClassId).subscribe({
   next: (response: any) => {
     this.scheduledClasses.set(response);
   },
    error: (error:any) => {
      console.error(error);
    }
  });
}

async submit() {
  if(this.gymClassForm.invalid) {
    return;
  }
  const formValue = this.gymClassForm.value;
  const classBookingAddRequest: ClassBookingAddRequest = {
    scheduledClassId: formValue.scheduledClassId,
    isRequestFromWeb: true
  } 
  this.classBookingService.createClassBooking(classBookingAddRequest).subscribe({
    next: (response: any) => {
      console.log('Class booked successfully:', response);
    },
    error: (error:any) => {
      console.error('Error booking class:', error);
    }
  });
}
  gymClasses = signal<GymClassResponse[]>([]);
  gymClassForm! : FormGroup;
  scheduledClasses = signal<ScheduledClassResponse[]>([]); 
  constructor(private fb: FormBuilder, private gymClassService: GymClassService, private router: Router, private scheduledClassService: ScheduledClassService, private classBookingService: ClassBookingService) {}

  ngOnInit(): void {
    this.gymClassForm = this.buildGymClassForm();
    this.gymClassService.getGymClasses().subscribe({
      next: (response: any) => {
        this.gymClasses.set(response);
        console.log(this.gymClasses());
      },
      error: (error) => {
        console.error('Error fetching gym classes:', error);
      }
    })
  }

  buildGymClassForm(): FormGroup {
    return this.fb.group({
      scheduledClassId: ['', [Validators.required]],
      gymClassId: ['', [Validators.required]],
    });
  }
}
