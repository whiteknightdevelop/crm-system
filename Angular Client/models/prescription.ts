import {User, UserEntity} from './user';
import {FormGroup} from '@angular/forms';

export interface Prescription {
  prescriptionId: number;
  visitId: number;
  drugName: string;
  drugFrequency: string;
  drugDosage: string;
  drugPeriod: string;
  drugComment: string;
  user: User | null;
}

export class PrescriptionEntity implements Prescription{
  constructor() {
    this.prescriptionId = 0;
    this.visitId = 0;
    this.drugName = '';
    this.drugFrequency = '';
    this.drugDosage = '';
    this.drugPeriod = '';
    this.drugComment = '';
    this.user = new UserEntity();
  }

  prescriptionId: number;
  visitId: number;
  drugName: string;
  drugFrequency: string;
  drugDosage: string;
  drugPeriod: string;
  drugComment: string;
  user: User | null;

  setData(form: FormGroup): void{
    this.visitId = form.get('visitId')?.value;
    this.drugName = form.get('drugName')?.value.drugName;
    this.drugDosage = form.get('drugDosage')?.value;
    this.drugFrequency = form.get('drugFrequency')?.value.drugFrequency;
    this.drugPeriod = form.get('drugPeriod')?.value.drugPeriod;
    this.drugComment = form.get('drugComment')?.value;
  }
}
