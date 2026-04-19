import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', loadComponent: () => import('./pages/users/users').then(m => m.Users) },
  { path: 'login', loadComponent: () => import('./pages/login-page/login-page').then(m => m.LoginPage) }
];
