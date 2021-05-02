import { NgModule} from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { ProgressBarModule } from 'primeng/progressbar';
import { MessagesModule } from 'primeng/messages';
import { ToastModule } from 'primeng/toast';
import {ConfirmationService, MessageService} from 'primeng/api';
import {CardModule} from 'primeng/card';
import {ButtonModule} from 'primeng/button';
import {RippleModule} from 'primeng/ripple';
import {AvatarModule} from 'primeng/avatar';
import {AvatarGroupModule} from 'primeng/avatargroup';
import { DropdownModule } from 'primeng/dropdown';
import { LoginComponent } from './login/login.component';
import { AuthRoutingModule } from './auth-routing.module';
import { RegisterComponent } from './register/register.component';
import {HttpErrorHandler} from '../error-handlers/http-error-handler.service';
import {ErrorMessageService} from '../error-handlers/error-message.service';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AuthRoutingModule,
    ProgressBarModule,
    ToastModule,
    MessagesModule,
    CardModule,
    ButtonModule,
    RippleModule,
    AvatarModule,
    AvatarGroupModule,
    DropdownModule,
  ],
  declarations: [
    LoginComponent,
    RegisterComponent
  ],
  providers: [
    HttpErrorHandler,
    ErrorMessageService,
    MessageService,
    ConfirmationService,
  ]
})
export class AuthModule {}
