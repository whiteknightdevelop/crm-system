import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import { ErrorMessageService } from './error-message.service';
import {MessageService} from 'primeng/api';
import {formatDate} from '@angular/common';
import {Angulartics2} from 'angulartics2';

export type HandleError =
  <T> (operation?: string, result?: T) => (error: HttpErrorResponse) => Observable<T>;

/** Handles HttpClient errors */
@Injectable()
export class HttpErrorHandler {
  constructor(private errorMessageService: ErrorMessageService,
              private messageService: MessageService,
              private angulartics2: Angulartics2) {
  }

  /** Create curried handleError function that already knows the service name */
  createHandleError = (serviceName = '') => {
    return <T>(operation = 'operation', result = {} as T) =>
      this.handleError(serviceName, operation, result);
  }

  /**
   * Returns a function that handles Http operation failures.
   * This error handler lets the app continue to run as if no error occurred.
   * @param serviceName = name of the data service that attempted the operation
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  handleError<T>(serviceName = '', operation = 'operation', result = {} as T): any {
    return (error: HttpErrorResponse): Observable<T> => {
      console.error(error); 

      if (error.status !== 504 && error.status !== 401) {
        this.downloadFile(error);
      }

      this.angulartics2.eventTrack.next({action: 'myAction', properties: {category: 'myTarget', label: 'Exception', value: error.message}});
      return throwError(error);
    };
  }

  downloadFile(data: any): void {
    const dateString = formatDate(Date.now(), 'dd-MM-yyyy', 'he');
    const blob = new Blob([JSON.stringify(data)], { type: 'data:application/txt;charset=utf-8'});
    const fileName = 'error-' + dateString + '.json';
    const objectUrl = URL.createObjectURL(blob);
    const a: HTMLAnchorElement = document.createElement('a') as HTMLAnchorElement;

    a.href = objectUrl;
    a.download = fileName;
    document.body.appendChild(a);
    a.click();

    document.body.removeChild(a);
    URL.revokeObjectURL(objectUrl);
  }

  getErrorMessage(error: any): string  {
    switch (error.status) {
      case 504: {
        return 'אין מענה מהשרת (שגיאה 504)';
        break;
      }
      case 401: {
        return 'פרטי ההתחברות אינם נכונים (שגיאה 401)';
        break;
      }
      default: {
        return error.message;
        break;
      }
    }
  }
}
