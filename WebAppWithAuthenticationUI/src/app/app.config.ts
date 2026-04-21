import {
  ApplicationConfig,
  importProvidersFrom,
  InjectionToken,
  provideBrowserGlobalErrorListeners
} from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { environment } from '../environments/environment';
import {TimeagoModule} from 'ngx-timeago';
import {provideSweetAlert2, SweetAlert2Module} from '@sweetalert2/ngx-sweetalert2';
import {provideHttpClient, withInterceptors} from '@angular/common/http';
import {errorHandlerInterceptor} from './interceptors/error-handler-interceptor';
import {refreshTokenInterceptor} from './interceptors/refresh-token-interceptor';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL', {
  providedIn: 'root',
  factory: () => environment.baseUrl
});

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes),
    importProvidersFrom(TimeagoModule.forRoot()),
    provideSweetAlert2({
      fireOnInit: false,
      dismissOnDestroy: true,
    }),
    provideHttpClient(
      withInterceptors([errorHandlerInterceptor, refreshTokenInterceptor])
    )
  ]
};
