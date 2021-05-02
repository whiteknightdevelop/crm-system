import {User, UserEntity} from './user';
import {FormGroup} from '@angular/forms';
import {AnimalPage} from './animal-page';

export interface Animal {
  animalId: number;
  ownerId: number;
  name: string;
  type: string;
  breed: string;
  color: string;
  gender: string;
  dateOfBirth: Date | null;
  active: boolean;
  sterilized: boolean;
  dateOfSterilization: Date | null;
  chipNumber: string;
  chipMarkDate: Date | null;
  comment: string;
  createdDate: Date;
  user: User | null;
}


export class AnimalEntity implements Animal{
  constructor() {
    this.active = false;
    this.animalId = 0;
    this.breed = '';
    this.chipMarkDate = new Date();
    this.chipNumber = '';
    this.color = '';
    this.comment = '';
    this.createdDate = new Date();
    this.dateOfBirth = new Date();
    this.dateOfSterilization = new Date();
    this.gender = '';
    this.name = '';
    this.ownerId = 0;
    this.sterilized = false;
    this.type = '';
    this.user = new UserEntity();
  }

  active: boolean;
  animalId: number;
  breed: string;
  chipMarkDate: Date | null;
  chipNumber: string;
  color: string;
  comment: string;
  createdDate: Date;
  dateOfBirth: Date | null;
  dateOfSterilization: Date | null;
  gender: string;
  name: string;
  ownerId: number;
  sterilized: boolean;
  type: string;
  user: User | null;

  setData(form: FormGroup, animalPage: AnimalPage | null): void{
    this.active = form.get('active')?.value;
    this.animalId = animalPage ? animalPage.animal.animalId : 0;
    this.breed = form.get('breedName')?.value.breedName;
    this.chipMarkDate = form.get('chipMarkDate')?.value ? new Date(form.get('chipMarkDate')?.value) : null;
    this.chipNumber = form.get('chipNumber')?.value;
    this.color = form.get('color')?.value.color;
    this.comment = form.get('comment')?.value;
    this.dateOfBirth = form.get('dateOfBirth')?.value ? new Date(form.get('dateOfBirth')?.value) : null;
    this.dateOfSterilization = form.get('dateOfSterilization')?.value ? new Date(form.get('dateOfSterilization')?.value) : null;
    this.gender = form.get('gender')?.value.gender;
    this.name = form.get('name')?.value;
    this.ownerId = form.get('ownerId')?.value;
    this.sterilized = form.get('sterilized')?.value;
    this.type = form.get('type')?.value.type;
    this.user = animalPage ? animalPage.animal.user : null;
  }
}


