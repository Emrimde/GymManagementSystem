import { Component, OnInit } from '@angular/core';
import { GymClassDto } from '../../../../dto/GymClass/gym-class-dto';
import { TrainerServiceClient } from '../../../../services-api/trainer-service-client';

@Component({
  selector: 'app-group-instructor-dashboard',
  imports: [],
  templateUrl: './group-instructor-dashboard.html',
  styleUrl: './group-instructor-dashboard.css',
})

export class GroupInstructorDashboard implements OnInit {
  gymClasses: GymClassDto[] = [];
  loading = false;

  constructor(private trainerServiceClient: TrainerServiceClient) {}

  ngOnInit(): void {
    this.loading = true;
    this.trainerServiceClient.getMyGymClasses().subscribe({
      next: (data:any) => {
        this.gymClasses = data;
        this.loading = false;
      },
      error: () => this.loading = false
    });
  }

  openSchedule(gymClassId: string) {
   
  }
}
