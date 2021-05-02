import { Injectable } from '@angular/core';
import {
  CanActivate, Router,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree
} from '@angular/router';
import { AuthService } from './auth.service';
import {UserSessionStoreService} from '../_helpers/user-session-store.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router, private userSessionStoreService: UserSessionStoreService) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): true|UrlTree {
    const url: string = state.url;

    return this.checkLogin(url);
  }

  checkLogin(url: string): true|UrlTree {

    // Store the attempted URL for redirecting
    this.authService.redirectUrl = url;

    if (!this.authService.refrashTokenValid()){
      return this.router.parseUrl('/account/login');
    }

    if (this.authService.isLoggedIn) {
      return true;
    }

    // const token = localStorage.getItem('accessToken');
    const token = this.userSessionStoreService.getAccessToken();
    if (token) {
      this.authService.isLoggedIn = true;
      return true;
    }

    // Redirect to the login page
    return this.router.parseUrl('/account/login');
  }
}
