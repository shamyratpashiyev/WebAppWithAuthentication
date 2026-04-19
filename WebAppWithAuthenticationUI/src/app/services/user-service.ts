import {inject, Injectable, Signal} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {API_BASE_URL} from '../app.config';
import {Observable} from 'rxjs';
import {UserDto} from '../models/models';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private API_BASE_URL = inject<string>(API_BASE_URL);
  private serviceBaseUrl = `${this.API_BASE_URL}/api/user`;

  constructor(private httpClient: HttpClient) {}

  getList = (filter?: string): Observable<UserDto[]> => {
    return this.httpClient.request<UserDto[]>('GET', `${this.serviceBaseUrl}${(filter ? '?filter=' + filter : '')}`)
  }

  blockSelected = (idList: number[]): Observable<void> => {
    return this.httpClient.request<void>('PUT', `${this.serviceBaseUrl}/block-selected`, {
      body: JSON.stringify(idList),
      headers: { 'Content-Type': 'application/json' },
    })
  }

  unblockSelected = (idList: number[]): Observable<void> => {
    return this.httpClient.request<void>('PUT', `${this.serviceBaseUrl}/unblock-selected`, {
      body: JSON.stringify(idList),
      headers: { 'Content-Type': 'application/json' },
    })
  }

  deleteSelected = (idList: number[]): Observable<void> => {
    return this.httpClient.request<void>('DELETE', `${this.serviceBaseUrl}/delete-selected`, {
      body: JSON.stringify(idList),
      headers: { 'Content-Type': 'application/json' },
    })
  }

  deleteUnverified = (): Observable<void> => {
    return this.httpClient.request<void>('DELETE', `${this.serviceBaseUrl}/delete-unverified`)
  }
}
