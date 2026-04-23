import {Component, signal} from '@angular/core';
import {AuthService} from '../../services/auth-service';
import {LoginRequestDto} from '../../models/models';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {Router, RouterLink} from '@angular/router';
import {NotificationService} from '../../services/notification-service';

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
    private router: Router,
    private notificationService: NotificationService) {
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

  onConfirmationLinkSend(){
    if (this.loginForm.value.email){
      this.authService.sendConfirmationLink(this.loginForm.value.email)
        .subscribe({
          complete: () => {
            this.notificationService.throwSuccess('Success','Confirmation link sent successfully, please check your email.')
          },
          error: (err) => {
            this.notificationService.throwError('Error','Error sending confirmation link. Please make sure you are registered.')
          }
        });
    }
  }

  onPasswordResetLinkSend(){
    if (this.loginForm.value.email){
      this.authService.sendPasswordResetLink(this.loginForm.value.email)
        .subscribe({
          complete: () => {
            this.notificationService.throwSuccess('Success','Password reset link sent successfully, please check your email.')
          },
          error: (err) => {
            this.notificationService.throwError('Error','Error sending password reset link. Please make sure you are registered.')
          }
        });
    }
  }
}
