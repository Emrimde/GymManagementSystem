import { CanActivateFn, Router } from '@angular/router';
import { AuthStateService } from '../services-api/auth-state-service';
import { inject } from '@angular/core';

export const roleGuardGuard: CanActivateFn = (route, state) => {
 const auth = inject(AuthStateService);
  const router = inject(Router);

  const allowedRoles = route.data['roles'] as string[];

  if (!auth.isLoggedIn()) {
    return router.createUrlTree(['/login-client']);
  }

  if (!allowedRoles.some(item => auth.hasRole(item))) {
    return router.createUrlTree(['/access-denied']);
  }

  return true;
};
