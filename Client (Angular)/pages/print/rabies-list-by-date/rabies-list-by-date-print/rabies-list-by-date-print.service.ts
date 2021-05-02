import { Injectable } from '@angular/core';
import {HandleError, HttpErrorHandler} from '../../../../error-handlers/http-error-handler.service';

@Injectable()
export class RabiesListByDatePrintService {
  private readonly handleError: HandleError;

  constructor(httpErrorHandler: HttpErrorHandler) {
    this.handleError = httpErrorHandler.createHandleError('RabiesListByDatePrintService');
    this.from = new Date();
    this.to = new Date();
  }

  from: Date;
  to: Date;
}



