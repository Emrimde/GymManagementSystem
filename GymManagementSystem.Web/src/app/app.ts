import { Component, signal } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { AuthStateService } from './services-api/auth-state-service';
import { AuthService } from './services-api/auth-service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,RouterLink],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('GymManagementSystem.Web');
  constructor(public authStateService: AuthStateService, private authService: AuthService){}
  
  logout(){
    localStorage.removeItem("token")
    this.authStateService.setLoggedIn(false);
  }
}
