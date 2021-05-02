import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import {HandleError, HttpErrorHandler} from '../../error-handlers/http-error-handler.service';
import {Visit} from '../../models/visit';
import {VisitPage} from '../../models/visit-page';
import {VisitPageLists} from '../../models/visit-page-lists';
import {HelperService} from '../../helper.service';

@Injectable({
  providedIn: 'root',
})
export class VisitService {

  visitUrl = 'api/visit/';
  private readonly handleError: HandleError;

  constructor(
    private http: HttpClient,
    httpErrorHandler: HttpErrorHandler,
    private helperService: HelperService) {
    this.handleError = httpErrorHandler.createHandleError('VisitService');
  }

  getVisit(id: number): Observable<VisitPage> {
    return this.http.get<VisitPage>(this.visitUrl + id)
      .pipe(
        catchError(this.handleError<VisitPage>('getVisit'))
      );
  }

  getVisitLists(): Observable<VisitPageLists> {
    return this.http.get<VisitPageLists>(this.visitUrl)
      .pipe(
        catchError(this.handleError<VisitPageLists>('getVisitLists'))
      );
  }

  updateVisit(visit: Visit): Observable<Visit> {
    visit.visitTime = this.helperService.addCurrentTimeToDate(visit.visitTime);
    return this.http.put<Visit>(this.visitUrl + visit.visitId, visit)
      .pipe(
        catchError(this.handleError<Visit>('updateVisit'))
      );
  }

  addVisit(visit: Visit): Observable<HttpErrorResponse| Visit> {
    visit.visitTime = this.helperService.addCurrentTimeToDate(visit.visitTime);
    return this.http.post<Visit>(this.visitUrl, visit)
      .pipe(
        catchError(this.handleError<Visit>('addVisit'))
      );
  }

  deleteVisit(visit: Visit): Observable<object> {
    const options = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        Authorization: 'my-auth-token'
      }),
      body: visit,
    };

    return this.http.delete(this.visitUrl + visit.visitId, options )
      .pipe(
        catchError(this.handleError<object>('deleteVisit')));
  }
}



