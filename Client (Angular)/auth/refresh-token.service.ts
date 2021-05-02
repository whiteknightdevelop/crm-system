import { Injectable } from '@angular/core';
import {Observable, of, Subject} from 'rxjs';
import {RefreshTokenResult} from '../models/refresh-token-result';

@Injectable({
  providedIn: 'root',
})
export class RefreshTokenService {
  constructor(private _tokenAuthService: TokenAuthServiceProxy) { }

  tryAuthWithRefreshToken(): Observable<boolean> {
    const refreshToken = this.getRefreshToken();
    if (!refreshToken) {
      return of(false);
    }
    const refreshTokenObservable = new Subject<boolean>();
    this._tokenAuthService.refreshAccessToken(refreshToken)
      .subscribe(
        (tokenResult: RefreshTokenResult) => {
          if (tokenResult && tokenResult.isSucceed) {
            this.storeAccessToken(tokenResult.accessToken, tokenResult.tokenExpireDate);
            refreshTokenObservable.next(true);
          } else {
            refreshTokenObservable.next(false);
          }
        },
        (error: any) => {
          refreshTokenObservable.next(false);
        }
      );
    return refreshTokenObservable;
  }

  getRefreshToken(): string {
    const token = localStorage.getItem('refreshToken');
    return token as string;
  }

  storeAccessToken(token: string, expireDate: Date): void {
    localStorage.setItem('accessToken', token);
    localStorage.setItem('accessTokenExpireDate', expireDate.toLocaleString());
  }
}
