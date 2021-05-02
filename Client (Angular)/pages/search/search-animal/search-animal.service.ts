import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError} from 'rxjs/operators';
import {HandleError, HttpErrorHandler} from '../../../error-handlers/http-error-handler.service';
import {Animal} from '../../../models/animal';
import {SearchAnimalPage} from '../../../models/search-animal-page';

@Injectable()
export class SearchAnimalService {
  animalUrl = 'api/SearchAnimal/';
  private readonly handleError: HandleError;

  constructor(
    private http: HttpClient,
    httpErrorHandler: HttpErrorHandler) {
    this.handleError = httpErrorHandler.createHandleError('SearchAnimalService');
  }

  SearchAnimal(animal: Animal): Observable<HttpErrorResponse| SearchAnimalPage[]> {
    return this.http.post<SearchAnimalPage[]>(this.animalUrl, animal)
      .pipe(
        catchError(this.handleError<SearchAnimalPage[]>('SearchAnimal')));
  }
}
