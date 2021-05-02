import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import {from, Observable, throwError} from 'rxjs';
import {AuthService} from './auth.service';
import {Router} from '@angular/router';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService, private router: Router) {}

  cachedRequest: Promise<any> | null;

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return from(this.addBearerToken(req, next));
  }

  private async addBearerToken(req: HttpRequest<any>, next: HttpHandler): Promise<HttpEvent<any>> {
    const token = await this.authService.getAuthToken();
    if (token) {
      req = req.clone({
        headers: req.headers.set('Authorization', `Bearer ${token}`)
      });
    }
    const result = next.handle(req).toPromise();

    return result.catch(async (error) => {
      if (error.status === 401) {
        const newToken = await this.cachedRequest;
        this.cachedRequest = null;
        const updatedRequest = req.clone({
          headers: req.headers.set('Authorization', `Bearer ${newToken}`)
        });
        return next.handle(updatedRequest).toPromise().then(data => {
          return data; 
        });
      }
      return throwError(error);
    }) as any;
  }
}
