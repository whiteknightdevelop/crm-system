import {AuthService} from '../auth/auth.service';
import {UserSessionStoreService as StoreService} from './user-session-store.service';
import {Injectable} from '@angular/core';
import {EMPTY, Observable, Subject, throwError} from 'rxjs';
import {catchError, switchMap} from 'rxjs/operators';
import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';

@Injectable()
export class SessionRecoveryInterceptor implements HttpInterceptor {
  constructor(
    private readonly store: StoreService,
    private readonly authService: AuthService,
  ) {}

  private refreshSubject: Subject<any> = new Subject<any>();

  private ifTokenExpired(): Subject<any> {
    this.refreshSubject.subscribe({
      complete: () => {
        this.refreshSubject = new Subject<any>();
      }
    });
    if (this.refreshSubject.observers.length === 1) {
      this.authService.refreshAccessToken().subscribe(this.refreshSubject);
    }
    return this.refreshSubject;
  }

  private checkTokenExpiryErr(error: HttpErrorResponse): boolean {
    return (
      error.status === 401
    );
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (req.url.endsWith('/refresh-token/')) {
      return next.handle(req).pipe(
        catchError((error, caught) => {
          if (error instanceof HttpErrorResponse) {
            if (this.checkTokenExpiryErr(error)) {
              this.authService.logout();
              return EMPTY;
            }
          }
          return caught;
        })
      );
    } else if (req.url.endsWith('/revoke-token/')) {
      return next.handle(req);
    } else {
      if (this.authService.isLoggedIn) {
        req = this.updateHeader(req);
      }
      return next.handle(req).pipe(
        catchError((error, caught) => {
          if (error instanceof HttpErrorResponse) {
            if (this.checkTokenExpiryErr(error)) {
              return this.ifTokenExpired().pipe(
                switchMap(() => {
                  return next.handle(this.updateHeader(req));
                })
              );
            } else {
              return throwError(error);
            }
          }
          return caught;
        })
      );
    }
  }

  updateHeader(req: HttpRequest<any>): HttpRequest<any> {
    const token = this.store.getAccessToken();
    req = req.clone({
      headers: req.headers.set('Authorization', `Bearer ${token}`)
    });
    return req;
  }
}
