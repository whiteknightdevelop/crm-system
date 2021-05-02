import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import {HandleError, HttpErrorHandler} from '../../../error-handlers/http-error-handler.service';
import {Owner} from '../../../models/owner';

@Injectable()
export class SearchOwnerService {
  ownerUrl = 'api/SearchOwner/';
  private readonly handleError: HandleError;

  constructor(
    private http: HttpClient,
    httpErrorHandler: HttpErrorHandler) {
    this.handleError = httpErrorHandler.createHandleError('SearchOwnerService');
  }

  SearchOwner(owner: Owner): Observable<HttpErrorResponse| Owner[]> {
    return this.http.post<Owner[]>(this.ownerUrl, owner)
      .pipe(
        catchError(this.handleError<Owner[]>('SearchOwner')));
  }
}
