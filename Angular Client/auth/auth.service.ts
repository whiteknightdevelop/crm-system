import { Injectable } from '@angular/core';
import {BehaviorSubject, Observable} from 'rxjs';
import {tap, catchError} from 'rxjs/operators';
import { EMPTY } from 'rxjs';
import {Router} from '@angular/router';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {HandleError, HttpErrorHandler} from '../error-handlers/http-error-handler.service';
import {User, UserEntity} from '../models/user';
import {RefreshAccessTokenResponse} from '../models/refresh-access-token-response';
import {LoginResponse} from '../models/login-response';

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  constructor(private router: Router,
              private http: HttpClient,
              httpErrorHandler: HttpErrorHandler) {
    this.handleError = httpErrorHandler.createHandleError('AuthService');
    this.isLoggedIn = false;
    this.redirectUrl = '';
    this.userSubject = new BehaviorSubject<User>(new UserEntity());
    this.user = this.userSubject.asObservable();
  }

  loginUrl = 'api/account/login/';
  refreshTokenUrl = 'api/account/refresh-token/';
  revokeTokenUrl = 'api/account/revoke-token/';
  private readonly handleError: HandleError;
  isLoggedIn: boolean;
  redirectUrl: string;
  private userSubject: BehaviorSubject<User>;
  public user: Observable<User>;

  initializeUser(): Promise<any> {
    return new Promise((resolve) => {
      this.userSubject.next(JSON.parse(localStorage.getItem('currentUser') as string));
      resolve(true);
    });
  }

  getUser(): User {
    return this.userSubject.value;
  }

  login(user: User): Observable<HttpErrorResponse| LoginResponse> {
    return this.http.post<LoginResponse>(this.loginUrl, user).pipe(
      catchError(this.handleError<LoginResponse>('login')),
      tap(response => {
        localStorage.setItem('accessToken', response.accessToken.token);
        localStorage.setItem('accessTokenExpireDate', JSON.stringify(response.accessToken.expires));
        localStorage.setItem('refreshToken', response.refreshToken.token);
        localStorage.setItem('refreshTokenExpireDate', JSON.stringify(response.refreshToken.expires));
        localStorage.setItem('currentUser', JSON.stringify(response.user));
        this.userSubject.next(response.user);
        this.isLoggedIn = true;
      })
    );
  }

  logout(): void {
    const refreshToken = localStorage.getItem('refreshToken');
    const accessToken = localStorage.getItem('accessToken');
    this.http.post<boolean>(this.revokeTokenUrl, {refreshToken, accessToken}).pipe(
      catchError(this.handleError<boolean>('logout')),
    ).subscribe((ans) => {
      if (ans) {
        this.isLoggedIn = false;
        localStorage.clear();
        this.userSubject.next(new UserEntity());
        this.router.navigate(['/account/login']);
      }
    });
  }

  getAuthToken(): Promise<string | null> {
    const refreshTokenExpireDate = new Date(JSON.parse(localStorage.getItem('refreshTokenExpireDate') as string));
    if (refreshTokenExpireDate >= new Date()) {
      return Promise.resolve(
        localStorage.getItem('accessToken')
      );
    }
    return Promise.resolve(null);
  }

  refreshAccessToken(): Observable<HttpErrorResponse| RefreshAccessTokenResponse> {
    const refreshToken = localStorage.getItem('refreshToken');
    const accessToken = localStorage.getItem('accessToken');
    if (refreshToken === null || accessToken === null) {
      this.isLoggedIn = false;
      this.router.navigate(['/account/login']);
      return EMPTY;
    }
    return this.http.post<RefreshAccessTokenResponse>(this.refreshTokenUrl, {refreshToken, accessToken}).pipe(
      catchError(this.handleError<RefreshAccessTokenResponse>('refreshAccessToken')),
      tap(response => {
        localStorage.setItem('accessToken', response.accessToken.token);
        localStorage.setItem('accessTokenExpireDate', JSON.stringify(response.accessToken.expires));
        this.userSubject.next(response.user);
        this.isLoggedIn = true;
      })
    );
  }

  accessTokenValid(): boolean {
    const accessTokenExpireDate = new Date(JSON.parse(localStorage.getItem('accessTokenExpireDate') as string));
    const accessTokenExpireDateLocal = new Date(accessTokenExpireDate.toLocaleString());
    if (accessTokenExpireDateLocal >= new Date()) {
      this.isLoggedIn = false;
      return false;
    }
    return true;
  }

  refrashTokenValid(): boolean {
    const refreshTokenExpireDate = new Date(JSON.parse(localStorage.getItem('refreshTokenExpireDate') as string));
    if (refreshTokenExpireDate < new Date()) {
      this.logout();
      return false;
    }
    return true;
  }
}
