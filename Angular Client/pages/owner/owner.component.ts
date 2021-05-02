import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import { OwnerService } from './owner.service';
import { Animal } from '../../models/animal';
import {Observable, throwError} from 'rxjs';
import { distinctUntilChanged, switchMap} from 'rxjs/operators';
import {ActivatedRoute, Router} from '@angular/router';
import {Owner} from '../../models/owner';
import { PrimeNGConfig } from 'primeng/api';
import { MenuItem } from 'primeng/api';
import {OwnerPage, OwnerPageEntity} from '../../models/owner-page';
import {formatDate} from '@angular/common';
import {
  PASSPORT_REGEX,
  ALPHABETIC_REGEX,
  ALPHANUMERICWITHHEBREW_REGEX,
  NUMERICEMPTYORLENGTH7_REGEX,
  PHONENUMBER_REGEX,
  EMAIL_REGEX, DATE_REGEX
} from '../../models/RegexPatterns';
import {ConfirmationService, MessageService} from 'primeng/api';
import {HelperService} from '../../helper.service';
import {OwnerEntity} from '../../models/owner';
import {HttpErrorHandler} from '../../error-handlers/http-error-handler.service';
import {AuthService} from '../../auth/auth.service';

@Component({
  selector: 'app-owner',
  templateUrl: './owner.component.html',
  styleUrls: ['./owner.component.css'],
  providers: [OwnerService, MessageService],
})
export class OwnerComponent implements OnInit {
  constructor(private fb: FormBuilder, private ownerService: OwnerService,
              private route: ActivatedRoute,
              private router: Router,
              private httpErrorHandler: HttpErrorHandler,
              private confirmationService: ConfirmationService,
              private messageService: MessageService,
              private helperService: HelperService,
              private primengConfig: PrimeNGConfig,
              private authService: AuthService
  ) {
    this.menuItems = [];
    this.pageIsLoaded = false;
    this.showProgress = false;
    this.ownerPage$ = new Observable<OwnerPage>();
    this.ownerPage = new OwnerPageEntity();
    this.owner = new OwnerEntity();
    this.animals = [];
    this.inCreateNewMode = false;
    this.h1Header = '';
    this.h1Description = '';
    this.showComment = false;
    this.dateValue = new Date();
  }

  ownerDetailsForm = this.fb.group({
    idNumber: [null, Validators.compose([
      Validators.required,
      Validators.maxLength(45),
      Validators.pattern(PASSPORT_REGEX)])
    ],
    firstName: [null, Validators.compose([
      Validators.required,
      Validators.maxLength(45),
      Validators.pattern(ALPHABETIC_REGEX)])
    ],
    lastName: [null, Validators.compose([
      Validators.required,
      Validators.maxLength(45),
      Validators.pattern(ALPHABETIC_REGEX)])
    ],
    dateOfBirth: [formatDate(0, 'yyyy-MM-dd', 'he'), Validators.compose([
      Validators.pattern(DATE_REGEX)
      ])
    ],
    createdDate: [formatDate(0, 'yyyy-MM-dd', 'he'), Validators.compose([
      Validators.pattern(DATE_REGEX)
    ])
    ],
    city: [null, Validators.compose([
      Validators.required,
      Validators.maxLength(45),
      Validators.pattern(ALPHABETIC_REGEX)])
    ],
    city2: [null, Validators.compose([
      Validators.maxLength(45),
      Validators.pattern(ALPHABETIC_REGEX)])
    ],
    street: [null, Validators.compose([
      Validators.maxLength(45),
      Validators.pattern(ALPHABETIC_REGEX)])
    ],
    street2: [null, Validators.compose([
      Validators.maxLength(45),
      Validators.pattern(ALPHABETIC_REGEX)])
    ],
    houseNumber: [null, Validators.compose([
      Validators.maxLength(45),
      Validators.pattern(ALPHANUMERICWITHHEBREW_REGEX)])
    ],
    houseNumber2: [null, Validators.compose([
      Validators.maxLength(45),
      Validators.pattern(ALPHANUMERICWITHHEBREW_REGEX)])
    ],
    apartmentNumber: [null, Validators.compose([
      Validators.maxLength(45),
      Validators.pattern(ALPHANUMERICWITHHEBREW_REGEX)])
    ],
    apartmentNumber2: [null, Validators.compose([
      Validators.maxLength(45),
      Validators.pattern(ALPHANUMERICWITHHEBREW_REGEX)])
    ],
    postalCode: [null, Validators.compose([
      Validators.maxLength(45),
      Validators.pattern(NUMERICEMPTYORLENGTH7_REGEX)])
    ],
    postalCode2: [null, Validators.compose([
      Validators.maxLength(45),
      Validators.pattern(NUMERICEMPTYORLENGTH7_REGEX)])
    ],
    phone: [null, Validators.compose([
      Validators.required,
      Validators.maxLength(45),
      Validators.pattern(PHONENUMBER_REGEX)])
    ],
    mobile: [null, Validators.compose([
      Validators.maxLength(45),
      Validators.pattern(PHONENUMBER_REGEX)])
    ],
    mailBox: 0,
    email: [null, Validators.compose([Validators.pattern(EMAIL_REGEX), Validators.maxLength(45)])],
    comment: [null, Validators.compose([Validators.maxLength(500)])],
    ownerId: 0,
    userId: 0,
  });

  get ownerId(): any { return this.ownerDetailsForm.get('ownerId'); }
  get idNumber(): any { return this.ownerDetailsForm.get('idNumber'); }
  get lastName(): any { return this.ownerDetailsForm.get('lastName'); }
  get firstName(): any { return this.ownerDetailsForm.get('firstName'); }
  get phone(): any { return this.ownerDetailsForm.get('phone'); }
  get mobile(): any { return this.ownerDetailsForm.get('mobile'); }
  get email(): any { return this.ownerDetailsForm.get('email'); }
  get comment(): any { return this.ownerDetailsForm.get('comment'); }
  get city(): any { return this.ownerDetailsForm.get('city'); }
  get city2(): any { return this.ownerDetailsForm.get('city2'); }
  get houseNumber(): any { return this.ownerDetailsForm.get('houseNumber'); }
  get houseNumber2(): any { return this.ownerDetailsForm.get('houseNumber2'); }
  get apartmentNumber(): any { return this.ownerDetailsForm.get('apartmentNumber'); }
  get apartmentNumber2(): any { return this.ownerDetailsForm.get('apartmentNumber2'); }
  get postalCode(): any { return this.ownerDetailsForm.get('postalCode'); }
  get postalCode2(): any { return this.ownerDetailsForm.get('postalCode2'); }
  get street(): any { return this.ownerDetailsForm.get('street'); }
  get street2(): any { return this.ownerDetailsForm.get('street2'); }
  get dateOfBirth(): any { return this.ownerDetailsForm.get('dateOfBirth'); }
  get createdDate(): any { return this.ownerDetailsForm.get('createdDate'); }

  menuItems: MenuItem[];
  pageIsLoaded: boolean;
  showProgress: boolean;
  ownerPage$: Observable<OwnerPage>;
  ownerPage: OwnerPage;
  owner: Owner;
  animals: Animal[];
  inCreateNewMode: boolean;
  h1Header: string;
  h1Description: string;
  showComment: boolean;
  dateValue: Date;

  ngOnInit(): void {
    this.primengConfig.ripple = true;
    this.showProgress = true;
    this.route.data.subscribe(data => {
      switch (data.kind) {
        case 'new': {
          this.inCreateNewMode = true;
          this.h1Header = 'תיק חדש';
          this.h1Description = 'יצירת תיק חדש';
          break;
        }
        case 'edit': {
          this.inCreateNewMode = false;
          this.h1Header = 'פרטי בעלים';
          this.h1Description = 'דף המכיל פרטים של הבעלים';
          break;
        }
        default: {
          break;
        }
      }
    });

    if (this.inCreateNewMode){
      this.ownerDetailsForm.patchValue({
        city: '',
        dateOfBirth: null,
      });
      this.showProgress = false;
      this.pageIsLoaded = true;
    }else{
      this.ownerPage$ = this.route.paramMap.pipe(
        switchMap(params => {
          const ownerId = Number(params.get('id'));
          return this.ownerService.getOwner(ownerId);
        })
      );

      this.ownerPage$.subscribe(data => {
        this.ownerPage = data;
        this.changeDisabledMenuStatus(true);
        this.ownerDetailsForm.patchValue({
          ownerId: this.ownerPage.owner.ownerId,
          userId: this.ownerPage.owner.userId,
          idNumber: this.ownerPage.owner.idNumber,
          firstName: this.ownerPage.owner.firstName,
          lastName: this.ownerPage.owner.lastName,
          dateOfBirth: this.ownerPage.owner.dateOfBirth ? formatDate(this.ownerPage.owner.dateOfBirth, 'yyyy-MM-dd', 'he') : null,
          city: this.ownerPage.owner.city,
          city2: this.ownerPage.owner.city2,
          street: this.ownerPage.owner.street,
          street2: this.ownerPage.owner.street2,
          houseNumber: this.ownerPage.owner.houseNumber,
          houseNumber2: this.ownerPage.owner.houseNumber2,
          apartmentNumber: this.ownerPage.owner.apartmentNumber,
          apartmentNumber2: this.ownerPage.owner.apartmentNumber2,
          postalCode: this.ownerPage.owner.postalCode,
          postalCode2: this.ownerPage.owner.postalCode2,
          phone: this.ownerPage.owner.phone,
          mobile: this.ownerPage.owner.mobile,
          mailBox: this.ownerPage.owner.mailBox,
          email: this.ownerPage.owner.email,
          comment: this.ownerPage.owner.comment,
        });
        this.showProgress = false;
        this.pageIsLoaded = true;
        this.onChanges();
      }, error => {
        this.showProgress = false;
        this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
      });
    }
  }

  private onChanges(): void {
    this.ownerDetailsForm.valueChanges.pipe(
      distinctUntilChanged(),
    ).subscribe(formData => {
      if (this.ownerDetailsForm.valid){
        this.changeDisabledMenuStatus(false);
      }else {
        this.changeDisabledMenuStatus(true);
      }
    }, error => {
      this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
    });
  }

  onAdd(): void {
    if (this.ownerDetailsForm.valid){
      const owner = this.FormToOwnerMapper(this.ownerDetailsForm);
      this.ownerService.addOwner(owner).subscribe(ownerIdAdded => {
        if (Number(ownerIdAdded)){
          this.router.navigateByUrl('/owner/' + ownerIdAdded);
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
        command: () => this.UpdateServerWithOwnerChanges(),
        disabled: isDisabled
      },
      {
        label: 'חובות',
        icon: 'pi pi-fw pi-wallet',
        styleClass: this.checkIfDebtExist(),
        command: () => this.goToDebtsPage(),
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
    ];
  }

  private checkIfDebtExist(): string {
    const debtAmount = this.ownerPage.ownerTotalDebtAmount;
    if (debtAmount === 0) {
      return '';
    }

    if (debtAmount > 0) {
      return 'red-alert';
    }else {
      return 'green-alert';
    }
  }

  private goToNewAnimalpage(): void {
    this.router.navigate(['/animal', { ownerid: this.ownerId.value }]);
  }

  private goToDebtsPage(): void {
    this.router.navigate(['/debt', { ownerId: this.ownerId.value}]);
  }

  private onDelete(): void {
    this.confirmationService.confirm({
      header: 'מחיקת תיק',
      message: 'האם הינך בטוח כי ברצונך למחוק תיק זה?',
      icon: 'pi pi-exclamation-triangle',
      acceptLabel: 'מחק',
      rejectLabel: 'בטל',
      acceptIcon: 'pi pi-trash',
      acceptButtonStyleClass: 'confirmDialogAcceptButton',
      accept: () => {
        this.ownerService.deleteOwner(this.ownerPage.owner).subscribe(ans => {
          if (ans){
            this.messageService.add({severity: 'success', summary: 'התיק נמחק!', detail: '' });
            setTimeout(() =>
            {
              this.router.navigateByUrl('');
            }, 3000);
          } else {
            throwError(new Error('onDelete Error'));;
          }
        }, error => {
          this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
        });
      },
      reject: () => {}
    });
  }

  private UpdateServerWithOwnerChanges(): void {
    this.ownerService.updateOwner(this.FormToOwnerMapper(this.ownerDetailsForm)).subscribe(data => {
      if (data) {
        this.showSaveChangesMsg();
        this.changeDisabledMenuStatus(true);
      }
    }, error => {
      this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
    });
  }

  showSaveChangesMsg(): void {
    this.messageService.add({key: 'showSaveChanges', severity: 'success', summary: 'השינויים נשמרו בהצלחה', life: 800});
  }

  FormToOwnerMapper(form: FormGroup): Owner {
    const owner = new OwnerEntity();
    owner.setData(form);
    owner.user = this.authService.getUser();
    return owner;
  }
}

