import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpErrorResponse} from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import {HandleError, HttpErrorHandler} from '../../error-handlers/http-error-handler.service';
import {Debt} from '../../models/debt';
import {DebtPage} from '../../models/debt-page';

@Injectable()
export class DebtService {
  debtUrl = 'api/debt/';
  debtsListUrl = 'api/debtsList/';
  private readonly handleError: HandleError;

  constructor(
    private http: HttpClient,
    httpErrorHandler: HttpErrorHandler) {
    this.handleError = httpErrorHandler.createHandleError('DebtService');
  }

  getDebtPage(ownerId: number): Observable<DebtPage> {
    return this.http.get<DebtPage>(this.debtUrl + ownerId)
      .pipe(
        catchError(this.handleError<DebtPage>('getDebtPage'))
      );
  }

  getDebtsList(ownerId: number): Observable<Debt[]> {
    return this.http.get<Debt[]>(this.debtsListUrl + ownerId)
      .pipe(
        catchError(this.handleError<Debt[]>('getDebtsList'))
      );
  }

  addDebt(debt: Debt): Observable<HttpErrorResponse| Debt> {
    return this.http.post<Debt>(this.debtUrl, debt)
      .pipe(
        catchError(this.handleError<Debt>('addDebt'))
      );
  }

  updateDebt(debt: Debt): Observable<Debt> {
    return this.http.put<Debt>(this.debtUrl + debt.debtId, debt)
      .pipe(
        catchError(this.handleError<Debt>('updateDebt'))
      );
  }

  deleteDebt(debt: Debt): Observable<object> {
    const options = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        Authorization: 'my-auth-token'
      }),
      body: debt,
    };

    return this.http.delete(this.debtUrl + debt.debtId, options )
      .pipe(
        catchError(this.handleError<object>('deleteDebt')));
  }
}



