import { Component, OnInit } from '@angular/core';
import {ConfirmationService, MenuItem, MessageService, PrimeNGConfig} from 'primeng/api';
import {FollowupPage, FollowupPageEntity} from '../../models/followup-page';
import {FormArray, FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ALPHANUMERICWITHHEBREW_REGEX, DATE_REGEX} from '../../models/RegexPatterns';
import {ActivatedRoute, Router} from '@angular/router';
import {FollowUpService} from '../follow-up/follow-up.service';
import {Followup, FollowupEntity} from '../../models/followup';
import {formatDate} from '@angular/common';
import {HttpErrorHandler} from '../../error-handlers/http-error-handler.service';

@Component({
  selector: 'app-follow-up',
  templateUrl: './follow-up.component.html',
  styleUrls: ['./follow-up.component.css'],
  providers: [FollowUpService],
})
export class FollowUpComponent implements OnInit {

  constructor(private fb: FormBuilder,
              private primengConfig: PrimeNGConfig,
              private followUpService: FollowUpService,
              private route: ActivatedRoute,
              private httpErrorHandler: HttpErrorHandler,
              private confirmationService: ConfirmationService,
              private messageService: MessageService,
              private router: Router) {
    this.menuItems = [];
    this.pageIsLoaded = false;
    this.showProgress = false;
    this.h1Header = '';
    this.h1Description = '';
    this.followupPage = new FollowupPageEntity();
    this.addNewOpened = false;
    this.currentAnimalId = 0;
    this.tableForm = this.fb.group({
      followupsArray: this.fb.array([])
    });
    this.rowIdCounter = 0;
    this.tableDataLoaded = false;
  }

  menuItems: MenuItem[];
  pageIsLoaded: boolean;
  showProgress: boolean;
  h1Header: string;
  h1Description: string;
  followupPage: FollowupPage;
  addNewOpened: boolean;
  currentAnimalId: number;
  tableForm: FormGroup;
  rowIdCounter: number;
  tableDataLoaded: boolean;

  addNewRowForm = this.fb.group({
    followUpId: 0,
    animalId: 0,
    date: [formatDate(new Date(), 'yyyy-MM-dd', 'he'), Validators.compose([Validators.required, Validators.pattern(DATE_REGEX)])],
    cause: ['', Validators.compose([Validators.required, Validators.pattern(ALPHANUMERICWITHHEBREW_REGEX), Validators.maxLength(100)])],
    status: false,
  });

  get followupsArray(): FormArray {
    return this.tableForm.get('followupsArray') as FormArray;
  }
  get followUpId(): any { return this.addNewRowForm.get('followUpId'); }
  get animalId(): any { return this.addNewRowForm.get('animalId'); }
  get date(): any { return this.addNewRowForm.get('date'); }
  get cause(): any { return this.addNewRowForm.get('cause'); }
  get status(): any { return this.addNewRowForm.get('status'); }

  ngOnInit(): void {
    this.primengConfig.ripple = true;
    this.showProgress = true;
    this.h1Header = 'מעקב';
    this.h1Description = 'דף המכיל פרטי מעקב של הבע"ח';

    this.currentAnimalId = Number(this.route.snapshot.paramMap.get('animalId'));
    this.followUpService.getFollowupPage(this.currentAnimalId).subscribe(data => {
      if (data) {
        this.followupPage = data;
        this.initializeAddNewRowFormData();
        this.setTableFormListData(this.followupPage.followUpsList);
      }
    }, error => {
      this.showProgress = false;
      this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
    }, () => {
      this.changeDisabledMenuStatus(true);
      this.tableDataLoaded = true;
      this.showProgress = false;
      this.pageIsLoaded = true;
    });
  }

  trackByFn(index: number, row: any): number {
    return index;
  }

  tableFormGetArrayControl(index: number): any {
    const arrayControl = this.tableForm.get('followupsArray') as FormArray;
    return arrayControl.at(index);
  }

  initializeAddNewRowFormData(): void {
    this.addNewRowForm.patchValue({
      followUpId: 0,
      animalId: this.currentAnimalId,
      cause: '',
      status: false,
      date: formatDate(new Date(), 'yyyy-MM-dd', 'he')
    });
  }

  setTableFormListData(list: Followup[]): void {
    const arrayControl = this.tableForm.get('followupsArray') as FormArray;
    arrayControl.clear();
    list.forEach((item) => {
      this.addRow(item);
    });
  }

  addRow(followup: Followup): void {
    this.followupsArray.push(this.addFormGroupToTable(followup));
    this.rowIdCounter = this.rowIdCounter + 1;
  }

  addFormGroupToTable(followup: Followup): FormGroup {
    return this.fb.group({
      id: this.rowIdCounter,
      followUpId: followup.followUpId,
      animalId: followup.animalId,
      cause: [followup.cause,
        Validators.compose([Validators.required, Validators.pattern(ALPHANUMERICWITHHEBREW_REGEX), Validators.maxLength(100)])],
      date: [formatDate(followup.date, 'yyyy-MM-dd', 'he') , Validators.compose([Validators.required, Validators.pattern(DATE_REGEX)])],
      status: followup.status,
    });
  }

  private changeDisabledMenuStatus(isDisabled: boolean): void {
    this.menuItems = [
      {
        label: 'הדפסה',
        icon: 'pi pi-fw pi-print',
        disabled: isDisabled,
      },
      {
        label: 'חזרה לבע"ח',
        icon: 'pi pi-fw pi-arrow-left',
        command: () => this.goBackToAnimalPage(),
      },
    ];
  }

  private goBackToAnimalPage(): void {
    this.router.navigate(['/animal', this.followupPage.animal.animalId]);
  }

  showAddNewRow(): void {
    this.addNewOpened = true;
  }

  onAddFollowup(): void  {
    if (this.addNewRowForm.valid){
      const followup = this.FormToFollowupMapper(this.addNewRowForm);
      this.followUpService.addFollowup(followup).subscribe(followUpIdAdded => {
        if (Number(followUpIdAdded)){
          this.initializeAddNewRowFormData();
          this.getFollowupsTableData();
          this.showSaveChangesMsg('הרשומה נוספה!');
          this.addNewOpened = false;
        }
      }, error => {
        this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
      });
    }
  }

  FormToFollowupMapper(form: FormGroup): Followup {
    const followup = new FollowupEntity();
    followup.setData(form);
    return followup;
  }

  getFollowupsTableData(): void {
    this.tableDataLoaded = false;
    this.followUpService.getFollowupsList(this.currentAnimalId).subscribe(list => {
      if (list) {
        this.followupPage.followUpsList = list;
        this.setTableFormListData(list);
        this.tableDataLoaded = true;
      }
    }, error => {
      this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
    });
  }

  showSaveChangesMsg(summary: string): void {
    this.messageService.add({key: 'showSaveChanges', severity: 'success', summary, life: 1000});
  }

  onRowEditSave(followupItem: FormGroup): void {
    this.followUpService.updateFollowup(this.FormToFollowupMapper(followupItem)).subscribe(data => {
      if (data) {
        this.getFollowupsTableData();
        this.showSaveChangesMsg('השינויים ברשומה נשמרו!');
      }
    }, error => {
      this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
    });
  }

  onRowEditCancel(rowItem: FormGroup, ri: any): void {
    const followup = rowItem.value as Followup;
    this.confirmationService.confirm({
      header: 'מחיקת רשומה',
      message: 'האם הינך בטוח כי ברצונך למחוק רשומה זו?',
      icon: 'pi pi-exclamation-triangle',
      acceptLabel: 'מחק',
      rejectLabel: 'בטל',
      acceptIcon: 'pi pi-trash',
      acceptButtonStyleClass: 'confirmDialogAcceptButton',
      accept: () => {
        this.followUpService.deleteFollowup(followup).subscribe(ans => {
          if (ans){
            this.getFollowupsTableData();
            this.showSaveChangesMsg('הרשומה נמחקה!');
          }
        }, error => {
          this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
        });
      },
      reject: () => {}
    });
  }

  getBoolFromString(value: string): boolean {
    switch (value) {
      case 'true': {
        return true;
        break;
      }
      case 'false': {
        return false;
        break;
      }
      default: {
        return false;
        break;
      }
    }
  }
}
