import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpErrorResponse} from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import {HandleError, HttpErrorHandler} from '../../error-handlers/http-error-handler.service';
import {OwnerPage} from '../../models/owner-page';
import {Owner} from '../../models/owner';

@Injectable()
export class OwnerService {
  ownerUrl = 'api/owner/';
  private readonly handleError: HandleError;

  constructor(
    private http: HttpClient,
    httpErrorHandler: HttpErrorHandler) {
    this.handleError = httpErrorHandler.createHandleError('OwnerService');
  }

  getOwner(id: number): Observable<OwnerPage> {
    return this.http.get<OwnerPage>(this.ownerUrl + id)
      .pipe(
        catchError(this.handleError<OwnerPage>('getOwner'))
      );
  }

  updateOwner(owner: Owner): Observable<Owner> {
    return this.http.put<Owner>(this.ownerUrl + owner.ownerId, owner)
      .pipe(
        catchError(this.handleError<Owner>('updateOwner'))
      );
  }

  addOwner(owner: Owner): Observable<HttpErrorResponse| Owner> {
    return this.http.post<Owner>(this.ownerUrl, owner)
      .pipe(
        catchError(this.handleError<Owner>('addOwner')));
  }

  deleteOwner(owner: Owner): Observable<object> {
    const options = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        Authorization: 'my-auth-token'
      }),
      body: owner,
    };
    return this.http.delete(this.ownerUrl + owner.ownerId, options )
      .pipe(
        catchError(this.handleError<Owner>('deleteOwner')));
  }
}