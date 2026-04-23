import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', loadComponent: () => import('./pages/users/users').then(m => m.Users) },
  { path: 'login', loadComponent: () => import('./pages/login-page/login-page').then(m => m.LoginPage) },
  { path: 'signup', loadComponent: () => import('./pages/signup-page/signup-page').then(m => m.SignupPage) },
  { path: `email-confirmation`, loadComponent: () => import('./pages/email-confirmation-page/email-confirmation-page').then(m => m.EmailConfirmationPage) },
  { path: 'password-reset', loadComponent: () => import('./pages/password-reset-page/password-reset-page').then(m => m.PasswordResetPage) }
];
