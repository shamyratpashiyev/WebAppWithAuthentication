import {HttpInterceptorFn} from '@angular/common/http';
import {catchError, throwError} from 'rxjs';
import Swal from 'sweetalert2';
import {inject} from '@angular/core';
import {Router} from '@angular/router';

export const errorHandlerInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);

  return next(req).pipe(
    catchError((err, caught) => {
      if(err.status == 401) {
        router.navigateByUrl('/login').then();
      }
      else {
        Swal.fire({
          title: 'Http Request Error',
          text: err.error?.message || err.message || 'Server Error',
          icon: 'error',
          confirmButtonColor: '#d33' // You can match your app's theme here
        }).then();
      }
      return throwError(() => err);
    })
  );
};
