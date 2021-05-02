import {Followup, FollowupEntity} from './followup';
import {Animal, AnimalEntity} from './animal';
import {Owner, OwnerEntity} from './owner';

export interface FollowupAllItem {
  followup: Followup;
  animal: Animal;
  owner: Owner;
}

export class FollowupAllItemEntity implements FollowupAllItem{
  constructor() {
    this.followup = new FollowupEntity();
    this.animal = new AnimalEntity();
    this.owner = new OwnerEntity();
  }
  followup: Followup;
  animal: Animal;
  owner: Owner;
}
