import { Component, OnInit, signal } from '@angular/core';
import { TrainerServiceClient } from '../../../../services-api/trainer-service-client';
import { PersonalBookingForTrainerDto } from '../../../../dto/PersonalBooking/personal-booking-for-trainer-dto';
import { TrainerPanelInfoDto } from '../../../../dto/Trainer/trainer-panel-info-dto';

@Component({
  selector: 'app-personal-trainer-dashboard',
  imports: [],
  templateUrl: './personal-trainer-dashboard.html',
  styleUrl: './personal-trainer-dashboard.css',
})
export class PersonalTrainerDashboard implements OnInit {

  panel = signal<TrainerPanelInfoDto | null>(null);
  loading = signal<boolean>(true);

  constructor(private trainerServiceClient: TrainerServiceClient) {}

  ngOnInit(): void {
    this.trainerServiceClient.getTrainerPanelInfo().subscribe({
      next: (data: any) => {
        this.panel.set(data);
        this.loading.set(false);
      },
      error: () => {
        this.loading.set(false);
      }
    });
  }
}