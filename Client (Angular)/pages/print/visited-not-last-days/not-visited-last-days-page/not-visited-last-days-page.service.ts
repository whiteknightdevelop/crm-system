import {Injectable} from '@angular/core';
import {HandleError, HttpErrorHandler} from '../../../../error-handlers/http-error-handler.service';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Router} from '@angular/router';
import {Observable} from 'rxjs';
import {VisitedOwnersItem} from '../../../../models/visited-owners-item';
import {catchError} from 'rxjs/operators';

@Injectable()
export class NotVisitedLastDaysPageService {
  private readonly handleError: HandleError;
  apiUrl = 'api/report/not-visited-last-days/';

  constructor(
    private http: HttpClient,
    private router: Router,
    httpErrorHandler: HttpErrorHandler) {
    this.handleError = httpErrorHandler.createHandleError('NotVisitedLastDaysPageService');
  }

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json',
    })
  };

  getNotVisitedListByNumOfDays(days: number): Observable<VisitedOwnersItem[]> {
    return this.http.post<VisitedOwnersItem[]>(this.apiUrl, days.toString(), this.httpOptions)
      .pipe(
        catchError(this.handleError<VisitedOwnersItem[]>('getNotVisitedListByNumOfDays'))
      );
  }
}

