import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import {HandleError, HttpErrorHandler} from '../../../../../error-handlers/http-error-handler.service';
import {PreventiveTreatment} from '../../../../../models/preventive-treatment';


@Injectable({
  providedIn: 'root',
})
export class PreventiveTreatmentService {

  treatmentUrl = 'api/preventivetreatment/';
  private readonly handleError: HandleError;

  constructor(
    private http: HttpClient,
    httpErrorHandler: HttpErrorHandler) {
    this.handleError = httpErrorHandler.createHandleError('PreventiveTreatmentService');
  }

  addNewPreventiveTreatment(treatment: PreventiveTreatment): Observable<HttpErrorResponse| PreventiveTreatment> {
    return this.http.post<PreventiveTreatment>(this.treatmentUrl, treatment)
      .pipe(
        catchError(this.handleError<PreventiveTreatment>('addNewPreventiveTreatment'))
      );
  }

  addNewPreventiveTreatmentsList(list: PreventiveTreatment[]): Observable<HttpErrorResponse| number> {
    return this.http.post<number>(this.treatmentUrl, list)
      .pipe(
        catchError(this.handleError<number>('addNewPreventiveTreatmentsList'))
      );
  }

  getVisitPreventiveTreatmentsList(id: number): Observable<PreventiveTreatment[]> {
    return this.http.get<PreventiveTreatment[]>(this.treatmentUrl + id)
      .pipe(
        catchError(this.handleError<PreventiveTreatment[]>('getVisitPreventiveTreatmentsList'))
      );
  }

  deletePreventiveTreatments(selectedTreatments: PreventiveTreatment[]): Observable<object| PreventiveTreatment[]> {
    const options = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        Authorization: 'my-auth-token'
      }),
      body: selectedTreatments,
    };

    return this.http.delete(this.treatmentUrl, options)
      .pipe(
        catchError(this.handleError<PreventiveTreatment[]>('deletePreventiveTreatments')));
  }
}


