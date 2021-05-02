import { Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'emptyString'})
export class EmptyStringPipe implements PipeTransform {
  transform(value: string): string {

    if (value && value.length !== 0){
      return value;
    }
    return '(ריק)';
  }
}
