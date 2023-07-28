import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class DefaultHeadersInterceptor implements HttpInterceptor {

  constructor() {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if (request.url.includes('http://localhost:8080/api/auth/login') || request.url.includes('http://localhost:8080/api/auth/register')){
      return next.handle(request);
    }

    const token = localStorage.getItem('jwt-token');
    const defaultHeaders = {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`,
    };

    const modifiedRequest = request.clone({
      setHeaders: defaultHeaders
    });

    return next.handle(modifiedRequest);
  }
}
