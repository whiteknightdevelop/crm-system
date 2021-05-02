import {FormGroup} from '@angular/forms';

export interface SmsAppointment {
  id: string;
  date: Date | null;
  category: string;
  name: string;
  recipient: string;
  sentStatus: number;
}

export class SmsAppointmentEntity implements SmsAppointment{
  constructor() {
    this.id = '';
    this.date = new Date();
    this.category = 'Appointment';
    this.name = '';
    this.recipient = '';
    this.sentStatus = 0;
  }
  id: string;
  date: Date | null;
  category: string;
  name: string;
  recipient: string;
  sentStatus: number;

  setData(form: FormGroup): void{
    this.date = form.get('date')?.value ? new Date(form.get('date')?.value) : null;
    const time = form.get('time')?.value;
    const hour = time.substring(0, 2);
    const minutes = time.substring(3, 5);
    this.date?.setHours(parseInt(hour, 10));
    this.date?.setMinutes(parseInt(minutes, 10));
    this.name = form.get('name')?.value;
    this.recipient = form.get('recipient')?.value;
  }
}


