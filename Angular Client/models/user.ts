import {FormGroup} from '@angular/forms';

export interface User {
  userId: number;
  firstName: string;
  lastName: string;
  userName: string;
  email: string;
  password: string;
  createdDate: Date;
  gender: string;
  license: string;
  archive: boolean;
}

export class UserEntity implements User{
  constructor() {
    this.archive = false;
    this.createdDate = new Date();
    this.email = '';
    this.firstName = '';
    this.gender = '';
    this.lastName = '';
    this.license = '';
    this.password = '';
    this.userId = 0;
    this.userName = '';
    this.phoneNumber = '';
  }
  archive: boolean;
  createdDate: Date;
  email: string;
  firstName: string;
  gender: string;
  lastName: string;
  license: string;
  password: string;
  userId: number;
  userName: string;
  phoneNumber: string;

  setData(form: FormGroup): void{
    this.userName = form.get('userName')?.value;
    this.password = form.get('password')?.value;
    this.gender = form.get('gender')?.value.gender;
  }
}
