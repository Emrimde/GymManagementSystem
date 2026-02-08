import { inject } from '@angular/core';
import { CanActivateFn,Router } from '@angular/router';
import { AuthStateService } from '../services-api/auth-state-service';

export const authGuardGuard: CanActivateFn = (route, state) => {
  // const token = localStorage.getItem('token');
  const router = inject(Router);
  const auth = inject(AuthStateService);
 if (!auth.isLoggedIn()) {
    return router.createUrlTree(['/login-client']);
  }

  return true;
};
