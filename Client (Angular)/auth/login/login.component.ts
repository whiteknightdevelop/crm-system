import { Component, OnInit } from '@angular/core';
import {AuthService} from '../auth.service';
import {Router} from '@angular/router';
import {FormBuilder, Validators} from '@angular/forms';
import {ALPHANUMERIC_REGEX} from '../../models/RegexPatterns';
import {MessageService, PrimeNGConfig} from 'primeng/api';
import {UserEntity} from '../../models/user';
import {HttpErrorHandler} from '../../error-handlers/http-error-handler.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  constructor(private primengConfig: PrimeNGConfig,
              public authService: AuthService,
              public router: Router,
              private messageService: MessageService,
              private httpErrorHandler: HttpErrorHandler,
              private fb: FormBuilder) {
    this.pageIsLoaded = false;
    this.message = '';
    this.setMessage();
  }

  pageIsLoaded: boolean;
  message: string;
  loginForm = this.fb.group({
    userName: ['', Validators.compose([Validators.required, Validators.pattern(ALPHANUMERIC_REGEX)])],
    password: ['', Validators.compose([Validators.required, Validators.pattern(ALPHANUMERIC_REGEX)])],
  });
  get userName(): any { return this.loginForm.get('userName'); }
  get password(): any { return this.loginForm.get('password'); }

  ngOnInit(): void {
    this.primengConfig.ripple = true;
    this.pageIsLoaded = true;
  }

  setMessage(): void {
    this.message = 'Logged ' + (this.authService.isLoggedIn ? 'in' : 'out');
  }

  onSubmit(): void {
    const user = new UserEntity();
    user.setData(this.loginForm);
    this.authService.login(user).subscribe(data => {
      this.setMessage();
      if (this.authService.isLoggedIn) {
        const redirectUrl = this.authService.redirectUrl;
        this.router.navigateByUrl(redirectUrl);
      }
    }, error => {
      this.loginForm.reset();
      this.messageService.add({severity: 'error', summary: '', detail: this.httpErrorHandler.getErrorMessage(error)});
    });
  }
}
