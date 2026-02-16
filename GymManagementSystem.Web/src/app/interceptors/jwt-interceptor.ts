import { inject } from '@angular/core';
import { HttpInterceptorFn } from '@angular/common/http';
import { catchError, switchMap, throwError } from 'rxjs';
import { AuthStateService } from '../services-api/auth-state-service';
import { AuthService } from '../services-api/auth-service';


export const JwtInterceptor: HttpInterceptorFn = (req, next) => {

  const authStateService = inject(AuthStateService);
  const authService = inject(AuthService);

  const token = localStorage.getItem('token');

  let authReq = req;

  if (token && !req.url.includes('/refresh')) {
    authReq = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
  }

  return next(authReq).pipe(

    catchError(error => {

      if (
        error.status === 401 &&
        !req.url.includes('/refresh')
      ) {

        return authService.refreshToken().pipe(

          switchMap((newToken: string) => {

            const retryReq = req.clone({
              setHeaders: {
                Authorization: `Bearer ${newToken}`
              }
            });

            return next(retryReq);
          }),

          catchError(err => {
            authStateService.logout();
            return throwError(() => err);
          })
        );
      }

      return throwError(() => error);
    })
  );
};
