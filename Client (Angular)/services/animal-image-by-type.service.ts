import {Injectable, NgZone} from '@angular/core';
import * as signalR from '@microsoft/signalr';
import {HandleError, HttpErrorHandler} from '../error-handlers/http-error-handler.service';
import {HttpClient} from '@angular/common/http';
import { isDevMode } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SignalrService {

  constructor(httpErrorHandler: HttpErrorHandler, private http: HttpClient, private zone: NgZone) {
    this.handleError = httpErrorHandler.createHandleError('SignalrService');

    if ( isDevMode() ) {
      this.hubConnection = new signalR.HubConnectionBuilder()
        .withUrl('https://localhost:44444/systemhub')
        .build();
    }else {
      this.hubConnection = new signalR.HubConnectionBuilder()
        .withUrl('https://program.local:5555/systemhub')
        .build();
    }
  }

  private readonly handleError: HandleError;
  private hubConnection: signalR.HubConnection;
}



