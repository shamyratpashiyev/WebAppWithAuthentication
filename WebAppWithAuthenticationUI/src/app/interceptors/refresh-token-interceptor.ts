import { HttpInterceptorFn } from '@angular/common/http';
import {catchError, throwError} from 'rxjs';
import {inject} from '@angular/core';
import {AuthService} from '../services/auth-service';
import {switchMap} from 'rxjs/operators';
import {environment} from '../../environments/environment';

export const refreshTokenInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const refreshTokenPath = environment.refreshTokenPath;
  return next(req).pipe(
    catchError((err) => {
      if (err.status === 401 && !req.url.includes(refreshTokenPath)) {
        return authService.refresh().pipe(
          switchMap(() => {
            return next(req.clone({withCredentials: true}));
          }),
          catchError((refreshErr) => {
            return throwError(() => refreshErr);
          })
        );
      }
      return throwError(() => err);
    })
  );
};
