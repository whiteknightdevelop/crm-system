import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class HelperService {

  constructor() {}

  setDateNull(date: Date): Date | null {
    const d1 = new Date('0001-01-01T00:00:00');
    const d2 = new Date(date);
    if (d1.valueOf() === d2.valueOf()){
      return null;
    }

    return d2;
  }

  addCurrentTimeToDate(date: Date): Date {
    const currentDate = new Date(Date.now());
    date.setHours(currentDate.getHours());
    date.setMinutes(currentDate.getMinutes());
    date.setSeconds(currentDate.getSeconds());
    return date;
  }
}



