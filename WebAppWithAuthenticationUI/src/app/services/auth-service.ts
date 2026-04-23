import { Injectable } from '@angular/core';
import {BaseHttpService} from './base-http-service';
import {Observable} from 'rxjs';
import {LoginRequestDto, SignupRequestDto} from '../models/models';
import {environment} from '../../environments/environment';

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
    return this.httpClient.request<void>('POST',
      `${this.serviceBaseUrl}/confirm-email?${environment.emailConfirmation.userIdQueryString}=${userId}&${environment.emailConfirmation.tokenQueryString}=${token}`,
      {
        ...this.defaultOptions,
      })
  }
}
