import {inject, Injectable} from '@angular/core';
import {API_BASE_URL} from '../app.config';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class BaseHttpService {
  protected API_BASE_URL = `${inject<string>(API_BASE_URL)}/api`;
  constructor(protected httpClient: HttpClient) {
  }
}
