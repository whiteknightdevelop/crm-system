import { Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'measurements'})
export class MeasurementsPipe implements PipeTransform {
  transform(value: number): string {
    if (value === 0){
      return 'לא נמדד';
    }
    return value.toString();
  }
}
