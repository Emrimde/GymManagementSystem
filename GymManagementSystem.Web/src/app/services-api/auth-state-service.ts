import { Injectable, signal } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthStateService {
  isLoggedIn = signal<boolean>(!!localStorage.getItem('token'));
  roles = signal<string[]>([]);

  constructor() {
    const token = localStorage.getItem('token');
    if (token) {
      this.loadFromToken(token);
    }
  }

logout(): void {
  localStorage.removeItem('token');
  localStorage.removeItem('expirationDate');

  this.isLoggedIn.set(false);
  this.roles.set([]);
}

  setLoggedIn(v: boolean) {
    if (!v) {
      localStorage.removeItem('token');
      this.isLoggedIn.set(false);
      this.roles.set([]);
      return;
    }

    const token = localStorage.getItem('token');
    if (!token) return;

    this.isLoggedIn.set(true);
    this.loadFromToken(token);
  }

  setToken(token: string, refreshToken: string) {
    localStorage.setItem('token', token);
    localStorage.setItem('refreshToken', refreshToken);
    this.isLoggedIn.set(true);
    this.loadFromToken(token);
  }

  private loadFromToken(token: string) {
  try {
    const payload = JSON.parse(atob(token.split('.')[1]));
    const msRoleClaim = payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];

    let roles: string[] = [];

    if (Array.isArray(msRoleClaim)) {
      roles = msRoleClaim;
    } else if (typeof msRoleClaim === 'string') {
      roles = [msRoleClaim];
    }

    this.roles.set(roles);
  } catch {
    this.roles.set([]);
  }
}

 hasRole(role: string): boolean {
  return this.roles().includes(role);
}

isClient(): boolean {
  return this.hasRole('Client');
}

isTrainer(): boolean {
  return this.hasRole('Trainer');
}

isGroupInstructor(): boolean {
  return this.hasRole('GroupInstructor');
}

}

