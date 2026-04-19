import { Injectable } from '@angular/core';
import {BaseHttpService} from './base-http-service';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService extends BaseHttpService {
  private serviceBaseUrl = `${this.API_BASE_URL}/auth`;

  login = (email: string, password: string, rememberMe: boolean): Observable<string> => {
    return this.httpClient.request<string>('POST', `${this.serviceBaseUrl}/login`,
      {
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email, password, rememberMe }),
      })
  }
}
