import {Component, signal} from '@angular/core';
import {Router, RouterLink} from '@angular/router';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  ValidationErrors,
  ValidatorFn,
  Validators
} from '@angular/forms';
import {AuthService} from '../../services/auth-service';
import {SignupRequestDto} from '../../models/models';

@Component({
  selector: 'app-signup-page',
  imports: [
    ReactiveFormsModule,
    RouterLink
  ],
  templateUrl: './signup-page.html',
  styleUrl: './signup-page.scss',
})
export class SignupPage {
  isPasswordHidden = signal(true);
  constructor(private authService: AuthService,
              private router: Router) {
  }

  // Custom validator to check if passwords match
  passwordMatchValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
    const password = control.get('password');
    const confirmPassword = control.get('confirmPassword');

    return password && confirmPassword && password.value !== confirmPassword.value
      ? { passwordMismatch: true }
      : null;
  };

  signupForm = new FormGroup({
    name: new FormControl('', [Validators.required]),
    surname: new FormControl('', [Validators.required]),
    position: new FormControl(''),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
    confirmPassword: new FormControl('', [Validators.required])
  }, { validators: this.passwordMatchValidator });

  onSubmit() {
    if (this.signupForm.valid) {
      this.authService.register(this.signupForm.value as SignupRequestDto).subscribe({
        complete: async () => {
          await this.router.navigateByUrl('');
        }
      });
    }
  }

  togglePasswordHidden() {
    this.isPasswordHidden.update(x => !x);
  };
}
