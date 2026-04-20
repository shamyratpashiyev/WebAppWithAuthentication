import {inject, Injectable, Signal} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {UserDto} from '../models/models';
import {BaseHttpService} from './base-http-service';

@Injectable({
  providedIn: 'root',
})
export class UserService extends BaseHttpService {
  private serviceBaseUrl = `${this.API_BASE_URL}/user`;

  getList = (filter?: string): Observable<UserDto[]> => {
    return this.httpClient.request<UserDto[]>('GET', `${this.serviceBaseUrl}${(filter ? '?filter=' + filter : '')}`, this.defaultOptions);
  }

  blockSelected = (idList: number[]): Observable<void> => {
    return this.httpClient.request<void>('PUT', `${this.serviceBaseUrl}/block-selected`, {
      ...this.defaultOptions,
      body: JSON.stringify(idList),
      headers: { 'Content-Type': 'application/json' },
    });
  }

  unblockSelected = (idList: number[]): Observable<void> => {
    return this.httpClient.request<void>('PUT', `${this.serviceBaseUrl}/unblock-selected`, {
      ...this.defaultOptions,
      body: JSON.stringify(idList),
      headers: { 'Content-Type': 'application/json' },
    });
  }

  deleteSelected = (idList: number[]): Observable<void> => {
    return this.httpClient.request<void>('DELETE', `${this.serviceBaseUrl}/delete-selected`, {
      ...this.defaultOptions,
      body: JSON.stringify(idList),
      headers: { 'Content-Type': 'application/json' },
    });
  }

  deleteUnverified = (): Observable<void> => {
    return this.httpClient.request<void>('DELETE', `${this.serviceBaseUrl}/delete-unverified`, this.defaultOptions);
  }
}
