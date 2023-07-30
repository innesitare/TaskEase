import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class DefaultHeadersInterceptor implements HttpInterceptor {
  private readonly excludedUrls = [
    'http://localhost:8080/api/auth/login',
    'http://localhost:8080/api/auth/register'
  ];

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if (this.isExcludedUrl(request.url)) {
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

  private isExcludedUrl(url: string): boolean {
    return this.excludedUrls.some(excludedUrl => url.includes(excludedUrl));
  }
}
