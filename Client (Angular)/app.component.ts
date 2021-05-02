import {Component, HostListener} from '@angular/core';
import {Router} from '@angular/router';
import {Angulartics2GoogleTagManager} from 'angulartics2/gtm';
import {AuthService} from './auth/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  title = 'Petadmin';

  constructor(public angulartics2GoogleTagManager: Angulartics2GoogleTagManager,
              private authService: AuthService,
              private router: Router) {
    angulartics2GoogleTagManager.startTracking();
  }

  @HostListener('click', ['$event.target'])
  handleMouseClick(event: MouseEvent): void {
    this.validateToken();
  }

  @HostListener('window:keydown', ['$event'])
  handleKeyDown(event: KeyboardEvent): void {
    this.validateToken();
  }

  validateToken(): void {
    if (!this.authService.refrashTokenValid()){
      this.router.parseUrl('/account/login');
    }
  }
}
