import {Prescription} from './prescription';
import {Drug} from './drug';
import {Period} from './period';
import {Frequency} from './frequency';
import {Dosage} from './dosage';
import {Visit, VisitEntity} from './visit';
import {Animal, AnimalEntity} from './animal';
import {Owner, OwnerEntity} from './owner';

export interface PrescriptionPage {
  visit: Visit;
  animal: Animal;
  owner: Owner;
  prescriptionsList: Prescription[];
  drugsList: Drug[];
  periodsList: Period[];
  frequencysList: Frequency[];
  dosagesList: Dosage[];
}

export class PrescriptionPageEntity implements PrescriptionPage{
  constructor() {
    this.visit = new VisitEntity();
    this.animal = new AnimalEntity();
    this.owner = new OwnerEntity();
    this.prescriptionsList = [];
    this.drugsList = [];
    this.periodsList = [];
    this.frequencysList = [];
    this.dosagesList = [];
  }

  visit: Visit;
  animal: Animal;
  owner: Owner;
  prescriptionsList: Prescription[];
  drugsList: Drug[];
  periodsList: Period[];
  frequencysList: Frequency[];
  dosagesList: Dosage[];
}
