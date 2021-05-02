import {Type} from './type';
import {Gender} from './gender';
import {Breed} from './breed';
import {Color} from './color';

export interface AnimalPageLists {
  typesList: Type[];
  gendersList: Gender[];
  breedsList: Breed[];
  colorsList: Color[];
}

export class AnimalPageListsEntity implements AnimalPageLists{
  constructor() {
    this.breedsList = [];
    this.colorsList = [];
    this.gendersList = [];
    this.typesList = [];
  }
  breedsList: Breed[];
  colorsList: Color[];
  gendersList: Gender[];
  typesList: Type[];
}
