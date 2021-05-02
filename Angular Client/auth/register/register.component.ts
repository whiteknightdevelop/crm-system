import { Component, OnInit } from '@angular/core';
import {MessageService, PrimeNGConfig} from 'primeng/api';
import {ActivatedRoute, Router} from '@angular/router';
import {HttpErrorHandler} from '../../error-handlers/http-error-handler.service';
import {FormBuilder, Validators} from '@angular/forms';
import {ALPHANUMERIC_REGEX} from '../../models/RegexPatterns';
import {UserEntity} from '../../models/user';
import {Gender} from '../../models/gender';
import {RegisterPage, RegisterPageEntity} from '../../models/register-page';
import {RegisterService} from './register.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private primengConfig: PrimeNGConfig,
              public registerService: RegisterService,
              public router: Router,
              private route: ActivatedRoute,
              private messageService: MessageService,
              private httpErrorHandler: HttpErrorHandler,
              private fb: FormBuilder) {
    this.pageIsLoaded = false;
    this.message = '';
    this.registerPage = new RegisterPageEntity();
    this.gendersList = [];
  }

  pageIsLoaded: boolean;
  message: string;
  registerPage: RegisterPage;
  gendersList: { gender: Gender }[];
  registerForm = this.fb.group({
    userName: ['', Validators.compose([Validators.required, Validators.pattern(ALPHANUMERIC_REGEX)])],
    password: ['', Validators.compose([Validators.required, Validators.pattern(ALPHANUMERIC_REGEX)])],
    gender: ['', Validators.compose([Validators.required])],
  });
  get userName(): any { return this.registerForm.get('userName'); }
  get password(): any { return this.registerForm.get('password'); }
  get gender(): any { return this.registerForm.get('gender'); }

  ngOnInit(): void {
    this.primengConfig.ripple = true;
    if (localStorage.getItem('token') != null) {
      this.setPageData();
      this.pageIsLoaded = true;
    }
  }

  setPageData(): void {
    this.registerService.getRegisterPageData().subscribe((res) => {
      if (res) {
        this.gendersList = (res as RegisterPage).gendersList.map(item => ({gender: item}));
      }
    }, error => {
      this.messageService.add({severity: 'error', summary: '', detail: this.httpErrorHandler.getErrorMessage(error)});
    });
  }

  onSubmit(): void {
    const user = new UserEntity();
    user.setData(this.registerForm);
    this.registerService.register(user).subscribe((res) => {
      if (res) {
        this.router.navigateByUrl('/account/login');
      }
    }, error => {
      this.registerForm.reset();
      this.messageService.add({severity: 'error', summary: '', detail: this.httpErrorHandler.getErrorMessage(error)});
    });
  }
}
