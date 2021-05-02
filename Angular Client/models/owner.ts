import {User, UserEntity} from './user';
import {FormGroup} from '@angular/forms';

export interface Owner {
  ownerId: number;
  idNumber: string;
  firstName: string;
  lastName: string;
  dateOfBirth: Date | null;
  city: string;
  city2: string;
  street: string;
  street2: string;
  houseNumber: string;
  houseNumber2: string;
  apartmentNumber: string;
  apartmentNumber2: string;
  postalCode: string;
  postalCode2: string;
  phone: string;
  mobile: string;
  mailBox: number;
  email: string;
  comment: string;
  createdDate: Date;
  userId: number;
  user: User;
}


export class OwnerEntity implements Owner{
  constructor() {
    this.apartmentNumber =  '';
    this.apartmentNumber2 =  '';
    this.city =  '';
    this.city2 =  '';
    this.comment =  '';
    this.createdDate = new Date();
    this.dateOfBirth = new Date();
    this.email =  '';
    this.firstName =  '';
    this.houseNumber = '';
    this.houseNumber2 = '';
    this.idNumber = '';
    this.lastName =  '';
    this.mailBox = 0;
    this.mobile =  '';
    this.ownerId = 0;
    this.phone =  '';
    this.postalCode =  '';
    this.postalCode2 =  '';
    this.street =  '';
    this.street2 =  '';
    this.user = new UserEntity();
    this.userId = 0;
  }

  apartmentNumber: string;
  apartmentNumber2: string;
  city: string;
  city2: string;
  comment: string;
  createdDate: Date;
  dateOfBirth: Date | null;
  email: string;
  firstName: string;
  houseNumber: string;
  houseNumber2: string;
  idNumber: string;
  lastName: string;
  mailBox: number;
  mobile: string;
  ownerId: number;
  phone: string;
  postalCode: string;
  postalCode2: string;
  street: string;
  street2: string;
  user: User;
  userId: number;

  setData(form: FormGroup): void{
    this.apartmentNumber = form.get('apartmentNumber')?.value;
    this.apartmentNumber2 = form.get('apartmentNumber2')?.value;
    this.city = form.get('city')?.value;
    this.city2 = form.get('city2')?.value;
    this.comment = form.get('comment')?.value;
    this.dateOfBirth = form.get('dateOfBirth')?.value ? new Date(form.get('dateOfBirth')?.value) : null;
    this.email = form.get('email')?.value;
    this.firstName = form.get('firstName')?.value;
    this.houseNumber = form.get('houseNumber')?.value;
    this.houseNumber2 = form.get('houseNumber2')?.value;
    this.idNumber = form.get('idNumber')?.value;
    this.lastName = form.get('lastName')?.value;
    this.mailBox = form.get('mailBox')?.value;
    this.mobile = form.get('mobile')?.value;
    this.ownerId = form.get('ownerId')?.value;
    this.phone = form.get('phone')?.value;
    this.postalCode = form.get('postalCode')?.value;;
    this.postalCode2 = form.get('postalCode2')?.value;
    this.street = form.get('street')?.value;
    this.street2 = form.get('street2')?.value;
    this.userId = form.get('userId')?.value;
  }
}
