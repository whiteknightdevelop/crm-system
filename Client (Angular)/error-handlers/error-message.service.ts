import { Injectable } from '@angular/core';

@Injectable()
export class ErrorMessageService {
  messages: string[] = [];

  add(message: string): void {
    this.messages.push(message);
  }

  clear(): void {
    this.messages = [];
  }
}
