import { Component, OnInit } from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {ALPHABETIC_REGEX, DATE_REGEX, PHONENUMBER_REGEX} from '../../../models/RegexPatterns';
import {HandleError, HttpErrorHandler} from '../../../error-handlers/http-error-handler.service';
import {PrintService} from '../../print/print.service';
import {ConfirmationService, MessageService} from 'primeng/api';
import {Router} from '@angular/router';
import {formatDate} from '@angular/common';
import {SmsAppointment, SmsAppointmentEntity} from '../../../models/sms-appointment';
import {SmsService} from '../sms.service';
import {SmsPullNotification} from '../../../models/sms-pull-notification';
import {SmsStatus} from '../../../enums/sms-status';

@Component({
  selector: 'app-appointments',
  templateUrl: './appointments.component.html',
  styleUrls: ['./appointments.component.css']
})
export class AppointmentsComponent implements OnInit {

  private readonly handleError: HandleError;
  constructor(private printService: PrintService, private fb: FormBuilder,
              private smsService: SmsService,
              private messageService: MessageService,
              private httpErrorHandler: HttpErrorHandler,
              private confirmationService: ConfirmationService,
              private router: Router) {
    this.handleError = httpErrorHandler.createHandleError('AppointmentsComponent');
    this.appointmentsList = [];
    this.showTable = false;
    this.showProgress = false;
    this.sendMessagesBtnDisabled = false;
    this.showInforuWebsiteUrl = false;
    this.pullNotifications = [];
    this.sentStatusColor = '';
    this.displayDialog = false;
    this.progressBarValue = 0;
  }

  appointmentForm = this.fb.group({
    date: [formatDate(new Date(), 'yyyy-MM-dd', 'he'),
      Validators.compose([Validators.required, Validators.pattern(DATE_REGEX)])],
    time: [formatDate(new Date(), 'HH:mm', 'he'), Validators.compose([Validators.required])],
    name: ['', Validators.compose([
      Validators.required,
      Validators.maxLength(45),
      Validators.pattern(ALPHABETIC_REGEX)])
    ],
    recipient: ['', Validators.compose([
      Validators.required,
      Validators.maxLength(45),
      Validators.pattern(PHONENUMBER_REGEX)])
    ],
  });

  get date(): any { return this.appointmentForm.get('date'); }
  get time(): any { return this.appointmentForm.get('time'); }
  get name(): any { return this.appointmentForm.get('name'); }
  get recipient(): any { return this.appointmentForm.get('recipient'); }
  appointmentsList: SmsAppointment[];
  showTable: boolean;
  showProgress: boolean;
  sendMessagesBtnDisabled: boolean;
  showInforuWebsiteUrl: boolean;
  pullNotifications: SmsPullNotification[];
  sentStatusColor: string;
  displayDialog: boolean;
  progressBarValue: number;

  ngOnInit(): void {
    this.appointmentForm.patchValue({
      date: formatDate(this.getTomorrowDate(), 'yyyy-MM-dd', 'he'),
      time: formatDate(this.setInitialTime(), 'HH:mm', 'he'),
    });
  }

  getTomorrowDate(): Date {
    const year = new Date().getFullYear();
    const month = new Date().getMonth() + 1;
    const lastDayOfTheMonth = new Date(year, month, 0).getDate();
    const today = new Date().getDate();

    if (today === lastDayOfTheMonth) {
      return new Date(year + '-' + (month + 1) + '-' + 1);
    } else {
      return new Date(year + '-' + month + '-' + (today + 1));
    }
  }

  setInitialTime(): Date {
    const hours = parseInt( '9', 10);
    const minutes = parseInt( '0', 10);
    const seconds = parseInt( '0', 10);
    const time = new Date();
    time.setHours(hours);
    time.setMinutes(minutes);
    time.setSeconds(seconds);

    return time;
  }

  addItemToList(): void {
    if (this.appointmentForm.valid) {
      const appointment = new SmsAppointmentEntity();
      appointment.setData(this.appointmentForm);
      appointment.id = (this.appointmentsList.length).toString();

      this.appointmentsList.push(appointment);
      this.appointmentForm.patchValue({
        name: '',
        recipient: '',
      });
      this.showTable = true;
    }
  }

  sendMessages(): void {
    this.confirmationService.confirm({
      header: 'אישור שליחת הודעות SMS',
      message: 'האם הינך בטוח כי ברצונך לשלוח הודעות SMS?',
      icon: 'pi pi-exclamation-triangle',
      acceptLabel: 'שלח',
      rejectLabel: 'בטל',
      acceptIcon: 'pi pi-send',
      acceptButtonStyleClass: 'confirmDialogSaveButton',
      accept: () => {
        this.displayDialog = true;
        this.startProgressBar();
        this.showInforuWebsiteUrl = true;
        this.sendMessagesBtnDisabled = true;
        this.appointmentsList.forEach(item => item.sentStatus = SmsStatus.Sending);
        this.smsService.sendSmsAppointments(this.appointmentsList).subscribe(response => {
          if (response) {
            this.pullNotifications = response;
            this.markMessageSentStatus();
            this.displayDialog = false;
            this.messageService.add({key: 'sendSmsResponseMessage', severity: 'success', summary: 'ההודעות נשלחו!', life: 1000});
          }else {
            this.messageService.add({
              key: 'sendSmsResponseMessage', severity: 'error', summary: 'תקלה! יש לבדוק דוח הודעות באתר !', life: 3000
            });
          }
        }, error => {
          this.displayDialog = false;
          this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
        });
      }
    });
  }

  deleteItemFromList(item: any): void {
    const index = this.appointmentsList.indexOf(item);
    if (index > -1) {
      this.appointmentsList.splice(index, 1);
    }

    if (this.appointmentsList.length === 0) {
      this.showTable = false;
    }
  }

  markMessageSentStatus(): void {
    this.pullNotifications.forEach(element => {
      this.appointmentsList[element.customerMessageId].sentStatus = element.status;
    });
  }

  public textColorbyStatus(item: SmsAppointment): string {
    switch (item.sentStatus) {
      case SmsStatus.Waiting: {
        return '';
        break;
      }
      case SmsStatus.Sending: {
        return '';
        break;
      }
      case SmsStatus.Delivered: {
        return '#00804a';
        break;
      }
      case SmsStatus.NotDelivered: {
        return '#f44336';
        break;
      }
      case SmsStatus.Blocked: {
        return '#f44336';
        break;
      }
      default: {
        return '#f44336';
        break;
      }
    }
  }

  startProgressBar(): void {
    const interval = setInterval(() => {
      this.progressBarValue = this.progressBarValue + 12;
      if (this.progressBarValue >= 100) {
        this.progressBarValue = 100;
        clearInterval(interval);
      }
    }, 2000);
  }
}


