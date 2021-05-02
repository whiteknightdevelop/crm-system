import { Injectable } from '@angular/core';
import {Router} from '@angular/router';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {HandleError, HttpErrorHandler} from '../../error-handlers/http-error-handler.service';
import {User, UserEntity} from '../../models/user';
import {Observable} from 'rxjs';
import {RegisterPage} from '../../models/register-page';
import {catchError} from 'rxjs/operators';
import {RegisterResponse} from '../../models/register-response';

@Injectable({
  providedIn: 'root',
})
export class RegisterService {

  constructor(private router: Router,
              private http: HttpClient,
              httpErrorHandler: HttpErrorHandler) {
    this.handleError = httpErrorHandler.createHandleError('RegisterService');
    this.isLoggedIn = false;
    this.redirectUrl = '';
    this.user = new UserEntity();
  }

  registerUrl = 'api/account/register/';
  private readonly handleError: HandleError;
  isLoggedIn: boolean;
  redirectUrl: string;
  user: User;

  getRegisterPageData(): Observable<HttpErrorResponse| RegisterPage> {
    return this.http.get<RegisterPage>(this.registerUrl).pipe(
      catchError(this.handleError<RegisterPage>('getRegisterPageData')));
  }

  register(user: User): Observable<HttpErrorResponse| RegisterResponse> {
    return this.http.post<RegisterResponse>(this.registerUrl, user).pipe(
      catchError(this.handleError<RegisterResponse>('register'))
    );
  }
}
