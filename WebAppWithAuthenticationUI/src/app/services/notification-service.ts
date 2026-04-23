import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  throwError(title: string, message: string) {
    Swal.fire({
      title: title,
      text: message,
      icon: 'error',
      confirmButtonColor: '#d33'
    }).then();
  }

  throwSuccess(title: string, message: string) {
    Swal.fire({
      title: title,
      text: message,
      icon: 'success',
      confirmButtonColor: '#0d6efd'
    }).then();
  }
}
