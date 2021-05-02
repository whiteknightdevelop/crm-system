import { Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'smsSentStatus'})
export class SmsSentStatusPipe implements PipeTransform {
  transform(value: number): string {
    switch (value) {
      case 0: {
        return 'ממתין לשליחה...';
        break;
      }
      case 1: {
        return 'שולח...';
        break;
      }
      case 2: {
        return 'התקבל!';
        break;
      }
      case -2: {
        return 'לא התקבל!';
        break;
      }
      case -4: {
        return 'חסום!';
        break;
      }
      case -10: {
        return 'לא ניתן לשלוח!';
        break;
      }
      default: {
        return 'שגיאה!';
        break;
      }
    }
  }
}
