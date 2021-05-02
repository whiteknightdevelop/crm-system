import {Owner, OwnerEntity} from './owner';
import {Animal} from './animal';

export interface OwnerPage {
  owner: Owner;
  ownerTotalDebtAmount: number;
  animalsList: Animal[];
}

export class OwnerPageEntity implements OwnerPage {
  constructor() {
    this.animalsList = [];
    this.owner = new OwnerEntity();
    this.ownerTotalDebtAmount = 0;
  }
  animalsList: Animal[];
  owner: Owner;
  ownerTotalDebtAmount: number;
}
