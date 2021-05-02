import { Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'booleanStringToBoolPipe'})
export class BooleanStringToBoolPipe implements PipeTransform {
  transform(value: string): boolean {
    switch (value) {
      case 'true': {
        return true;
        break;
      }
      case 'false': {
        return false;
        break;
      }
      default: {
        return false;
        break;
      }
    }
  }
}
