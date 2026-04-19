import {Component, signal} from '@angular/core';
import {AuthService} from '../../services/auth-service';

@Component({
  selector: 'app-login-page',
  imports: [],
  templateUrl: './login-page.html',
  styleUrl: './login-page.scss',
})
export class LoginPage {
  isPasswordHidden = signal<boolean>(true);
  email = signal<string>('');
  password = signal<string>('');
  rememberMe = signal<boolean>(false);

  constructor(private authService: AuthService) {
  }

  togglePasswordHidden() {
    this.isPasswordHidden.update(x => !x);
  };

  login(): void {
    this.authService.login(this.email(), this.password(), this.rememberMe());
  }

  onRememberMeToggle(): void {
    this.rememberMe.update(x => !x);
  }

  onEmailChange(e: any): void {
    this.email.set(e.target.value);
  }

  onPasswordChange(e: any): void {
    this.password.set(e.target.value);
  }
}
