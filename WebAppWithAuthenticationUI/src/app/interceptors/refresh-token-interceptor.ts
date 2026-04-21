import { HttpInterceptorFn } from '@angular/common/http';
import {catchError, pipe, throwError} from 'rxjs';
import {inject} from '@angular/core';
import {AuthService} from '../services/auth-service';
import {takeUntilDestroyed} from '@angular/core/rxjs-interop';

export const refreshTokenInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  return next(req).pipe(
    catchError((err, caught) => {
      if (err.status == 401){
        authService.refresh()
          .pipe(takeUntilDestroyed())
          .subscribe();
      }
      return throwError(() => err);
    })
  );
};
