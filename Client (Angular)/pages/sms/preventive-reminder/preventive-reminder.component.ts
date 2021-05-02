import { Component, OnInit } from '@angular/core';
import {HandleError, HttpErrorHandler} from '../../../error-handlers/http-error-handler.service';
import {PrintService} from '../../print/print.service';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {SmsService} from '../sms.service';
import {ConfirmationService, MessageService} from 'primeng/api';
import {Router} from '@angular/router';
import {formatDate} from '@angular/common';
import {DATE_REGEX, MOBILENUMBER_REGEX} from '../../../models/RegexPatterns';
import {SmsAppointment} from '../../../models/sms-appointment';
import {SmsPullNotification} from '../../../models/sms-pull-notification';
import {SmsStatus} from '../../../enums/sms-status';
import {DateInterval, DateIntervalEntity} from '../../../models/date-interval';
import {PreventiveReminderSms} from '../../../models/preventive-reminder-sms';

@Component({
  selector: 'app-preventive-reminder',
  templateUrl: './preventive-reminder.component.html',
  styleUrls: ['./preventive-reminder.component.css']
})
export class PreventiveReminderComponent implements OnInit {

  private readonly handleError: HandleError;
  constructor(private fb: FormBuilder,
              private smsService: SmsService,
              private messageService: MessageService,
              private httpErrorHandler: HttpErrorHandler,
              private confirmationService: ConfirmationService,
              private router: Router) {
    this.handleError = httpErrorHandler.createHandleError('PreventiveReminderComponent');
    this.remindersList = [];
    this.showTable = false;
    this.showProgress = false;
    this.sendMessagesBtnDisabled = false;
    this.showInforuWebsiteUrl = false;
    this.pullNotifications = [];
    this.sentStatusColor = '';
    this.displayDialog = false;
    this.progressBarValue = 0;
    this.loadingTableData = false;
    this.numberOfSentSms = 0;
  }

  form = this.fb.group({
    from: [formatDate(Date.now(), 'yyyy-MM-dd', 'he'),
      Validators.compose([Validators.required, Validators.pattern(DATE_REGEX)])],
    to: [formatDate(this.todayPlusNumOfDays(3), 'yyyy-MM-dd', 'he'),
      Validators.compose([Validators.required, Validators.pattern(DATE_REGEX)])]
  });

  get from(): any { return this.form.get('from'); }
  get to(): any { return this.form.get('to'); }

  remindersList: PreventiveReminderSms[];
  showTable: boolean;
  showProgress: boolean;
  sendMessagesBtnDisabled: boolean;
  showInforuWebsiteUrl: boolean;
  pullNotifications: SmsPullNotification[];
  sentStatusColor: string;
  displayDialog: boolean;
  progressBarValue: number;
  loadingTableData: boolean;
  numberOfSentSms: number;

  ngOnInit(): void {
    this.searchByDateInterval();
  }

  todayPlusNumOfDays(numOfDays: number): Date {
    const year = new Date().getFullYear();
    let month = new Date().getMonth();
    const lastDayOfTheMonth = new Date(year, month, 0).getDate();
    const today = new Date().getDate();
    let wantedDay = today + numOfDays;
    if (wantedDay > lastDayOfTheMonth) {
      wantedDay = wantedDay - lastDayOfTheMonth;
      month = month + 1;
    }
    return new Date(year, month, wantedDay);
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

  sendMessages(): void {
    if (!this.sendMessagesBtnDisabled){

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
          const validNumbersOnly = this.remindersList.filter( reminder => this.isValidMobileNumber(reminder.phone));
          this.remindersList.forEach(item => item.sentStatus = SmsStatus.Sending);
          this.smsService.sendSmsReminders(validNumbersOnly).subscribe(response => {
            if (response) {
              this.pullNotifications = response;
              this.markMessageSentStatus();
              this.numberOfSentSms = response.length;
              this.displayDialog = false;
              this.messageService.add({key: 'sendSmsResponseMessage', severity: 'success', summary: 'ההודעות נשלחו!', life: 1000});
            }else {
              this.messageService.add({
                key: 'sendSmsResponseMessage', severity: 'error', summary: 'תקלה! יש לבדוק דוח הודעות באתר!', life: 3000
              });
            }
          }, error => {
            this.displayDialog = false;
            this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
          });
        },
        reject: () => {}
      });
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

  markMessageSentStatus(): void {
    this.pullNotifications.forEach(notification => {
      this.remindersList.forEach(reminder => {
        if (notification.phoneNumber === reminder.phone) {
          reminder.sentStatus = notification.status;
        }
      });
    });
  }

  searchByDateInterval(): void {
    this.loadingTableData = true;
    this.smsService.getPreventiveReminderByDateInterval(this.convertFormToDateInterval(this.form)).subscribe(response => {
      this.remindersList = response;
      this.remindersList.forEach((item, index) => {
        item.id = index.toString();
        item.category = 'Preventive_Reminder';
        if (this.isValidMobileNumber(item.phone)) {
          item.sentStatus = SmsStatus.Waiting;
        }else {
          item.sentStatus = SmsStatus.NotValid;
        }
      });
      this.showTable = true;
      this.loadingTableData = false;
    }, error => {
      this.loadingTableData = false;
      this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
    });
  }

  convertFormToDateInterval(form: FormGroup): DateInterval {
    const interval = new DateIntervalEntity();
    interval.from = form.get('from')?.value;
    interval.to = form.get('to')?.value;
    return interval;
  }

  itemSelected(item: PreventiveReminderSms): void {
    this.router.navigateByUrl('/animal/' + item.animalId);
  }

  isValidMobileNumber(mobile: string): boolean {
    const regExp = new RegExp(MOBILENUMBER_REGEX);
    if (regExp.test(mobile)) {
      return true;
    }
    return false;
  }

  goToOwner(ownerId: number): void {
    this.router.navigateByUrl('/owner/' + ownerId);
  }

  goToAnimal(animalId: number): void {
    this.router.navigateByUrl('/animal/' + animalId);
  }
}
