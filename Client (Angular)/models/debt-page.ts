import {Owner, OwnerEntity} from './owner';
import {Debt} from './debt';

export interface DebtPage {
  owner: Owner;
  debtsList: Debt[];
}

export class DebtPageEntity implements DebtPage{
  constructor() {
    this.owner = new OwnerEntity();
    this.debtsList = [];
  }
  owner: Owner;
  debtsList: Debt[];
}
