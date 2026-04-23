import {HttpInterceptorFn} from '@angular/common/http';
import {catchError, throwError} from 'rxjs';
import {inject} from '@angular/core';
import {Router} from '@angular/router';
import {NotificationService} from '../services/notification-service';

export const errorHandlerInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const notificationService = inject(NotificationService);

  return next(req).pipe(
    catchError((err, caught) => {
      if(err.status == 401) {
        router.navigateByUrl('/login').then();
      }
      else {
        notificationService.throwError('Http Request Error', err.error?.message || err.message || 'Server Error');
      }
      return throwError(() => err);
    })
  );
};
