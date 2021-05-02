import {Injectable, NgZone} from '@angular/core';
import * as signalR from '@microsoft/signalr';
import {HandleError, HttpErrorHandler} from '../error-handlers/http-error-handler.service';
import {catchError} from 'rxjs/operators';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable, Subject} from 'rxjs';
import { isDevMode } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SignalrService {

  constructor(httpErrorHandler: HttpErrorHandler, private http: HttpClient, private zone: NgZone) {
    this.handleError = httpErrorHandler.createHandleError('SignalrService');
    if ( isDevMode() ) {
      this.hubConnection = new signalR.HubConnectionBuilder()
        .withUrl('https://localhost:4444/systemhub')
        .build();
    }else {
      this.hubConnection = new signalR.HubConnectionBuilder()
        .withUrl('https://program.local:5555/systemhub')
        .build();
    }
  }

  private readonly handleError: HandleError;
  private hubConnection: signalR.HubConnection;
  backupUrl = 'api/system/backup/';
  restoreUrl = 'api/system/restore/';
  connectionId = '';
  progressPercentageComplete = 0;
  progressmessage = '';
  private progressMessageSource = new Subject<string>();
  progressMessageString$ = this.progressMessageSource.asObservable();

  updateProgressMessage(data: string): void {
    this.progressMessageSource.next(data);
  }

  public startConnection(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.hubConnection.start()
        .then(() => {
          console.log('Connection started...');
        }).finally(() => {
        this.hubConnection.invoke('GetConnectionId').then((id) => {
          this.connectionId = id;
          resolve();
        });
      })
        .catch(reason => {
          this.handleError(reason);
          reject();
        });
    });
  }

  public closeConnection(): void {
    this.hubConnection.stop().then(() => {
      this.progressPercentageComplete = 0;
      this.progressmessage = '';
      console.log('Connection stopped!');
    });
  }

  public addReportBackupProgress(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.hubConnection.on('ReportBackupProgress', (percentageComplete) => {
        this.zone.run(() => {
          this.progressPercentageComplete = percentageComplete;
        });
      });
      resolve();
    });
  }

  public addReportRestoreProgress(): Promise<void> {
    return new Promise((resolve) => {
      this.hubConnection.on('ReportRestoreProgress', (percentageComplete, message) => {
        this.zone.run(() => {
          this.progressPercentageComplete = percentageComplete;
          this.progressmessage = message;
          this.updateProgressMessage(message);
        });
      });
      resolve();
    });
  }

  getBackupFile(): Observable<object> {
    return this.http.get(this.backupUrl + '?connectionId=' + this.connectionId, { responseType: 'blob'})
      .pipe(
        catchError(this.handleError<object>('getBackupFile'))
      );
  }

  UploadRestoreFile($event: any): Observable<object> {
    const file = $event.files[0];
    const formData = new FormData();
    formData.append('restore', file);
    return this.http.post(this.restoreUrl + '?connectionId=' + this.connectionId, formData)
      .pipe(
        catchError(this.handleError<object>('UploadRestoreFile'))
      );
  }
}



