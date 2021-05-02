import { Component, OnInit } from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {RabiesListByDatePageService} from './rabies-list-by-date-page.service';
import {RabiesListByDatePrintService} from '../rabies-list-by-date-print/rabies-list-by-date-print.service';
import {PrintService} from '../../print.service';
import {DATE_REGEX} from '../../../../models/RegexPatterns';
import {RabiesReportItem} from '../../../../models/rabies-report-item';
import {DateIntervalEntity} from '../../../../models/date-interval';
import {MessageService} from 'primeng/api';
import {HandleError, HttpErrorHandler} from '../../../../error-handlers/http-error-handler.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-rabies-list-by-date-page',
  templateUrl: './rabies-list-by-date-page.component.html',
  styleUrls: ['./rabies-list-by-date-page.component.css']
})
export class RabiesListByDatePageComponent implements OnInit {

  private readonly handleError: HandleError;
  constructor(private printService: PrintService, private fb: FormBuilder, private rabiesListService: RabiesListByDatePageService,
              private rabiesListPrintService: RabiesListByDatePrintService,
              private messageService: MessageService,
              private httpErrorHandler: HttpErrorHandler,
              private router: Router) {
    this.handleError = httpErrorHandler.createHandleError('RabiesListByDatePageComponent');
    this.rabiesList = [];
    this.showTable = false;
    this.showProgressBar = false;
  }

  rabiesListForm = this.fb.group({
    fromDate: ['', Validators.compose([Validators.required, Validators.pattern(DATE_REGEX)])],
    toDate: ['', Validators.compose([Validators.required, Validators.pattern(DATE_REGEX)])],
  });

  get fromDate(): any { return this.rabiesListForm.get('fromDate')?.value; }
  get toDate(): any { return this.rabiesListForm.get('toDate')?.value; }
  rabiesList: RabiesReportItem[];
  showTable: boolean;
  showProgressBar: boolean;

  ngOnInit(): void {
  }

  getRabiesListByDate(): void {
    this.showProgressBar = true;
    const dateInterval = new DateIntervalEntity();
    dateInterval.from = new Date(this.fromDate);
    dateInterval.to = new Date(this.toDate);

    this.rabiesListService.getRabiesListByDate(dateInterval).subscribe((list) => {
      this.rabiesList = list;
      this.showTable = true;
      this.showProgressBar = false;
    }, error => {
      this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
    });
  }

  printRabiesListByDate(): void {
    const from = new Date(this.fromDate);
    const to = new Date(this.toDate);
    this.printService
      .printDocument('rabies-list-print' , {from, to});
  }

  itemSelected(item: RabiesReportItem): void {
    this.router.navigateByUrl('/visit/' + item.visit.visitId);
  }
}
