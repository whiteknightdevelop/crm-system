import {FormGroup, Validators} from '@angular/forms';

export interface Debt {
  ownerId: number;
  debtId: number;
  debtDate: Date;
  animalName: string;
  cause: string;
  debtAmount: number;
  paidAmount: number;
}

export class DebtEntity implements Debt{
  constructor() {
    this.ownerId = 0;
    this.debtId = 0;
    this.debtDate = new Date();
    this.animalName = '';
    this.cause = '';
    this.debtAmount = 0;
    this.paidAmount = 0;
  }

  ownerId: number;
  debtId: number;
  debtDate: Date;
  animalName: string;
  cause: string;
  debtAmount: number;
  paidAmount: number;

  setData(form: FormGroup): void{
    this.ownerId = form.get('ownerId')?.value;
    this.debtId = form.get('debtId')?.value;
    this.debtDate = form.get('debtDate')?.value;
    this.animalName = form.get('animalName')?.value;
    this.cause = form.get('causeOfDebt')?.value;
    this.debtAmount = form.get('debtAmount')?.value;
    this.paidAmount = form.get('debtPaid')?.value;
  }
}
