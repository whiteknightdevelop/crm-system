import {Animal, AnimalEntity} from './animal';
import {Visit} from './visit';
import {Owner, OwnerEntity} from './owner';

export interface AnimalPagePrint {
  animal: Animal;
  owner: Owner;
  visitsList: Visit[];
}

export class AnimalPagePrintEntity implements AnimalPagePrint{
  constructor() {
    this.animal = new AnimalEntity();
    this.owner = new OwnerEntity();
    this.visitsList = [];
  }
  animal: Animal;
  owner: Owner;
  visitsList: Visit[];
}
