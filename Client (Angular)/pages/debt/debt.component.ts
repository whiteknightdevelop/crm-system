import { Component, OnInit } from '@angular/core';
import { formatDate } from '@angular/common';
import {ConfirmationService, MenuItem, MessageService, PrimeNGConfig} from 'primeng/api';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ALPHANUMERICWITHHEBREW_REGEX, DATE_REGEX, NUMERIC_REGEX} from '../../models/RegexPatterns';
import {DebtPage, DebtPageEntity} from '../../models/debt-page';
import {Debt, DebtEntity} from '../../models/debt';
import {ActivatedRoute, Router} from '@angular/router';
import {DebtService} from './debt.service';
import { FormArray } from '@angular/forms';
import {HttpErrorHandler} from '../../error-handlers/http-error-handler.service';

@Component({
  selector: 'app-debts',
  templateUrl: './debt.component.html',
  styleUrls: ['./debt.component.css'],
  providers: [DebtService],
})
export class DebtComponent implements OnInit {

  constructor(private fb: FormBuilder,
              private primengConfig: PrimeNGConfig,
              private debtService: DebtService,
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
    this.debtPage = new DebtPageEntity();
    this.addNewOpened = false;
    this.currentOwnerId = 0;
    this.totalDebtAmount = 0;
    this.tableDataLoaded = false;
    this.tableRowInEditMode = false;
    this.enableEdit = false;
    this.enableTableRowTitle = true;
    this.enableEditIndex = 0;
    this.tableForm = this.fb.group({
      debtsArray: this.fb.array([])
    });
    this.rowIdCounter = 0;
  }

  menuItems: MenuItem[];
  pageIsLoaded: boolean;
  showProgress: boolean;
  h1Header: string;
  h1Description: string;
  debtPage: DebtPage;
  addNewOpened: boolean;
  currentOwnerId: number;
  totalDebtAmount: number;
  tableDataLoaded: boolean;
  tableRowInEditMode: boolean;
  enableEdit: boolean;
  enableTableRowTitle: boolean;
  enableEditIndex: number;
  tableForm: FormGroup;
  rowIdCounter: number;
  addNewRowForm = this.fb.group({
    ownerId: 0,
    debtDate: [formatDate(0, 'yyyy-MM-dd', 'he'), Validators.compose([Validators.required, Validators.pattern(DATE_REGEX)])],
    animalName: ['', Validators.compose([Validators.required, Validators.pattern(ALPHANUMERICWITHHEBREW_REGEX)])],
    causeOfDebt: ['', Validators.compose([Validators.required, Validators.pattern(ALPHANUMERICWITHHEBREW_REGEX)])],
    debtAmount: [null, Validators.compose([Validators.required, Validators.pattern(NUMERIC_REGEX)])],
    debtPaid: [null, Validators.compose([Validators.required, Validators.pattern(NUMERIC_REGEX)])],
  });

  get debtsArray(): FormArray {
    return this.tableForm.get('debtsArray') as FormArray;
  }
  get ownerId(): any { return this.addNewRowForm.get('ownerId'); }
  get debtDate(): any { return this.addNewRowForm.get('debtDate'); }
  get animalName(): any { return this.addNewRowForm.get('animalName'); }
  get causeOfDebt(): any { return this.addNewRowForm.get('causeOfDebt'); }
  get debtAmount(): any { return this.addNewRowForm.get('debtAmount'); }
  get debtPaid(): any { return this.addNewRowForm.get('debtPaid'); }

  ngOnInit(): void {
    this.primengConfig.ripple = true;
    this.showProgress = true;
    this.h1Header = 'חובות';
    this.h1Description = 'דף המכיל פרטי חובות של הבעלים';
    this.currentOwnerId = Number(this.route.snapshot.paramMap.get('ownerId'));
    this.debtService.getDebtPage(this.currentOwnerId).subscribe(data => {
      if (data) {
        this.debtPage = data;
        this.addNewRowForm.patchValue({
          ownerId: this.debtPage.owner.ownerId
        });
        this.initializeAddNewRowFormData();
        this.setTableFormListData(this.debtPage.debtsList);
      }
    }, error => {
      this.showProgress = false;
      this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
    }, () => {
      this.calculateTotalDebtAmount();
      this.changeDisabledMenuStatus(true);
      this.tableDataLoaded = true;
      this.showProgress = false;
      this.pageIsLoaded = true;
    });
  }

  tableFormGetArrayControl(index: number): any {
    const arrayControl = this.tableForm.get('debtsArray') as FormArray;
    return arrayControl.at(index);
  }

  trackByFn(index: number, row: any): number {
    return index;
  }

  addFormGroupToTable(debt: Debt): FormGroup {
    return this.fb.group({
      id: this.rowIdCounter,
      ownerId: debt.ownerId,
      debtId: debt.debtId,
      debtDate: [debt.debtDate ? formatDate(debt.debtDate, 'yyyy-MM-dd', 'he') : null ,
        Validators.compose([Validators.required, Validators.pattern(DATE_REGEX)])],
      animalName: [debt.animalName, Validators.compose([Validators.required, Validators.pattern(ALPHANUMERICWITHHEBREW_REGEX)])],
      causeOfDebt: [debt.cause, Validators.compose([Validators.required, Validators.pattern(ALPHANUMERICWITHHEBREW_REGEX)])],
      debtAmount: [debt.debtAmount, Validators.compose([Validators.required, Validators.pattern(NUMERIC_REGEX)])],
      debtPaid: [debt.paidAmount, Validators.compose([Validators.required, Validators.pattern(NUMERIC_REGEX)])],
    });
  }

  addRow(debt: Debt): void {
    this.debtsArray.push(this.addFormGroupToTable(debt));
    this.rowIdCounter = this.rowIdCounter + 1;
  }

  setTableFormListData(list: Debt[]): void {
    const arrayControl = this.tableForm.get('debtsArray') as FormArray;
    arrayControl.clear();
    list.forEach((item) => {
      this.addRow(item);
    });
  }

  onAddDebt(): void  {
    if (this.addNewRowForm.valid){
      const debt = FormToDebtMapper(this.addNewRowForm);
      this.debtService.addDebt(debt).subscribe(debtIdAdded => {
        if (Number(debtIdAdded)){
          this.initializeAddNewRowFormData();
          this.getDebtsTableData();
          this.showSaveChangesMsg('הרשומה נוספה!');
          this.addNewOpened = false;
        }
      }, error => {
        this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
      });
    }
  }

  initializeAddNewRowFormData(): void {
    this.addNewRowForm.patchValue({
      debtDate: formatDate(new Date(), 'yyyy-MM-dd', 'he'),
      animalName: '',
      causeOfDebt: '',
      debtAmount: 0,
      debtPaid: 0,
    });
  }

  getDebtsTableData(): void {
    this.tableDataLoaded = false;
    this.debtService.getDebtsList(this.currentOwnerId).subscribe(list => {
      if (list) {
        this.debtPage.debtsList = list;

        this.setTableFormListData(list);
        this.calculateTotalDebtAmount();
        this.tableDataLoaded = true;
      }
    }, error => {
      this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
    });
  }

  showSaveChangesMsg(summary: string): void {
    this.messageService.add({key: 'showSaveChanges', severity: 'success', summary, life: 1000});
  }

  showAddNewRow(): void {
    this.addNewOpened = true;
  }

  private changeDisabledMenuStatus(isDisabled: boolean): void {
    this.menuItems = [
      {
        label: 'הדפסה',
        icon: 'pi pi-fw pi-print',
        disabled: true,
      },
      {
        label: 'חזרה לבעלים',
        icon: 'pi pi-fw pi-arrow-left',
        command: () => this.goBackToOwnerPage(),
      },
    ];
  }

  private goBackToOwnerPage(): void {
    this.router.navigate(['/owner', this.debtPage.owner.ownerId]);
  }

  onDelete(debt: Debt): void {
    this.confirmationService.confirm({
      header: 'מחיקת חוב',
      message: 'האם הינך בטוח כי ברצונך למחוק חוב זה?',
      icon: 'pi pi-exclamation-triangle',
      acceptLabel: 'מחק',
      rejectLabel: 'בטל',
      acceptIcon: 'pi pi-trash',
      acceptButtonStyleClass: 'confirmDialogAcceptButton',
      accept: () => {
        this.debtService.deleteDebt(debt).subscribe(ans => {
          if (ans){
            this.getDebtsTableData();
            this.showSaveChangesMsg('החוב נמחק!');
          }
        }, error => {
          this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
        });
      },
      reject: () => {}
    });
  }

  private calculateTotalDebtAmount(): void {
    let totalToPay = 0;
    let totalPaid = 0;

    this.debtPage.debtsList.forEach( (value) => {
      totalToPay += value.debtAmount;
      totalPaid += value.paidAmount;
    });

    this.totalDebtAmount = totalPaid - totalToPay;
  }

  onRowEditSave(debtItem: FormGroup): void {
    this.debtService.updateDebt(FormToDebtMapper(debtItem)).subscribe(data => {
      if (data) {
        this.getDebtsTableData();
        this.showSaveChangesMsg('השינויים ברשומה נשמרו!');
      }
    }, error => {
      this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
    });
  }

  onRowEditCancel(rowItem: FormGroup, ri: any): void {
    const debt = rowItem.value as Debt;
    this.confirmationService.confirm({
      header: 'מחיקת רשומה',
      message: 'האם הינך בטוח כי ברצונך למחוק רשומה זו?',
      icon: 'pi pi-exclamation-triangle',
      acceptLabel: 'מחק',
      rejectLabel: 'בטל',
      acceptIcon: 'pi pi-trash',
      acceptButtonStyleClass: 'confirmDialogAcceptButton',
      accept: () => {
        this.debtService.deleteDebt(debt).subscribe(ans => {
          if (ans){
            this.getDebtsTableData();
            this.showSaveChangesMsg('הרשומה נמחקה!');
          }
        }, error => {
          this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
        });
      },
      reject: () => {}
    });
  }
}

function FormToDebtMapper(form: FormGroup): Debt {
  const debt = new DebtEntity();
  debt.setData(form);
  return debt;
}
