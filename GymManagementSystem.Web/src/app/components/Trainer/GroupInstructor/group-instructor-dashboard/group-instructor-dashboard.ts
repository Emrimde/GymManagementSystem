import { Component, OnInit, signal } from '@angular/core';
import { GymClassDto } from '../../../../dto/GymClass/gym-class-dto';
import { TrainerServiceClient } from '../../../../services-api/trainer-service-client';
import { GroupInstructorPanelResponse } from '../../../../dto/Trainer/group-instructor-panel-response';

@Component({
  selector: 'app-group-instructor-dashboard',
  imports: [],
  templateUrl: './group-instructor-dashboard.html',
  styleUrl: './group-instructor-dashboard.css',
})

export class GroupInstructorDashboard implements OnInit {

  panel = signal<GroupInstructorPanelResponse | null>(null);
  loading = signal<boolean>(true);

  constructor(private trainerServiceClient: TrainerServiceClient) {}

  ngOnInit(): void {
    this.trainerServiceClient.getGroupInstructorPanelInfo().subscribe({
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
