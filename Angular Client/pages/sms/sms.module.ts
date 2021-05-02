import { NgModule} from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import {AppointmentsComponent} from './appointments/appointments.component';
import {ProgressBarModule} from 'primeng/progressbar';
import {MessagesModule} from 'primeng/messages';
import {ButtonModule} from 'primeng/button';
import {CardModule} from 'primeng/card';
import {PipesModule} from '../../pipes/pipes.module';
import {TableModule} from 'primeng/table';
import {EmptyListErrorModule} from '../../custom-components/empty-list-error/empty-list-error.module';
import {SmsService} from './sms.service';
import {ToastModule} from 'primeng/toast';
import {DialogModule} from 'primeng/dialog';
import { PreventiveReminderComponent } from './preventive-reminder/preventive-reminder.component';
import {TooltipModule} from 'primeng/tooltip';
import {ConfirmDialogModule} from 'primeng/confirmdialog';


@NgModule({
  imports: [
    CommonModule,
    PipesModule,
    ButtonModule,
    CardModule,
    ReactiveFormsModule,
    FormsModule,
    TableModule,
    EmptyListErrorModule,
    ProgressBarModule,
    MessagesModule,
    ToastModule,
    DialogModule,
    TooltipModule,
    ConfirmDialogModule,
  ],
  declarations: [
    AppointmentsComponent,
    PreventiveReminderComponent
  ],
  providers: [
    SmsService
  ],
})
export class SmsModule {}
