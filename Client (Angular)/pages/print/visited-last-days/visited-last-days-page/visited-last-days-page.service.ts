import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Router} from '@angular/router';
import {Observable} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {HandleError, HttpErrorHandler} from '../../../../error-handlers/http-error-handler.service';
import {VisitedOwnersItem} from '../../../../models/visited-owners-item';

@Injectable()
export class VisitedLastDaysPageService {
  private readonly handleError: HandleError;
  apiUrl = 'api/report/visited-last-days/';

  constructor(
    private http: HttpClient,
    private router: Router,
    httpErrorHandler: HttpErrorHandler) {
    this.handleError = httpErrorHandler.createHandleError('VisitedLastDaysPageService');
  }

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json',
    })
  };

  getVisitedListByNumOfDays(days: number): Observable<VisitedOwnersItem[]> {
    return this.http.post<VisitedOwnersItem[]>(this.apiUrl, days.toString(), this.httpOptions)
      .pipe(
        catchError(this.handleError<VisitedOwnersItem[]>('getVisitedListByNumOfDays'))
      );
  }
}



