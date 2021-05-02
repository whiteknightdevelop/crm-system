import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {PrintService} from '../../print.service';
import {VisitedOwnersItem} from '../../../../models/visited-owners-item';
import {NotVisitedLastDaysPageService} from '../not-visited-last-days-page/not-visited-last-days-page.service';
import {formatDate} from '@angular/common';

@Component({
  selector: 'app-not-visited-last-days-print',
  templateUrl: './not-visited-last-days-print.component.html',
  styleUrls: ['./not-visited-last-days-print.component.css']
})
export class NotVisitedLastDaysPrintComponent implements OnInit {

  constructor(private route: ActivatedRoute,
              private printService: PrintService,
              private notVisitedService: NotVisitedLastDaysPageService) {
    this.visitedOwnersList = [];
    this.numOfDays = 0;
  }

  visitedOwnersList: VisitedOwnersItem[];
  numOfDays: number;

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.numOfDays = params.numOfDays;
    });

    this.notVisitedService.getNotVisitedListByNumOfDays(this.numOfDays).subscribe((list) => {
      this.visitedOwnersList = list;
    }, () => {
      const currentDate = formatDate(new Date(), 'yyyy-MM-ddThh:mm:ss', 'he');
      this.printService.onDataReady('not-visited-' + currentDate);
    });
  }
}
