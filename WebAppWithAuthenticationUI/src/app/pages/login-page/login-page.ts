import {Component, signal} from '@angular/core';
import {AuthService} from '../../services/auth-service';
import {LoginRequestDto} from '../../models/models';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {Router, RouterLink} from '@angular/router';

@Component({
  selector: 'app-login-page',
  imports: [
    ReactiveFormsModule,
    RouterLink
  ],
  templateUrl: './login-page.html',
  styleUrl: './login-page.scss',
})
export class LoginPage {
  isPasswordHidden = signal<boolean>(true);

  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
    rememberMe: new FormControl(false)
  })

  constructor(
    private authService: AuthService,
    private router: Router) {
  }

  togglePasswordHidden() {
    this.isPasswordHidden.update(x => !x);
  };

  onSubmit(e: any): void {
    e.preventDefault();
    this.authService.login(this.loginForm.value as LoginRequestDto)
      .subscribe({
        complete: async () => {
          await this.router.navigateByUrl('');
        }
      });
  }
}
