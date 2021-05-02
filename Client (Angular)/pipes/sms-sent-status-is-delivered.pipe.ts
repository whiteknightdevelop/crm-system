import { Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'smsSentStatusIsDelivered'})
export class SmsSentStatusIsDeliveredPipe implements PipeTransform {
  transform(value: number): boolean {
    switch (value) {
      case 2: {
        return true;
        break;
      }
      default: {
        return false;
        break;
      }
    }
  }
}
