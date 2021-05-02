import { Component, OnInit } from '@angular/core';
import {PrintService} from '../../print.service';
import {FormBuilder} from '@angular/forms';
import {Debtor, DebtorEntity} from '../../../../models/debtor';
import {DebtsSheetPageService} from './debts-sheet-page.service';
import {MessageService} from 'primeng/api';
import {HandleError, HttpErrorHandler} from '../../../../error-handlers/http-error-handler.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-debts-sheet-page',
  templateUrl: './debts-sheet-page.component.html',
  styleUrls: ['./debts-sheet-page.component.css']
})
export class DebtsSheetPageComponent implements OnInit {

  private readonly handleError: HandleError;
  constructor(private printService: PrintService, private fb: FormBuilder,
              private debtorsService: DebtsSheetPageService,
              private messageService: MessageService,
              private httpErrorHandler: HttpErrorHandler,
              private router: Router) {
    this.handleError = httpErrorHandler.createHandleError('DebtsSheetPageComponent');
    this.debtorsList = [];
    this.pageIsLoaded = false;
    this.showProgress = true;
    this.selectedItem = new DebtorEntity();
  }

  debtorsList: Debtor[];
  pageIsLoaded: boolean;
  showProgress: boolean;
  selectedItem: Debtor;

  ngOnInit(): void {
    this.getDebtorsList();
  }

  getDebtorsList(): void {
    this.showProgress = true;
    this.debtorsService.getDebtorsList().subscribe((list) => {
      this.debtorsList = list;
      this.pageIsLoaded = true;
      this.showProgress = false;
    }, error => {
      this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
    });
  }

  printDebtorsList(): void {
    this.printService
      .printDocument('debtors-list-print' , {});
  }

  itemSelected(item: Debtor): void {
    this.router.navigateByUrl('/owner/' + item.ownerId);
  }
}
