import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import {HandleError, HttpErrorHandler} from '../../../../error-handlers/http-error-handler.service';
import {PreventiveReminder} from '../../../../models/preventive-reminder';

@Injectable({
  providedIn: 'root',
})
export class PreventiveReminderListService {

  preventiveReminderUrl = 'api/reminder/';
  private readonly handleError: HandleError;

  constructor(
    private http: HttpClient,
    httpErrorHandler: HttpErrorHandler) {
    this.handleError = httpErrorHandler.createHandleError('AnimalService');
  }

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json',
      Authorization: 'my-auth-token'
    })
  };

  getPreventiveReminderList(id: number): Observable<PreventiveReminder[]> {
    return this.http.get<PreventiveReminder[]>(this.preventiveReminderUrl + id)
      .pipe(
        catchError(this.handleError<PreventiveReminder[]>('getPreventiveReminderList'))
      );
  }

  deletePreventiveReminder(selectedReminders: PreventiveReminder[]): Observable<object| PreventiveReminder[]> {
    const options = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        Authorization: 'my-auth-token'
      }),
      body: selectedReminders,
    };

    return this.http.delete(this.preventiveReminderUrl, options)
      .pipe(
        catchError(this.handleError<PreventiveReminder[]>('deletePreventiveReminder')));
  }

  addPreventiveReminder(preventiveReminder: PreventiveReminder): Observable<HttpErrorResponse| PreventiveReminder> {
    return this.http.post<PreventiveReminder>(this.preventiveReminderUrl, preventiveReminder)
      .pipe(
        catchError(this.handleError<PreventiveReminder>('addPreventiveReminder'))
      );
  }
}

