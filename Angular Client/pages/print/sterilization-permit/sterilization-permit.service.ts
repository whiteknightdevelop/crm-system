import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {HandleError, HttpErrorHandler} from '../../../error-handlers/http-error-handler.service';

@Injectable()
export class SterilizationPermitService {
  debtUrl = 'api/debt/';
  debtsListUrl = 'api/debtsList/';
  private readonly handleError: HandleError;

  constructor(
    private http: HttpClient,
    httpErrorHandler: HttpErrorHandler) {
    this.handleError = httpErrorHandler.createHandleError('SterilizationPermitService');
  }
}



