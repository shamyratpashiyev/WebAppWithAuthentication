import {HttpInterceptorFn} from '@angular/common/http';
import {catchError, throwError} from 'rxjs';
import Swal from 'sweetalert2';

export const errorHandlerInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    catchError((err, caught) => {
      Swal.fire({
        title: 'Http Request Error',
        text: err.error?.message || err.message || 'Server Error',
        icon: 'error',
        confirmButtonColor: '#d33' // You can match your app's theme here
      }).then();
      return throwError(() => err);
    })
  );
};
