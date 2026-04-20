import {Component, signal} from '@angular/core';
import {AuthService} from '../../services/auth-service';
import {LoginRequestDto} from '../../models/models';
import {FormControl, FormGroup, ReactiveFormsModule} from '@angular/forms';

@Component({
  selector: 'app-login-page',
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './login-page.html',
  styleUrl: './login-page.scss',
})
export class LoginPage {
  isPasswordHidden = signal<boolean>(true);

  loginForm = new FormGroup({
    email: new FormControl(''),
    password: new FormControl(''),
    rememberMe: new FormControl(false)
  })

  constructor(private authService: AuthService) {
  }

  togglePasswordHidden() {
    this.isPasswordHidden.update(x => !x);
  };

  login(e: any): void {
    e.preventDefault();
    this.authService.login(this.loginForm.value as LoginRequestDto).subscribe();
  }
}
