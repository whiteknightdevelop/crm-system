export interface VisitedOwnersItem {
  visitId: number;
  animalId: number;
  ownerId: number;
  visitTime: Date;
  numOfDaysPassed: number;
  name: string;
  type: string;
  breed: string;
  color: string;
  gender: string;
  active: boolean;
  sterilized: boolean;
  chipNumber: string;
  firstName: string;
  lastName: string;
  city: string;
  street: string;
  houseNumber: string;
  apartmentNumber: string;
  phone: string;
}

export class VisitedOwnersItemEntity implements VisitedOwnersItem{
  constructor() {
    this.visitId = 0;
    this.animalId = 0;
    this.ownerId = 0;
    this.visitTime = new Date();
    this.numOfDaysPassed = 0;
    this.name = '';
    this.type = '';
    this.breed = '';
    this.color = '';
    this.gender = '';
    this.active = false;
    this.sterilized = false;
    this.chipNumber = '';
    this.firstName = '';
    this.lastName = '';
    this.city = '';
    this.street = '';
    this.houseNumber = '';
    this.apartmentNumber = '';
    this.phone = '';
  }

  visitId: number;
  animalId: number;
  ownerId: number;
  visitTime: Date;
  numOfDaysPassed: number;
  name: string;
  type: string;
  breed: string;
  color: string;
  gender: string;
  active: boolean;
  sterilized: boolean;
  chipNumber: string;
  firstName: string;
  lastName: string;
  city: string;
  street: string;
  houseNumber: string;
  apartmentNumber: string;
  phone: string;
}
