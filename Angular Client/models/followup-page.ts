import {Owner, OwnerEntity} from './owner';
import {Animal, AnimalEntity} from './animal';
import {Followup} from './followup';

export interface FollowupPage {
  animal: Animal;
  owner: Owner;
  followUpsList: Followup[];
}

export class FollowupPageEntity implements FollowupPage{
  constructor() {
    this.animal = new AnimalEntity();
    this.owner = new OwnerEntity();
    this.followUpsList = [];
  }
  animal: Animal;
  owner: Owner;
  followUpsList: Followup[];
}
