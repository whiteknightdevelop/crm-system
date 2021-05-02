import { Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'smsSentStatusIsNotDelivered'})
export class SmsSentStatusIsNotDeliveredPipe implements PipeTransform {
  transform(value: number): boolean {
    switch (value) {
      case -2: {
        return true;
        break;
      }
      case -4: {
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
