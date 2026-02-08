import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services-api/auth-service';
import { Router } from '@angular/router';
import { TrainerServiceClient } from '../../../services-api/trainer-service-client';
import { GymClassDto } from '../../../dto/GymClass/gym-class-dto';
import { AuthStateService } from '../../../services-api/auth-state-service';
import { GroupInstructorDashboard } from "../GroupInstructor/group-instructor-dashboard/group-instructor-dashboard";
import { PersonalTrainerDashboard } from "../PersonalTrainer/personal-trainer-dashboard/personal-trainer-dashboard";

@Component({
  selector: 'app-trainer-main',
  imports: [GroupInstructorDashboard, PersonalTrainerDashboard],
  templateUrl: './trainer-main.html',
  styleUrl: './trainer-main.css',
})
export class TrainerMain implements OnInit {

  constructor(private auth: AuthStateService) {}

  ngOnInit(): void {
  }

  get isGroupInstructor(): boolean {
    return this.auth.hasRole('GroupInstructor');
  }

  get isPersonalTrainer(): boolean {
    return this.auth.hasRole('Trainer');
  }
}