import { Component, OnInit } from '@angular/core';
import {Debtor} from '../../../../models/debtor';
import {DebtsSheetPageService} from '../debts-sheet-page/debts-sheet-page.service';
import {PrintService} from '../../print.service';
import {formatDate} from '@angular/common';

@Component({
  selector: 'app-debts-sheet-print',
  templateUrl: './debts-sheet-print.component.html',
  styleUrls: ['./debts-sheet-print.component.css']
})
export class DebtsSheetPrintComponent implements OnInit {

  constructor(private debtorsService: DebtsSheetPageService,
              private printService: PrintService) {
    this.currentDate = new Date();
    this.debtorsList = [];
  }

  currentDate: Date;
  reportH1Title = 'דו"ח חייבים נכון ליום:';
  debtorsList: Debtor[];

  ngOnInit(): void {
    this.debtorsService.getDebtorsList().subscribe((list) => {
      this.debtorsList = list;

    }, error => {}, () => {
      const currentDate = formatDate(new Date(), 'yyyy-MM-ddThh:mm:ss', 'he');
      this.printService.onDataReady('debtors-list-' + currentDate);
    });
  }
}
