import { Injectable } from '@angular/core';
import {BaseHttpService} from './base-http-service';
import {Observable} from 'rxjs';
import {LoginRequestDto, SignupRequestDto} from '../models/models';
import {environment} from '../../environments/environment';
import {HttpParams} from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AuthService extends BaseHttpService {
  private serviceBaseUrl = `${this.API_BASE_URL}/auth`;

  login = (input: LoginRequestDto): Observable<void> => {
    return this.httpClient.request<void>('POST', `${this.serviceBaseUrl}/login`,
      {
        ...this.defaultOptions,
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(input),
      })
  }

  register = (input: SignupRequestDto): Observable<void> => {
    return this.httpClient.request<void>('POST', `${this.serviceBaseUrl}/register`,
      {
        ...this.defaultOptions,
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(input),
      })
  }

  refresh = (): Observable<void> => {
    return this.httpClient.request<void>('POST', `${this.serviceBaseUrl}/${environment.refreshTokenPath}`,
      {
        ...this.defaultOptions,
      })
  }

  sendConfirmationLink = (email: string): Observable<void> => {
    return this.httpClient.request<void>('POST', `${this.serviceBaseUrl}/send-confirmation-link/${email}`,
      {
        ...this.defaultOptions,
      })
  }

  confirmEmail = (userId: string, token: string): Observable<void> => {
    const params = new HttpParams()
      .set(environment.emailConfirmation.userIdQueryString, userId)
      .set(environment.emailConfirmation.tokenQueryString, token);
    return this.httpClient.request<void>('POST',
      `${this.serviceBaseUrl}/confirm-email`,
      {
        ...this.defaultOptions,
        params
      })
  }

  sendPasswordResetLink = (email: string): Observable<void> => {
    return this.httpClient.request<void>('POST', `${this.serviceBaseUrl}/send-password-reset-link/${email}`,
      {
        ...this.defaultOptions,
      })
  }

  passwordReset = (userId: string, token: string, newPassword: string): Observable<void> => {
    const params = new HttpParams()
      .set(environment.passwordReset.userIdQueryString, userId)
      .set(environment.passwordReset.tokenQueryString, token);
    return this.httpClient.request<void>('POST',
      `${this.serviceBaseUrl}/password-reset`,
      {
        ...this.defaultOptions,
        body: { newPassword },
        params
      })
  }
}
