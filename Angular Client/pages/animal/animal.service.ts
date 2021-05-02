import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import {HandleError, HttpErrorHandler} from '../../error-handlers/http-error-handler.service';
import {AnimalPage} from '../../models/animal-page';
import {Animal} from '../../models/animal';
import {AnimalPageLists} from '../../models/animal-page-lists';
import {HelperService} from '../../helper.service';

@Injectable({
  providedIn: 'root',
})
export class AnimalService {

  animalUrl = 'api/animal/';
  private readonly handleError: HandleError;

  constructor(
    private http: HttpClient,
    httpErrorHandler: HttpErrorHandler,
    private helperService: HelperService) {
    this.handleError = httpErrorHandler.createHandleError('AnimalService');
  }

  getAnimal(id: number): Observable<AnimalPage> {
    return this.http.get<AnimalPage>(this.animalUrl + id)
      .pipe(
        catchError(this.handleError<AnimalPage>('getAnimal'))
      );
  }

  getAnimalLists(): Observable<AnimalPageLists> {
    return this.http.get<AnimalPageLists>(this.animalUrl)
      .pipe(
        catchError(this.handleError<AnimalPageLists>('getAnimalLists'))
      );
  }

  updateAnimal(animal: Animal): Observable<Animal> {
    return this.http.put<Animal>(this.animalUrl + animal.animalId, animal)
      .pipe(
        catchError(this.handleError<Animal>('updateAnimal'))
      );
  }

  deleteAnimal(animal: Animal): Observable<object> {
    const options = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        Authorization: 'my-auth-token'
      }),
      body: animal,
    };

    return this.http.delete(this.animalUrl + animal.animalId, options )
      .pipe(
        catchError(this.handleError<object>('deleteAnimal')));
  }

  addAnimal(animal: Animal): Observable<HttpErrorResponse| Animal> {
    return this.http.post<Animal>(this.animalUrl, animal)
      .pipe(
        catchError(this.handleError<Animal>('addAnimal'))
      );
  }
}



