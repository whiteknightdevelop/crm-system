import {Injectable} from '@angular/core';

@Injectable()
export class UserSessionStoreService {
  constructor() {}

  public saveAccessToken(token: string, expires: Date): boolean {
    localStorage.setItem('accessToken', token);
    localStorage.setItem('accessTokenExpireDate', JSON.stringify(expires));
    return true;
  }

  public getAccessToken(): string {
    return localStorage.getItem('accessToken') as string;
  }

  public removeAccessToken(): boolean {
    localStorage.removeItem('accessToken');
    return true;
  }

  public saveRefreshToken(token: string, expires: Date): boolean {
    localStorage.setItem('refreshToken', token);
    localStorage.setItem('refreshTokenExpireDate', JSON.stringify(expires));
    return true;
  }

  public getRefreshToken(): string {
    return localStorage.getItem('refreshToken') as string;
  }

  public removeRefreshToken(): boolean {
    localStorage.removeItem('refreshToken');
    return true;
  }
}
