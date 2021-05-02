import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError} from 'rxjs/operators';
import {HandleError, HttpErrorHandler} from '../../error-handlers/http-error-handler.service';
import {SmsAppointment} from '../../models/sms-appointment';
import {SmsPullNotification} from '../../models/sms-pull-notification';
import {DateInterval} from '../../models/date-interval';
import {PreventiveReminderSms} from '../../models/preventive-reminder-sms';


@Injectable()
export class SmsService {
  apiUrl = 'api/sms/';
  private readonly handleError: HandleError;

  constructor(
    private http: HttpClient,
    httpErrorHandler: HttpErrorHandler) {
    this.handleError = httpErrorHandler.createHandleError('SmsService');
  }

  sendSmsAppointments(list: SmsAppointment[]): Observable<SmsPullNotification[]> {
    return this.http.post<SmsPullNotification[]>(this.apiUrl + 'send-appointments', list)
      .pipe(
        catchError(this.handleError<SmsPullNotification[]>('sendSmsAppointments')));
  }

  getPreventiveReminderByDateInterval(interval: DateInterval): Observable<PreventiveReminderSms[]> {
    return this.http.post<PreventiveReminderSms[]>(this.apiUrl + 'preventive-reminder', interval)
      .pipe(
        catchError(this.handleError<PreventiveReminderSms[]>('getPreventiveReminderByDateInterval')));
  }

  sendSmsReminders(list: PreventiveReminderSms[]): Observable<SmsPullNotification[]> {
    return this.http.post<SmsPullNotification[]>(this.apiUrl + 'send-reminders', list)
      .pipe(
        catchError(this.handleError<SmsPullNotification[]>('sendSmsReminders')));
  }
}

