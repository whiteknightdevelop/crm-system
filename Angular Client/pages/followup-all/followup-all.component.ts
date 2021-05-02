import { Component, OnInit } from '@angular/core';
import {HandleError, HttpErrorHandler} from '../../error-handlers/http-error-handler.service';
import {FormBuilder, Validators} from '@angular/forms';
import {MessageService} from 'primeng/api';
import {Router} from '@angular/router';
import {DATE_REGEX} from '../../models/RegexPatterns';
import {DateIntervalEntity} from '../../models/date-interval';
import {Followup} from '../../models/followup';
import {FollowUpService} from '../follow-up/follow-up.service';
import {formatDate} from '@angular/common';
import {FollowupAllItemEntity} from '../../models/followup-all-item';

@Component({
  selector: 'app-followup-all',
  templateUrl: './followup-all.component.html',
  styleUrls: ['./followup-all.component.css']
})
export class FollowupAllComponent implements OnInit {

  private readonly handleError: HandleError;
  constructor(private fb: FormBuilder,
              private followUpService: FollowUpService,
              private messageService: MessageService,
              private httpErrorHandler: HttpErrorHandler,
              private router: Router) {
    this.handleError = httpErrorHandler.createHandleError('RabiesListByDatePageComponent');
    this.followupAllList = [];
    this.showTable = false;
    this.showProgress = false;
    this.futurDate = new Date(new Date().getDate() + 30);
  }

  followupAllForm = this.fb.group({
    fromDate: [formatDate(new Date(), 'yyyy-MM-dd', 'he'), Validators.compose([Validators.required, Validators.pattern(DATE_REGEX)])],
  });

  get fromDate(): any { return this.followupAllForm.get('fromDate')?.value; }

  followupAllList: FollowupAllItemEntity[];
  showTable: boolean;
  showProgress: boolean;
  futurDate: Date;

  ngOnInit(): void {
    this.getFollowupAllByDate();
  }

  getFollowupAllByDate(): void {
    this.showProgress = true;
    const dateInterval = new DateIntervalEntity();
    dateInterval.from = new Date(this.fromDate);
    this.followUpService.getFollowupAll(dateInterval).subscribe((list) => {
      this.followupAllList = list;
      this.showTable = true;
      this.showProgress = false;
    }, error => {
      this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
    });
  }

  itemSelected(item: Followup): void {
    this.router.navigateByUrl('/animal/' + item.animalId);
  }

  goToOwner(ownerId: number): void {
    this.router.navigateByUrl('/owner/' + ownerId);
  }

  goToAnimal(animalId: number): void {
    this.router.navigateByUrl('/animal/' + animalId);
  }

  goToFollowUp(animalId: number): void {
    this.router.navigate(['/followup', { animalId }]);
  }

  getClassFromDateIfToday(date: Date): string {

    const match = new Date(date).setHours(0, 0, 0, 0); // convert date to number

    const today = new Date().setHours(0, 0, 0, 0); // get present day as number
    const day = ( n: any ) => today + (86400000 * n); // assuming all days are 86400000 milliseconds, then add or remove days from today

    if (match === today) {
      return 'today';
    }
    return '';
  }

  followupStatusCheckboxChange(followupItem: Followup, status: boolean): void {
    this.followUpService.updateFollowup(followupItem).subscribe(data => {
      if (data) {
        this.getFollowupAllByDate();
        this.showSaveChangesMsg('השינויים ברשומה נשמרו!');
      }
    }, error => {
      this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
    });

  }

  showSaveChangesMsg(summary: string): void {
    this.messageService.add({key: 'showSaveChanges', severity: 'success', summary, life: 1000});
  }
}
