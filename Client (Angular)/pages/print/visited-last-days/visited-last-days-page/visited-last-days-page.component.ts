import { Component, OnInit } from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {NUMERIC_REGEX} from '../../../../models/RegexPatterns';
import {PrintService} from '../../print.service';
import {HandleError, HttpErrorHandler} from '../../../../error-handlers/http-error-handler.service';
import {VisitedOwnersItem} from '../../../../models/visited-owners-item';
import {VisitedLastDaysPageService} from './visited-last-days-page.service';
import {MessageService} from 'primeng/api';
import {Router} from '@angular/router';

@Component({
  selector: 'app-visited-last-days-page',
  templateUrl: './visited-last-days-page.component.html',
  styleUrls: ['./visited-last-days-page.component.css']
})
export class VisitedLastDaysPageComponent implements OnInit {

  private readonly handleError: HandleError;
  constructor(private printService: PrintService,
              private fb: FormBuilder,
              private httpErrorHandler: HttpErrorHandler,
              private messageService: MessageService,
              private router: Router,
              private visitedService: VisitedLastDaysPageService) {
    this.handleError = httpErrorHandler.createHandleError('VisitedLastDaysPageComponent');
    this.visitedOwnersList = [];
    this.showTable = false;
    this.showProgress = false;
  }

  form = this.fb.group({
    numOfDays: [0, Validators.compose([Validators.required, Validators.pattern(NUMERIC_REGEX)])],
  });

  get numOfDays(): any { return this.form.get('numOfDays')?.value; }
  visitedOwnersList: VisitedOwnersItem[];
  showTable: boolean;
  showProgress: boolean;

  ngOnInit(): void {
    this.getVisitedListByNumOfDays();
  }

  getVisitedListByNumOfDays(): void {
    this.showProgress = true;

    this.visitedService.getVisitedListByNumOfDays(this.numOfDays).subscribe((list) => {
      this.visitedOwnersList = list;
      this.showTable = true;
      this.showProgress = false;
    }, error => {
      this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
    });
  }

  printRabiesListByDate(): void {
    const numOfDays = this.numOfDays;
    this.printService
      .printDocument('visited-print' , {numOfDays});
  }

  itemSelected(item: VisitedOwnersItem): void {
    this.router.navigateByUrl('/owner/' + item.ownerId);
  }
}
