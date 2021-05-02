export interface DateInterval {
  from: Date;
  to: Date;
}

export class DateIntervalEntity implements DateInterval{
  constructor() {
    this.from = new Date();
    this.to = new Date();
  }
  from: Date;
  to: Date;
}


