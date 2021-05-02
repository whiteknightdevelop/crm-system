import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';
import {HandleError, HttpErrorHandler} from '../../error-handlers/http-error-handler.service';

@Injectable()
export class PrintService {
  private readonly handleError: HandleError;
  isPrinting = false;

  constructor(
    private http: HttpClient,
    private router: Router,
    httpErrorHandler: HttpErrorHandler) {
    this.handleError = httpErrorHandler.createHandleError('PrintService');
  }

  printDocument(documentName: string, documentData: object): void {
    this.isPrinting = true;
    this.router.navigate(['/',
      { outlets: {
          print: ['print', documentName, documentData]
        }}]);
  }

  onDataReady(fileName: string): void {
    setTimeout(() => {
      document.title = fileName;
      window.print();
      this.isPrinting = false;
      document.title = 'פט-אדמין';
      this.router.navigate([{ outlets: { print: null }}]);
    }, 250);
  }
}



