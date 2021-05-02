import { Component, OnInit } from '@angular/core';
import {PrintService} from '../../print.service';
import {VisitedOwnersItem} from '../../../../models/visited-owners-item';
import {formatDate} from '@angular/common';
import {ActivatedRoute} from '@angular/router';
import {VisitedLastDaysPageService} from '../visited-last-days-page/visited-last-days-page.service';

@Component({
  selector: 'app-visited-last-days-print',
  templateUrl: './visited-last-days-print.component.html',
  styleUrls: ['./visited-last-days-print.component.css']
})
export class VisitedLastDaysPrintComponent implements OnInit {

  constructor(private route: ActivatedRoute,
              private printService: PrintService,
              private visitedService: VisitedLastDaysPageService) {
    this.visitedOwnersList = [];
    this.numOfDays = 0;
  }

  visitedOwnersList: VisitedOwnersItem[];
  numOfDays: number;

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.numOfDays = params.numOfDays;
    });
    this.visitedService.getVisitedListByNumOfDays(this.numOfDays).subscribe((list) => {
      this.visitedOwnersList = list;

    }, error => {}, () => {
      const currentDate = formatDate(new Date(), 'yyyy-MM-ddThh:mm:ss', 'he');
      this.printService.onDataReady('visited-' + currentDate);
    });
  }
}
