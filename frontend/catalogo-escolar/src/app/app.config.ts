import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { Title } from '@angular/platform-browser';
import { jwtInterceptor } from './_helpers/jwt.interceptor';
import { unauthorizedInterceptor } from './_helpers/unauthorized.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [provideZoneChangeDetection({ eventCoalescing: true }), provideRouter(routes), 
    provideHttpClient(withInterceptors([jwtInterceptor, unauthorizedInterceptor])),
   Title]
};
