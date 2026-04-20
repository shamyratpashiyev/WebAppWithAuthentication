import { Injectable } from '@angular/core';
import {BaseHttpService} from './base-http-service';
import {Observable} from 'rxjs';
import {LoginRequestDto} from '../models/models';

@Injectable({
  providedIn: 'root',
})
export class AuthService extends BaseHttpService {
  private serviceBaseUrl = `${this.API_BASE_URL}/auth`;

  login = (input: LoginRequestDto): Observable<string> => {
    return this.httpClient.request<string>('POST', `${this.serviceBaseUrl}/login`,
      {
        ...this.defaultOptions,
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(input),
      })
  }
}
