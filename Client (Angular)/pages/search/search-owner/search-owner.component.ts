import { Component, OnInit } from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {
  ALPHABETIC_REGEX,
  EMAIL_REGEX, NUMERIC_REGEX,
  PASSPORT_REGEX,
} from '../../../models/RegexPatterns';
import {Owner, OwnerEntity} from '../../../models/owner';
import {SearchField} from '../../../models/search-field';
import {Subject, throwError} from 'rxjs';
import {SearchOwnerService} from './search-owner.service';
import {MessageService} from 'primeng/api';
import {HttpErrorHandler} from '../../../error-handlers/http-error-handler.service';
import {AuthService} from '../../../auth/auth.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-search-owner',
  templateUrl: './search-owner.component.html',
  styleUrls: ['./search-owner.component.css'],
})
export class SearchOwnerComponent implements OnInit {

  constructor(private fb: FormBuilder, private searchOwnerService: SearchOwnerService,
              private messageService: MessageService,
              private httpErrorHandler: HttpErrorHandler,
              public router: Router,
              public authService: AuthService) {
    this.pageIsLoaded = false;
    this.h1Header = 'חיפוש בעלים';
    this.h1Description = 'דף חיפוש לפי פרטי בעלים';
    this.ownerFields = [];
    this.selectedOwnerField = new OwnerEntity();
    this.fieldsList = [];
    this.owner = new OwnerEntity();
    this.resultList = [];
    this.showProgressSpinner = false;
    this.tableDataIsLoaded = false;
    this.fieldId = 0;
    this.validationError = '';
  }

  pageIsLoaded: boolean;
  h1Header: string;
  h1Description: string;
  ownerFields: Owner[];
  selectedOwnerField: Owner;
  fieldsList: SearchField[];
  owner: Owner;
  resultList: Owner[];
  showProgressSpinner: boolean;
  tableDataIsLoaded: boolean;
  fieldId: number;
  validationError: string;
  searchTextChanged = new Subject<string>();
  searchOwnerForm = this.fb.group({
    searchString: [null, Validators.compose([
      Validators.maxLength(45),
      Validators.pattern(NUMERIC_REGEX)])
    ],
    selectedField: [null],
  });

  get searchString(): any { return this.searchOwnerForm.get('searchString'); }
  get selectedField(): any { return this.searchOwnerForm.get('selectedField'); }

  initializeFieldList(): void {
    this.fieldsList = [
      {id: 0, field: 'שם משפחה'},
      {id: 1, field: 'ת.ז.'},
      {id: 2, field: 'שם פרטי'},
      {id: 3, field: 'ישוב'},
      {id: 4, field: 'רחוב'},
      {id: 5, field: 'טלפון'},
      {id: 6, field: 'דוא"ל'},
      {id: 7, field: 'מספר תיק'},
      ];
  }

  ngOnInit(): void {
    this.initializeFieldList();
    this.validationError = 'מספרים בלבד!';
    this.pageIsLoaded = true;
  }

  searchInputOnChange($event: Event): void {
    this.setFormValidators();
  }

  dropdownOnChnage($event: any): void {
    this.fieldId = ($event.value as SearchField).id;
    this.setFormValidators();
    const f = this.searchOwnerForm.valid;
  }

  onSubmit(): void {
    if (this.searchOwnerForm.valid) {
      this.tableDataIsLoaded = false;
      this.showProgressSpinner = true;

      this.setOwnerData();
      this.searchOwnerService.SearchOwner(this.owner).subscribe(ans => {
        if (ans){
          this.resultList = ans as Owner[];
          this.tableDataIsLoaded = true;
        } else {
          throwError(new Error('onSearchOwner Error'));
        }
      }, error => {
        this.showProgressSpinner = false;
        this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
      }, () => {
        this.showProgressSpinner = false;
      });
    }
  }

  private setOwnerData(): void {
    this.owner = new OwnerEntity();
    switch (this.fieldId) {
      case 0: {
        // lastName
        this.owner.lastName = String(this.searchString.value);
        break;
      }
      case 1: {
        // idNumber
        this.owner.idNumber = String(this.searchString.value);
        break;
      }
      case 2: {
        // firstName
        this.owner.firstName = String(this.searchString.value);
        break;
      }

      case 3: {
        // city
        this.owner.city = String(this.searchString.value);
        break;
      }
      case 4: {
        // street
        this.owner.street = String(this.searchString.value);
        break;
      }
      case 5: {
        // phone
        this.owner.phone = String(this.searchString.value);
        break;
      }
      case 6: {
        // email
        this.owner.email = String(this.searchString.value);
        break;
      }
      case 7: {
        // ownerId
        this.owner.ownerId = Number(this.searchString.value);
        break;
      }
      default: {
        break;
      }
    }
  }

  private setFormValidators(): void {
    this.searchString.setValidators(null);
    switch (this.fieldId) {
      case 0: {
        // lastName
        this.searchString.setValidators(Validators.compose([
          Validators.required,
          Validators.maxLength(45),
          Validators.pattern(ALPHABETIC_REGEX)
        ]));
        this.validationError = 'אותיות בלבד!';
        break;
      }
      case 1: {
        // idNumber
        this.searchString.setValidators(Validators.compose([
          Validators.required,
          Validators.maxLength(45),
          Validators.pattern(PASSPORT_REGEX)
        ]));
        this.validationError = 'מספרים ואותיות בלבד!';
        break;
      }
      case 2: {
        // firstName
        this.searchString.setValidators(Validators.compose([
          Validators.required,
          Validators.maxLength(45),
          Validators.pattern(ALPHABETIC_REGEX)
        ]));
        this.validationError = 'אותיות בלבד!';
        break;
      }
      case 3: {
        // city
        this.searchString.setValidators(Validators.compose([
          Validators.required,
          Validators.maxLength(45),
          Validators.pattern(ALPHABETIC_REGEX)
        ]));
        this.validationError = 'אותיות בלבד!';
        break;
      }
      case 4: {
        // street
        this.searchString.setValidators(Validators.compose([
          Validators.required,
          Validators.maxLength(45),
          Validators.pattern(ALPHABETIC_REGEX)
        ]));
        this.validationError = 'אותיות בלבד!';
        break;
      }
      case 5: {
        // phone
        this.searchString.setValidators(Validators.compose([
          Validators.required,
          Validators.maxLength(45),
        ]));
        this.validationError = 'טלפון לא תקין!';
        break;
      }
      case 6: {
        // email
        this.searchString.setValidators(Validators.compose([
          Validators.required,
          Validators.maxLength(45),
          Validators.pattern(EMAIL_REGEX)
        ]));
        this.validationError = 'אותיות בלבד!';
        break;
      }
      case 7: {
        // ownerId
        this.searchString.setValidators(Validators.compose([
          Validators.required,
          Validators.maxLength(10),
          Validators.pattern(NUMERIC_REGEX)
        ]));
        this.validationError = 'מספרים באורך עד 10 בלבד!';
        break;
      }
      default: {
        break;
      }
    }
    this.searchString.updateValueAndValidity();
  }
}
