import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpErrorResponse} from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import {HandleError, HttpErrorHandler} from '../../error-handlers/http-error-handler.service';
import {Followup} from '../../models/followup';
import {FollowupPage} from '../../models/followup-page';
import {DateInterval} from '../../models/date-interval';
import {FollowupAllItemEntity} from '../../models/followup-all-item';

@Injectable()
export class FollowUpService {
  followupUrl = 'api/followup/';
  followupsListUrl = 'api/followuplist/';
  followupsAllListUrl = 'api/followup/followup-all';
  private readonly handleError: HandleError;

  constructor(
    private http: HttpClient,
    httpErrorHandler: HttpErrorHandler) {
    this.handleError = httpErrorHandler.createHandleError('FollowUpService');
  }

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json'
    })
  };

  getFollowupPage(animalId: number): Observable<FollowupPage> {
    return this.http.get<FollowupPage>(this.followupUrl + animalId)
      .pipe(
        catchError(this.handleError<FollowupPage>('getFollowupPage'))
      );
  }

  getFollowupsList(animalId: number): Observable<Followup[]> {
    return this.http.get<Followup[]>(this.followupsListUrl + animalId)
      .pipe(
        catchError(this.handleError<Followup[]>('getFollowupsList'))
      );
  }

  addFollowup(followup: Followup): Observable<HttpErrorResponse| Followup> {
    return this.http.post<Followup>(this.followupUrl, followup)
      .pipe(
        catchError(this.handleError<Followup>('addFollowup'))
      );
  }

  updateFollowup(followup: Followup): Observable<Followup> {
    return this.http.put<Followup>(this.followupUrl + followup.followUpId, followup)
      .pipe(
        catchError(this.handleError<Followup>('updateFollowup'))
      );
  }

  deleteFollowup(followup: Followup): Observable<object> {
    const options = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        Authorization: 'my-auth-token'
      }),
      body: followup,
    };

    return this.http.delete(this.followupUrl + followup.followUpId, options )
      .pipe(
        catchError(this.handleError<object>('deleteFollowup')));
  }

  getFollowupAll(dateInterval: DateInterval): Observable<FollowupAllItemEntity[]> {
    return this.http.post<FollowupAllItemEntity[]>(this.followupsAllListUrl, dateInterval.from, this.httpOptions)
      .pipe(
        catchError(this.handleError<FollowupAllItemEntity[]>('getFollowupAll'))
      );
  }
}



