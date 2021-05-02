import {Animal, AnimalEntity} from './animal';
import {Visit} from './visit';
import {Owner, OwnerEntity} from './owner';
import {AnimalPageLists, AnimalPageListsEntity} from './animal-page-lists';
import {PreventiveReminder} from './preventive-reminder';
import {Reminder} from './reminder';

export interface AnimalPage {
  animal: Animal;
  animalOwner: Owner;
  visitsList: Visit[];
  lists: AnimalPageLists;
  preventiveRemindersList: PreventiveReminder[];
  remindersList: Reminder[];
}

export class AnimalPageEntity implements AnimalPage{
  constructor() {
    this.animal = new AnimalEntity();
    this.animalOwner = new OwnerEntity();
    this.lists = new AnimalPageListsEntity();
    this.preventiveRemindersList = [];
    this.visitsList = [];
    this.remindersList = [];
  }
  animal: Animal;
  animalOwner: Owner;
  lists: AnimalPageLists;
  preventiveRemindersList: PreventiveReminder[];
  visitsList: Visit[];
  remindersList: Reminder[];
}
