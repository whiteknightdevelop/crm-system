import {FormGroup} from '@angular/forms';

export interface Followup {
  followUpId: number;
  animalId: number;
  date: Date;
  cause: string;
  status: boolean;
}

export class FollowupEntity implements Followup{
  constructor() {
    this.followUpId = 0;
    this.animalId = 0;
    this.date = new Date();
    this.cause = '';
    this.status = false;
  }
  followUpId: number;
  animalId: number;
  date: Date;
  cause: string;
  status: boolean;

  setData(form: FormGroup): void{
    this.followUpId = form.get('followUpId')?.value;
    this.animalId = form.get('animalId')?.value;
    this.date = form.get('date')?.value;
    this.cause = form.get('cause')?.value;
    this.status = form.get('status')?.value;
  }
}
