import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';
import {Observable} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {HandleError, HttpErrorHandler} from '../../../../error-handlers/http-error-handler.service';
import {Debtor} from '../../../../models/debtor';

@Injectable()
export class DebtsSheetPageService {
  private readonly handleError: HandleError;
  apiUrl = 'api/report/debts-sheet/';

  constructor(
    private http: HttpClient,
    private router: Router,
    httpErrorHandler: HttpErrorHandler) {
    this.handleError = httpErrorHandler.createHandleError('DebtsSheetPageService');
  }

  getDebtorsList(): Observable<Debtor[]> {
    return this.http.get<Debtor[]>(this.apiUrl)
      .pipe(
        catchError(this.handleError<Debtor[]>('getDebtorsList'))
      );
  }
}



