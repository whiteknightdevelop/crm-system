import {Owner, OwnerEntity} from './owner';
import {Animal, AnimalEntity} from './animal';
import {Visit, VisitEntity} from './visit';

export interface RabiesReportItem {
  owner: Owner;
  animal: Animal;
  visit: Visit;
}

export class RabiesReportItemEntity implements RabiesReportItem{
  constructor() {
    this.owner = new OwnerEntity();
    this.animal = new AnimalEntity();
    this.visit = new VisitEntity();
  }
  owner: Owner;
  animal: Animal;
  visit: Visit;
}
