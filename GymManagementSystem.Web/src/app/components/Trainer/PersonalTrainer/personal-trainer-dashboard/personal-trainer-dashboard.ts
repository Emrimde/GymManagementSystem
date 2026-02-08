import { Component, OnInit, signal } from '@angular/core';
import { TrainerServiceClient } from '../../../../services-api/trainer-service-client';
import { PersonalBookingForTrainerDto } from '../../../../dto/PersonalBooking/personal-booking-for-trainer-dto';

@Component({
  selector: 'app-personal-trainer-dashboard',
  imports: [],
  templateUrl: './personal-trainer-dashboard.html',
  styleUrl: './personal-trainer-dashboard.css',
})
export class PersonalTrainerDashboard implements OnInit {

  bookings = signal<PersonalBookingForTrainerDto[]>([]);
  loading = signal<boolean>(true);

  constructor(private trainerServiceClient: TrainerServiceClient) {}

  ngOnInit(): void {
    this.trainerServiceClient.getMyPersonalBookings().subscribe({
      next: (data: any) => {
        this.bookings.set(data);
        console.log(data);
        this.loading.set(false);
      },
      error: () => {
        this.loading.set(false);
      }
    });
  }
}