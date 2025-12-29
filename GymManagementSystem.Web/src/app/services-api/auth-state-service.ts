import { Injectable, signal } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthStateService {
  isLoggedIn = signal<boolean>(!!localStorage.getItem('token'));

  setLoggedIn(v: boolean) {
    this.isLoggedIn.set(v);
  }
}

