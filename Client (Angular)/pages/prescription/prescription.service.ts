import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpErrorResponse} from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import {HandleError, HttpErrorHandler} from '../../error-handlers/http-error-handler.service';
import {PrescriptionPage} from '../../models/prescription-page';
import {Prescription} from '../../models/prescription';

@Injectable()
export class PrescriptionService {
  prescriptionUrl = 'api/prescription/';
  prescriptionsListUrl = 'api/PrescriptionsList/';
  private readonly handleError: HandleError;

  constructor(
    private http: HttpClient,
    httpErrorHandler: HttpErrorHandler) {
    this.handleError = httpErrorHandler.createHandleError('PrescriptionService');
  }

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json',
    })
  };

  getPrescriptionPage(visitId: number): Observable<PrescriptionPage> {
    return this.http.get<PrescriptionPage>(this.prescriptionUrl + visitId)
      .pipe(
        catchError(this.handleError<PrescriptionPage>('getPrescriptionPage'))
      );
  }

  getPrescriptionsList(visitId: number): Observable<Prescription[]> {
    return this.http.get<Prescription[]>(this.prescriptionsListUrl + visitId)
      .pipe(
        catchError(this.handleError<Prescription[]>('getPrescriptionsList'))
      );
  }

  addPrescription(prescription: Prescription): Observable<HttpErrorResponse| Prescription> {
    return this.http.post<Prescription>(this.prescriptionUrl, prescription, this.httpOptions)
      .pipe(
        catchError(this.handleError<Prescription>('addPrescription'))
      );
  }

  deletePrescription(prescription: Prescription): Observable<object> {
    const options = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        Authorization: 'my-auth-token'
      }),
      body: prescription,
    };

    return this.http.delete(this.prescriptionUrl + prescription.prescriptionId, options )
      .pipe(
        catchError(this.handleError<object>('deletePrescription')));
  }
}



