import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators} from '@angular/forms';
import { Gender } from '../../models/gender';
import { Type } from '../../models/type';
import { distinctUntilChanged, switchMap } from 'rxjs/operators';
import {ConfirmationService, MenuItem, MessageService} from 'primeng/api';
import {AnimalService} from './animal.service';
import {ActivatedRoute, Router} from '@angular/router';
import {HttpErrorHandler} from '../../error-handlers/http-error-handler.service';
import {ALPHABETIC_REGEX, ALPHANUMERIC_REGEX, DATE_REGEX, NUMERIC_REGEX} from '../../models/RegexPatterns';
import {Observable} from 'rxjs';
import {AnimalPage, AnimalPageEntity} from '../../models/animal-page';
import {Breed} from '../../models/breed';
import {Color} from '../../models/color';
import {Animal, AnimalEntity} from '../../models/animal';
import {PreventiveReminder} from '../../models/preventive-reminder';
import {HelperService} from '../../helper.service';
import { PrimeNGConfig } from 'primeng/api';
import {formatDate} from '@angular/common';
import {PrintService} from '../print/print.service';
import {Reminder} from '../../models/reminder';
import {AuthService} from '../../auth/auth.service';
import {Angulartics2} from 'angulartics2';

@Component({
  selector: 'app-animal',
  templateUrl: './animal.component.html',
  styleUrls: ['./animal.component.css'],
  providers: [AnimalService, MessageService],
})
export class AnimalComponent implements OnInit {

  constructor(private fb: FormBuilder, private animalService: AnimalService,
              private route: ActivatedRoute,
              private router: Router,
              private confirmationService: ConfirmationService,
              private messageService: MessageService,
              private httpErrorHandler: HttpErrorHandler,
              private helperService: HelperService,
              private primengConfig: PrimeNGConfig,
              private printService: PrintService,
              private authService: AuthService,
              private angulartics2: Angulartics2) {
    this.menuItems = [];
    this.showAge = false;
    this.pageIsLoaded = false;
    this.showProgress = false;
    this.inCreateNewMode = false;
    this.h1Header = '';
    this.h1Description = '';
    this.animalPage$ = new Observable<AnimalPage>();
    this.animalPage = new AnimalPageEntity();
    this.typesList = [];
    this.gendersList = [];
    this.breedsList = [];
    this.breedsListFiltered = [];
    this.colorsList = [];
    this.remindersList = [];
    this.preventiveRemindersList = [];
    this.checkboxTest = false;
    this.ageYears = 0;
    this.ageMonths = 0;
  }

  animalDetailsForm = this.fb.group({
    active: false,
    name: ['', Validators.compose([
      Validators.required,
      Validators.maxLength(45),
      Validators.pattern(ALPHABETIC_REGEX)])
    ],
    dateOfBirth: [formatDate(0, 'yyyy-MM-dd', 'he'), Validators.compose([Validators.pattern(DATE_REGEX)])],
    type: ['', Validators.compose([
      Validators.required,
      Validators.maxLength(45)])
    ],
    gender: ['', Validators.compose([
      Validators.required,
      Validators.maxLength(45)])
    ],
    breedName: ['', Validators.compose([
      Validators.required,
      Validators.maxLength(45)])
    ],
    color: ['', Validators.compose([
      Validators.required,
      Validators.maxLength(45)])
    ],
    sterilized: false,
    dateOfSterilization: [formatDate(0, 'yyyy-MM-dd', 'he'), Validators.compose([Validators.pattern(DATE_REGEX)])],
    chipNumber: [null, Validators.compose([
      Validators.maxLength(45),
      Validators.pattern(ALPHANUMERIC_REGEX)])
    ],
    chipMarkDate: [formatDate(0, 'yyyy-MM-dd', 'he'), Validators.compose([Validators.pattern(DATE_REGEX)])],
    comment: ['', Validators.compose([
      Validators.maxLength(500)])
    ],
    animalId: 0,
    ownerId: 0,
  });

  get dateOfBirth(): any { return this.animalDetailsForm.get('dateOfBirth'); }
  get name(): any { return this.animalDetailsForm.get('name'); }
  get type(): any { return this.animalDetailsForm.get('type'); }
  get gender(): any { return this.animalDetailsForm.get('gender'); }
  get breedName(): any { return this.animalDetailsForm.get('breedName'); }
  get color(): any { return this.animalDetailsForm.get('color'); }
  get sterilized(): any { return this.animalDetailsForm.get('sterilized'); }
  get dateOfSterilization(): any { return this.animalDetailsForm.get('dateOfSterilization'); }
  get chipNumber(): any { return this.animalDetailsForm.get('chipNumber'); }
  get chipMarkDate(): any { return this.animalDetailsForm.get('chipMarkDate'); }
  get comment(): any { return this.animalDetailsForm.get('comment'); }
  get animalId(): any { return this.animalDetailsForm.get('animalId'); }
  get ownerId(): any { return this.animalDetailsForm.get('ownerId'); }

  menuItems: MenuItem[];
  showAge: boolean;
  pageIsLoaded: boolean;
  showProgress: boolean;
  inCreateNewMode = false;
  h1Header: string;
  h1Description: string;

  private animalPage$: Observable<AnimalPage>;
  animalPage: AnimalPage;
  typesList: { type: Type }[];
  gendersList: { gender: Gender }[];
  breedsList: Breed[];
  breedsListFiltered: Breed[];
  colorsList: { color: Color }[];
  remindersList: { reminder: Reminder }[];
  preventiveRemindersList: PreventiveReminder[];
  checkboxTest: boolean;
  ageYears: number;
  ageMonths: number;

  ngOnInit(): void {
    this.primengConfig.ripple = true;
    this.showProgress = true;

    this.route.data.subscribe(data => {
      switch (data.kind) {
        case 'new': {
          this.inCreateNewMode = true;
          this.h1Header = 'בע"ח חדש';
          this.h1Description = 'יצירת בע"ח חדש';
          break;
        }
        case 'edit': {
          this.inCreateNewMode = false;
          this.h1Header = 'פרטי בע"ח';
          this.h1Description = 'דף המכיל פרטים של הבע"ח';
          break;
        }
        default: {
          break;
        }
      }
    });

    if (this.inCreateNewMode){
      this.animalDetailsForm.patchValue({
        ownerId: this.route.snapshot.paramMap.get('ownerid'),
        dateOfBirth: null,
        dateOfSterilization: null,
        chipMarkDate: null
      });
      this.animalService.getAnimalLists().subscribe(animalPageLists => {
        this.typesList = animalPageLists.typesList.map((item: any) => ({type: item}));
        this.gendersList = animalPageLists.gendersList.map((item: any) => ({gender: item}));
        this.breedsList = animalPageLists.breedsList.map((item: any) => ({breedName: item.breedName, type: item.type}));
        this.colorsList = animalPageLists.colorsList.map((item: any) => ({color: item}));
      }, error => {
        this.showProgress = false;
        this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
      });
      this.showProgress = false;
      this.pageIsLoaded = true;
    }else{
      this.animalPage$ = this.route.paramMap.pipe(
        switchMap(params => {
          const animalId = Number(params.get('id'));
          return this.animalService.getAnimal(animalId);
        })
      );
      this.animalPage$.subscribe(data => {
        this.animalPage = data;
        this.typesList = this.animalPage.lists.typesList.map(item => ({type: item}));
        this.gendersList = this.animalPage.lists.gendersList.map(item => ({gender: item}));
        this.breedsList = this.animalPage.lists.breedsList.map((item: any) => ({breedName: item.breedName, type: item.type}));
        this.breedsListFiltered = this.breedsList;
        this.colorsList = this.animalPage.lists.colorsList.map(item => ({color: item}));
        this.remindersList = this.animalPage.remindersList.map((item: any) => ({reminder: item}));
        this.preventiveRemindersList = this.animalPage.preventiveRemindersList;
        this.animalDetailsForm.patchValue({
          active: this.animalPage.animal.active,
          name: this.animalPage.animal.name,
          dateOfBirth: this.animalPage.animal.dateOfBirth ? formatDate(this.animalPage.animal.dateOfBirth, 'yyyy-MM-dd', 'he') : null,
          type: ({type: this.animalPage.animal.type}),
          gender: ({gender: this.animalPage.animal.gender}),
          breedName: {breedName: this.animalPage.animal.breed, type: this.animalPage.animal.type},
          color: ({color: this.animalPage.animal.color}),
          sterilized: this.animalPage.animal.sterilized,
          dateOfSterilization: this.animalPage.animal.dateOfSterilization ? formatDate(this.animalPage.animal.dateOfSterilization, 'yyyy-MM-dd', 'he') : null,
          chipNumber: this.animalPage.animal.chipNumber,
          chipMarkDate: this.animalPage.animal.chipMarkDate ? formatDate(this.animalPage.animal.chipMarkDate, 'yyyy-MM-dd', 'he') : null,
          comment: this.animalPage.animal.comment,
          animalId: this.animalPage.animal.animalId,
          ownerId: this.animalPage.animal.ownerId,
        });
        this.filterBreedsListByAnimalType(this.type.value.type);
        this.setAnimalAge(this.dateOfBirth.value);
        this.changeDisabledMenuStatus(true);
        this.showProgress = false;
        this.pageIsLoaded = true;
        this.onChanges();
      }, error => {
        this.showProgress = false;
        this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
      });
    }
  }

  filterBreedsListByAnimalType(type: string): void {
    switch (type) {
      case 'אוגר': {
        this.breedsListFiltered = this.breedsList.filter((item) => item.type === 'אוגר')
          .sort((a, b) => this.sortBreedNames(a, b));
        break;
      }
      case 'ארנב': {
        this.breedsListFiltered = this.breedsList.filter((item) => item.type === 'ארנב')
          .sort((a, b) => this.sortBreedNames(a, b));
        break;
      }
      case 'זוחל': {
        this.breedsListFiltered = this.breedsList.filter((item) => item.type === 'זוחל')
          .sort((a, b) => this.sortBreedNames(a, b));
        break;
      }
      case 'חולדה': {
        this.breedsListFiltered = this.breedsList.filter((item) => item.type === 'חולדה')
          .sort((a, b) => this.sortBreedNames(a, b));
        break;
      }
      case 'חמוס': {
        this.breedsListFiltered = this.breedsList.filter((item) => item.type === 'חמוס')
          .sort((a, b) => this.sortBreedNames(a, b));
        break;
      }
      case 'חתול': {
        this.breedsListFiltered = this.breedsList.filter((item) => item.type === 'חתול')
          .sort((a, b) => this.sortBreedNames(a, b));
        break;
      }
      case 'כלב': {
        this.breedsListFiltered = this.breedsList.filter((item) => item.type === 'כלב')
          .sort((a, b) => this.sortBreedNames(a, b));
        break;
      }
      case 'לא מוגדר': {
        this.breedsListFiltered = this.breedsList.filter((item) => item.type === 'לא מוגדר');
        break;
      }
      case 'לגומורף': {
        this.breedsListFiltered = this.breedsList.filter((item) => item.type === 'לגומורף')
          .sort((a, b) => this.sortBreedNames(a, b));
        break;
      }
      case 'מכרסם': {
        this.breedsListFiltered = this.breedsList.filter((item) => item.type === 'מכרסם')
          .sort((a, b) => this.sortBreedNames(a, b));
        break;
      }
      case 'עוף': {
        this.breedsListFiltered = this.breedsList.filter((item) => item.type === 'עוף')
          .sort((a, b) => this.sortBreedNames(a, b));
        break;
      }
      case 'ציפור נוי': {
        this.breedsListFiltered = this.breedsList.filter((item) => item.type === 'ציפור נוי')
          .sort((a, b) => this.sortBreedNames(a, b));
        break;
      }
      case 'תוכי': {
        this.breedsListFiltered = this.breedsList.filter((item) => item.type === 'תוכי')
          .sort((a, b) => this.sortBreedNames(a, b));
        break;
      }
      default: {
        this.breedsListFiltered = [{breedName: 'לא מוגדר', type: ''}];
        break;
      }
    }
  }

  setAnimalAge(dateOfBirth: Date): void {
    const date = new Date(dateOfBirth);
    if (dateOfBirth === null || (Object.prototype.toString.call(dateOfBirth) === '[object Date]' || isNaN(date.getDate()))) {
      this.showAge = false;
    }else {
      const currentYear = (new Date()).getFullYear();
      const currentMonth = (new Date()).getMonth();
      const dateOfBirthYear = new Date(dateOfBirth).getFullYear();
      const dateOfBirthMonth  = new Date(dateOfBirth).getMonth();

      this.ageYears = currentYear - dateOfBirthYear;
      if (currentMonth >= dateOfBirthMonth) {
        this.ageMonths = currentMonth - dateOfBirthMonth;

      }else {
        this.ageYears--;
        this.ageMonths = 12 + currentMonth - dateOfBirthMonth;
      }
      this.showAge = true;
    }
  }

  onChanges(): void {
    this.animalDetailsForm.valueChanges.pipe(
      distinctUntilChanged(),
    ).subscribe(() => {
        if (this.animalDetailsForm.valid){
          this.changeDisabledMenuStatus(false);
        }else {
          this.changeDisabledMenuStatus(true);
        }
    }, error => {
      this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
    });
  }


  sortBreedNames(a: Breed, b: Breed): any {
    const nameA = a.breedName.toUpperCase();
    const nameB = b.breedName.toUpperCase();
    return (nameA < nameB) ? -1 : (nameA > nameB) ? 1 : 0;
  }

  onAdd(): void {
    if (this.animalDetailsForm.valid){
      const animal = this.FormToAnimalMapper(this.animalDetailsForm, null);
      this.animalService.addAnimal(animal).subscribe(animalIdAdded => {
        if (Number(animalIdAdded)){
          this.router.navigateByUrl('/animal/' + animalIdAdded);
        }
      }, error => {
        this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
      });
    }
  }

  private changeDisabledMenuStatus(isDisabled: boolean): void {
    this.menuItems = [
      {
        label: 'שמור',
        icon: 'pi pi-fw pi-save',
        command: () => this.UpdateServerWithAnimalChanges(),
        disabled: isDisabled
      },
      {
        label: 'מעקב',
        icon: 'pi pi-fw pi-book',
        command: () => this.goToFollowUpPage(),
      },
      {
        label: 'אישורים',
        icon: 'pi pi-fw pi-file',
        items: [
          {
            label: 'אישור כלבת',
            icon: 'pi pi-fw pi-print',
            command: () => this.printRabiesVaccine(),
          },
          {
            label: 'אישור עיקור',
            icon: 'pi pi-fw pi-print',
            command: () => this.printSterilizationPermit(),
          },
          {
            label: 'בקשת רישיון לכלב',
            icon: 'pi pi-fw pi-print',
            command: () => this.printDogLicence(),
          },
        ]
      },
      {
        label: 'אפשרויות',
        icon: 'pi pi-fw pi-cog',
        items: [
          {
            label: 'בע"ח חדש',
            icon: 'pi pi-fw pi-plus',
            command: () => this.goToNewAnimalpage(),
          },
          {
            label: 'מחיקה',
            icon: 'pi pi-fw pi-trash',
            command: () => this.onDelete()
          },
        ]
      },
      {
        label: 'הדפסה',
        icon: 'pi pi-fw pi-print',
        command: () => this.printAnimal(),
      },
      {
        label: 'חזרה לבעלים',
        icon: 'pi pi-fw pi-arrow-left',
        command: () => this.goBackToOwnerpage(),
      },
    ];
  }

  private goToFollowUpPage(): void {
    this.router.navigate(['/followup', { animalId: this.animalId.value }]);
  }

  printRabiesVaccine(): void  {
    this.printService
      .printDocument('rabies-vaccine', { animalid: this.animalId.value });
  }

  printSterilizationPermit(): void  {
    this.printService
      .printDocument('sterilization-permit', { animalid: this.animalId.value });
  }

  printDogLicence(): void  {
    this.printService
      .printDocument('dog-licence', { animalid: this.animalId.value });
  }

  printAnimal(): void  {
    this.printService
      .printDocument('animal-print', { animalid: this.animalId.value });
  }

  private UpdateServerWithAnimalChanges(): void {
    this.animalService.updateAnimal(this.FormToAnimalMapper(this.animalDetailsForm, this.animalPage)).subscribe(data => {
      if (data) {
        this.showSaveChangesMsg();
        this.setAnimalAge(this.dateOfBirth.value);
        this.changeDisabledMenuStatus(true);
      }
    }, error => {
      this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
    });
  }

  onDelete(): void {
    this.confirmationService.confirm({
      header: 'מחיקת בע"ח',
      message: 'האם הינך בטוח כי ברצונך למחוק בע"ח זה?',
      icon: 'pi pi-exclamation-triangle',
      acceptLabel: 'מחק',
      rejectLabel: 'בטל',
      acceptIcon: 'pi pi-trash',
      acceptButtonStyleClass: 'confirmDialogAcceptButton',
      accept: () => {
        this.animalService.deleteAnimal(this.animalPage.animal).subscribe(ans => {
          if (ans){
            this.messageService.add({severity: 'success', summary: 'הבע"ח נמחק!', detail: '' });
            setTimeout(() =>
            {
              this.router.navigateByUrl('/owner/' + this.animalPage.animalOwner.ownerId);
            }, 3000);
          }
        }, error => {
          this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
        });
      },
      reject: () => {}
    });
  }

  showSaveChangesMsg(): void {
    this.messageService.add({key: 'showSaveChanges', severity: 'success', summary: 'השינויים נשמרו בהצלחה', life: 500});
  }

  private goToNewAnimalpage(): void {
    this.router.navigate(['/animal', { ownerid: this.ownerId.value }]);
  }

  private goBackToOwnerpage(): void {
    this.router.navigate(['/owner', this.ownerId.value ]);
  }

  sterilizedToggleButton(): void {
    if (this.sterilized.value) {
      this.animalDetailsForm.patchValue({
        dateOfSterilization: formatDate(new Date(), 'yyyy-MM-dd', 'he')
      });
    }else {
      this.animalDetailsForm.patchValue({
        dateOfSterilization: null
      });
    }
  }

  FormToAnimalMapper(form: FormGroup, animalPage: AnimalPage | null): Animal {
    const animal = new AnimalEntity();
    animal.setData(form, animalPage);
    animal.user = this.authService.getUser();
    return animal;
  }

  onTypeChange(): void {
    this.filterBreedsListByAnimalType(this.type.value.type);
  }
}

