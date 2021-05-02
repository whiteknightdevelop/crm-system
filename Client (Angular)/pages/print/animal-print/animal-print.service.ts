import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {HandleError, HttpErrorHandler} from '../../../error-handlers/http-error-handler.service';
import {AnimalPagePrint} from '../../../models/animal-page-print';


@Injectable({
  providedIn: 'root',
})
export class AnimalPrintService {

  animalPrintUrl = 'api/report/animal-print/';
  private readonly handleError: HandleError;

  constructor(
    private http: HttpClient,
    httpErrorHandler: HttpErrorHandler) {
    this.handleError = httpErrorHandler.createHandleError('AnimalPrintService');
  }

  getAnimalPrint(animalId: number): Observable<AnimalPagePrint> {
    return this.http.get<AnimalPagePrint>(this.animalPrintUrl + animalId)
      .pipe(
        catchError(this.handleError<AnimalPagePrint>('getAnimalPrint'))
      );
  }
}



