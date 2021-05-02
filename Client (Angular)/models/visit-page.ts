import {Animal, AnimalEntity} from './animal';
import {Visit, VisitEntity} from './visit';
import {Owner, OwnerEntity} from './owner';
import {PreventiveTreatment} from './preventive-treatment';
import {VisitPageLists, VisitPageListsEntity} from './visit-page-lists';

export interface VisitPage {
  visit: Visit;
  animal: Animal;
  owner: Owner;
  lists: VisitPageLists;
  preventiveTreatmentsList: PreventiveTreatment[];
  prescriptionsNumber: number;
}

export class VisitPageEntity implements VisitPage{
  constructor() {
    this.animal = new AnimalEntity();
    this.lists = new VisitPageListsEntity();
    this.owner = new OwnerEntity();
    this.preventiveTreatmentsList = [];
    this.visit = new VisitEntity();
    this.prescriptionsNumber = 0;
  }
  animal: Animal;
  lists: VisitPageLists;
  owner: Owner;
  preventiveTreatmentsList: PreventiveTreatment[] = [];
  visit: Visit;
  prescriptionsNumber: number;
}
