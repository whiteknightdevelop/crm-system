import { Component, OnInit } from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {MessageService} from 'primeng/api';
import {SearchField} from '../../../models/search-field';
import { throwError} from 'rxjs';
import {
  ALPHABETIC_REGEX,
  ALPHANUMERIC_REGEX,
} from '../../../models/RegexPatterns';
import {SearchAnimalService} from './search-animal.service';
import {Animal, AnimalEntity} from '../../../models/animal';
import {SearchAnimalPage} from '../../../models/search-animal-page';
import {HttpErrorHandler} from '../../../error-handlers/http-error-handler.service';

@Component({
  selector: 'app-search-animal',
  templateUrl: './search-animal.component.html',
  styleUrls: ['./search-animal.component.css']
})
export class SearchAnimalComponent implements OnInit {

  constructor(private fb: FormBuilder, private searchAnimalService: SearchAnimalService,
              private messageService: MessageService,
              private httpErrorHandler: HttpErrorHandler) {
    this.pageIsLoaded = false;
    this.h1Header = 'חיפוש בע"ח';
    this.h1Description = 'דף חיפוש לפי פרטי בעל־חיים';
    this.animalFields = [];
    this.selectedAnimalField = new AnimalEntity();
    this.fieldsList = [];
    this.animal = new AnimalEntity();
    this.resultList = [];
    this.showProgressSpinner = false;
    this.tableDataIsLoaded = false;
    this.fieldId = 0;
    this.validationError = '';
  }

  pageIsLoaded: boolean;
  h1Header: string;
  h1Description: string;
  animalFields: Animal[];
  selectedAnimalField: Animal;
  fieldsList: SearchField[];
  animal: Animal;
  resultList: SearchAnimalPage[];
  showProgressSpinner: boolean;
  tableDataIsLoaded: boolean;
  fieldId: number;
  validationError: string;
  searchAnimalForm = this.fb.group({
    searchString: [null, Validators.compose([
      Validators.maxLength(45),
      Validators.pattern(ALPHABETIC_REGEX)])
    ],
    selectedField: [null],
  });

  get searchString(): any { return this.searchAnimalForm.get('searchString'); }
  get selectedField(): any { return this.searchAnimalForm.get('selectedField'); }

  initializeFieldList(): void {
    this.fieldsList = [
      {id: 0, field: 'שם'},
      {id: 1, field: 'סוג'},
      {id: 2, field: 'גזע'},
      {id: 3, field: 'צבע'},
      {id: 4, field: 'מין'},
      {id: 5, field: 'מספר שבב'},
    ];
  }

  ngOnInit(): void {
    this.initializeFieldList();
    this.validationError = 'אותיות בלבד!';
    this.pageIsLoaded = true;
  }

  dropdownOnChnage($event: any): void {
    this.fieldId = ($event.value as SearchField).id;
    this.setFormValidators();
  }

  onSubmit(): void {
    if (this.searchAnimalForm.valid) {
      this.tableDataIsLoaded = false;
      this.showProgressSpinner = true;
      this.setAnimalData();
      this.searchAnimalService.SearchAnimal(this.animal).subscribe(ans => {
        if (ans){
          this.resultList = ans as SearchAnimalPage[];
          this.tableDataIsLoaded = true;
        } else {
          throwError(new Error('onSearchAnimal Error'));
        }
      }, error => {
        this.showProgressSpinner = false;
        this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
      }, () => {
        this.showProgressSpinner = false;
      });
    }
  }

  private setAnimalData(): void {
    this.animal = new AnimalEntity();
    switch (this.fieldId) {
      case 0: {
        // name
        this.animal.name = String(this.searchString.value);
        break;
      }
      case 1: {
        // type
        this.animal.type = String(this.searchString.value);
        break;
      }
      case 2: {
        // breed
        this.animal.breed = String(this.searchString.value);
        break;
      }
      case 3: {
        // color
        this.animal.color = String(this.searchString.value);
        break;
      }
      case 4: {
        // gender
        this.animal.gender = String(this.searchString.value);
        break;
      }
      case 5: {
        // chipNumber
        this.animal.chipNumber = String(this.searchString.value);
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
        // name
        this.searchString.setValidators(Validators.compose([
          Validators.required,
          Validators.maxLength(45),
          Validators.pattern(ALPHABETIC_REGEX)
        ]));
        this.validationError = 'אותיות בלבד!';
        break;
      }
      case 1: {
        // type
        this.searchString.setValidators(Validators.compose([
          Validators.required,
          Validators.maxLength(45),
          Validators.pattern(ALPHABETIC_REGEX)
        ]));
        this.validationError = 'אותיות בלבד!';
        break;
      }
      case 2: {
        // breed
        this.searchString.setValidators(Validators.compose([
          Validators.required,
          Validators.maxLength(45),
          Validators.pattern(ALPHABETIC_REGEX)
        ]));
        this.validationError = 'אותיות בלבד!';
        break;
      }
      case 3: {
        // color
        this.searchString.setValidators(Validators.compose([
          Validators.required,
          Validators.maxLength(45),
          Validators.pattern(ALPHABETIC_REGEX)
        ]));
        this.validationError = 'אותיות בלבד!';
        break;
      }
      case 4: {
        // gender
        this.searchString.setValidators(Validators.compose([
          Validators.required,
          Validators.maxLength(45),
          Validators.pattern(ALPHABETIC_REGEX)
        ]));
        this.validationError = 'אותיות בלבד!';
        break;
      }
      case 5: {
        // chipNumber
        this.searchString.setValidators(Validators.compose([
          Validators.required,
          Validators.maxLength(45),
          Validators.pattern(ALPHANUMERIC_REGEX)
        ]));
        this.validationError = 'מספרים ואותיות בלבד!';
        break;
      }
      default: {
        break;
      }
    }
    this.searchString.updateValueAndValidity();
  }
}
