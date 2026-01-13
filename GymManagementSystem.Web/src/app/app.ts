import { Component, signal } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { AuthStateService } from './services-api/auth-state-service';
import { AuthService } from './services-api/auth-service';
import { GeneralGymDetailService } from './services-api/general-gym-detail-service';
import { Observable } from 'rxjs';
import { GeneralPublicProfileResponse } from './dto/GeneralGymDetail/general-public-profile-response';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,RouterLink,AsyncPipe],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('GymManagementSystem.Web');
  generalProfile$!: Observable<GeneralPublicProfileResponse>;
  constructor(public authStateService: AuthStateService, private authService: AuthService, private router: Router, private generalGymDetailService: GeneralGymDetailService){}
  
  ngOnInit(): void {
    this.generalProfile$ = this.generalGymDetailService.getPublicGymProfile();
  }

  logout(){
    localStorage.removeItem("token")
    this.authStateService.setLoggedIn(false);
    this.router.navigate(['/']); 
  }
}
