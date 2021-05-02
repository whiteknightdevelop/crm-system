import { Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'shortString'})
export class ShortStringPipe implements PipeTransform {
  transform(value: string, length: number): string {

    if (value && value.length !== 0){
      if (value.length >= length){
        const newStr = value.substring(0, length) + '...';
        return newStr;
      }
      return value;
    }
    return '(ריק)';
  }
}
