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
      let errorMessage = 'An unexpected error occurred';
      let errorTitle = 'Http Request Error';
      switch (err.status) {
        case 400:
          errorTitle = 'Invalid Request';
          errorMessage = err.error?.message || err.error?.title || 'Please check the data you entered.';
          break;

        case 401:
          router.navigateByUrl('/login').then();
          errorTitle = 'Session Expired';
          errorMessage = err.error.message || 'Please log in again to continue.';
          break;

        case 403:
          errorTitle = 'Access Denied';
          errorMessage = 'You do not have permission to perform this action.';
          break;

        case 404:
          errorTitle = 'Not Found';
          errorMessage = 'The requested resource could not be found.';
          break;

        case 500:
          errorTitle = 'Server Error';
          errorMessage = 'Something went wrong on our server. We are working to fix it.';
          break;

        case 0:
          errorTitle = 'Connection Error';
          errorMessage = 'Cannot reach the server. Please check your internet connection.';
          break;

        default:
          errorMessage = err.error.message || 'Server Error';
          break;
      }

      notificationService.throwError(errorTitle, errorMessage);

      return throwError(() => err);
    })
  );
};
