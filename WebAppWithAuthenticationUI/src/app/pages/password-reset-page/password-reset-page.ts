import {booleanAttribute, Component, signal} from '@angular/core';
import {NgClass} from '@angular/common';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  ValidationErrors,
  ValidatorFn,
  Validators
} from '@angular/forms';
import {ActivatedRoute, Router, RouterLink} from '@angular/router';
import {environment} from '../../../environments/environment';
import {AuthService} from '../../services/auth-service';
import {NotificationService} from '../../services/notification-service';
import {finalize} from 'rxjs';

@Component({
  selector: 'app-password-reset-page',
  imports: [
    NgClass,
    RouterLink,
    ReactiveFormsModule
  ],
  templateUrl: './password-reset-page.html',
  styleUrl: './password-reset-page.scss',
})
export class PasswordResetPage {
  isPasswordHidden = signal<boolean>(true);
  isLoading = signal<boolean>(false);

  constructor(private activatedRoute: ActivatedRoute,
              private authService: AuthService,
              private notificationService: NotificationService,
              private router: Router) {
  }

  passwordMatchValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
    const password = control.get('password');
    const confirmPassword = control.get('confirmPassword');

    return password && confirmPassword && password.value !== confirmPassword.value
      ? { passwordMismatch: true }
      : null;
  };

  resetForm = new FormGroup({
    password: new FormControl('', [Validators.required]),
    confirmPassword: new FormControl('', [Validators.required]),
  }, { validators: this.passwordMatchValidator });

  togglePasswordHidden() {
    this.isPasswordHidden.update(x => !x);
  };

  onSubmit() {
    this.isLoading.set(true);
    const userId = this.activatedRoute.snapshot.queryParams[environment.passwordReset.userIdQueryString];
    const token = this.activatedRoute.snapshot.queryParams[environment.passwordReset.tokenQueryString];

    if(this.resetForm.valid && this.resetForm.value.password){
      this.authService.passwordReset(userId, token, this.resetForm.value.password)
        .pipe(finalize(() => {
          this.isLoading.set(false);
        }))
        .subscribe({
          complete: async () => {
            await this.router.navigateByUrl('/login');
            this.notificationService.throwSuccess('Success', 'Password has been reset successfully. You can now log in.');
          },
        })
    }
  }
}
