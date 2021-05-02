import { Component } from '@angular/core';
import {PrintService} from '../../print.service';
import {FormBuilder, Validators} from '@angular/forms';
import {HandleError, HttpErrorHandler} from '../../../../error-handlers/http-error-handler.service';
import {MessageService} from 'primeng/api';
import {Router} from '@angular/router';
import {NUMERIC_REGEX} from '../../../../models/RegexPatterns';
import {VisitedOwnersItem} from '../../../../models/visited-owners-item';
import {NotVisitedLastDaysPageService} from './not-visited-last-days-page.service';

@Component({
  selector: 'app-not-visited-last-days-page',
  templateUrl: './not-visited-last-days-page.component.html',
  styleUrls: ['./not-visited-last-days-page.component.css']
})
export class NotVisitedLastDaysPageComponent implements OnInit {

  private readonly handleError: HandleError;
  constructor(private printService: PrintService,
              private fb: FormBuilder,
              private httpErrorHandler: HttpErrorHandler,
              private messageService: MessageService,
              private router: Router,
              private notVisitedService: NotVisitedLastDaysPageService) {
    this.handleError = httpErrorHandler.createHandleError('VisitedLastDaysPageComponent');
    this.visitedOwnersList = [];
    this.showTable = false;
    this.showProgress = false;
  }

  form = this.fb.group({
    numOfDays: [30, Validators.compose([Validators.required, Validators.pattern(NUMERIC_REGEX)])],
  });

  get numOfDays(): any { return this.form.get('numOfDays')?.value; }
  visitedOwnersList: VisitedOwnersItem[];
  showTable: boolean;
  showProgress: boolean;

  getVisitedListByNumOfDays(): void {
    this.showProgress = true;
    this.notVisitedService.getNotVisitedListByNumOfDays(this.numOfDays).subscribe((list) => {
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
      .printDocument('not-visited-print' , {numOfDays});
  }

  itemSelected(item: VisitedOwnersItem): void {
    this.router.navigateByUrl('/owner/' + item.ownerId);
  }
}
