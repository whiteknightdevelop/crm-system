import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';
import {Observable} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {HandleError, HttpErrorHandler} from '../../../../error-handlers/http-error-handler.service';
import {RabiesReportItem} from '../../../../models/rabies-report-item';
import {DateInterval} from '../../../../models/date-interval';

@Injectable()
export class RabiesListByDatePageService {
  private readonly handleError: HandleError;
  apiUrl = 'api/report/rabies-list-by-date/';

  constructor(
    private http: HttpClient,
    private router: Router,
    httpErrorHandler: HttpErrorHandler) {
    this.handleError = httpErrorHandler.createHandleError('RabiesListByDatePageService');
  }

  getRabiesListByDate(dateInterval: DateInterval): Observable<RabiesReportItem[]> {
    return this.http.post<RabiesReportItem[]>(this.apiUrl, dateInterval)
      .pipe(
        catchError(this.handleError<RabiesReportItem[]>('getRabiesListByDate'))
      );
  }
}



