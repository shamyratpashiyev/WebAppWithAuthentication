import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {API_BASE_URL} from '../app.config';
import {Observable} from 'rxjs';
import {UserDto} from '../models/user-dto';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private API_BASE_URL = inject<string>(API_BASE_URL);
  private serviceBaseUrl = `${this.API_BASE_URL}/User`;

  constructor(private httpClient: HttpClient) {}

  getAll = (): Observable<UserDto[]> => {
    return this.httpClient.request<UserDto[]>('GET', `${this.serviceBaseUrl}/GetAll`)
  }
}
