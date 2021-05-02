import {User, UserEntity} from './user';
import {FormGroup} from '@angular/forms';
import {PreventiveTreatment} from './preventive-treatment';

export interface Visit {
  visitId: number;
  animalId: number;
  visitTime: Date;
  cause: string;
  symptoms: string;
  labResults: string;
  comment: string;
  temperature: number;
  weight: number;
  pulse: number;
  diagnosis1: string;
  diagnosis2: string;
  diagnosis3: string;
  treatment1: string;
  treatment2: string;
  treatment3: string;
  treatment4: string;
  treatment5: string;
  treatment6: string;
  createdDate: Date;
  archive: boolean;
  preventiveTreatmentsList: PreventiveTreatment[];
  userId: number;
  user: User;
}

export class VisitEntity implements Visit{
  constructor() {
    this.animalId = 0;
    this.archive =  false;
    this.cause =  '';
    this.comment =  '';
    this.createdDate = new Date();
    this.diagnosis1 =  '';
    this.diagnosis2 =  '';
    this.diagnosis3 =  '';
    this.labResults =  '';
    this.pulse = 0;
    this.symptoms =  '';
    this.temperature = 0;
    this.treatment1 =  '';
    this.treatment2 =  '';
    this.treatment3 =  '';
    this.treatment4 =  '';
    this.treatment5 =  '';
    this.treatment6 =  '';
    this.user = new UserEntity();
    this.userId = 0;
    this.visitId = 0;
    this.visitTime = new Date();
    this.weight = 0;
    this.preventiveTreatmentsList = [];
  }
  animalId: number;
  archive: boolean;
  cause: string;
  comment: string;
  createdDate: Date;
  diagnosis1: string;
  diagnosis2: string;
  diagnosis3: string;
  labResults: string;
  pulse: number;
  symptoms: string;
  temperature: number;
  treatment1: string;
  treatment2: string;
  treatment3: string;
  treatment4: string;
  treatment5: string;
  treatment6: string;
  user: User;
  userId: number;
  visitId: number;
  visitTime: Date;
  weight: number;
  preventiveTreatmentsList: PreventiveTreatment[];

  setData(form: FormGroup): void{
    this.animalId = form.get('animalId')?.value;
    this.cause = form.get('cause')?.value;
    this.comment = form.get('comment')?.value;
    this.diagnosis1 = form.get('diagnosis1')?.value;
    this.diagnosis2 = form.get('diagnosis2')?.value;
    this.diagnosis3 = form.get('diagnosis3')?.value;
    this.labResults = form.get('labResults')?.value;
    this.symptoms = form.get('symptoms')?.value;
    this.pulse = form.get('pulse')?.value;
    this.temperature = form.get('temperature')?.value;
    this.weight = form.get('weight')?.value;
    this.treatment1 = form.get('treatment1')?.value;
    this.treatment2 = form.get('treatment2')?.value;
    this.treatment3 = form.get('treatment3')?.value;
    this.treatment4 = form.get('treatment4')?.value;
    this.treatment5 = form.get('treatment5')?.value;
    this.treatment6 = form.get('treatment6')?.value;
    this.visitId = form.get('visitId')?.value;
    this.visitTime = new Date(form.get('visitTime')?.value);
  }
}
