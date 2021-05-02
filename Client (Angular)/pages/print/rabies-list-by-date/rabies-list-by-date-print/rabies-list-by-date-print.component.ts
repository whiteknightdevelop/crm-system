import { Component, OnInit } from '@angular/core';
import {RabiesListByDatePageService} from '../rabies-list-by-date-page/rabies-list-by-date-page.service';
import {formatDate} from '@angular/common';
import {RabiesListByDatePrintService} from './rabies-list-by-date-print.service';
import {ActivatedRoute} from '@angular/router';
import {PrintService} from '../../print.service';
import {RabiesReportItem} from '../../../../models/rabies-report-item';
import {DateIntervalEntity} from '../../../../models/date-interval';

@Component({
  selector: 'app-rabies-list-by-date-print',
  templateUrl: './rabies-list-by-date-print.component.html',
  styleUrls: ['./rabies-list-by-date-print.component.css']
})
export class RabiesListByDatePrintComponent implements OnInit {

  constructor(private rabiesListPageService: RabiesListByDatePageService, private printService: PrintService,
              public rabiesListPrintService: RabiesListByDatePrintService,
              private route: ActivatedRoute) {
    this.rabiesList = [];
    this.from = new Date();
    this.to = new Date();
  }

  rabiesList: RabiesReportItem[];
  reportH1Title = 'גליון חיסוני כלבת';
  from: Date;
  to: Date;

  ngOnInit(): void {

    this.route.params.subscribe(params => {
      this.from = new Date(formatDate(new Date(params.from), 'yyyy-MM-dd', 'he'));
      this.to = new Date(formatDate(new Date(params.to), 'yyyy-MM-dd', 'he'));
    });

    const dateInterval = new DateIntervalEntity();
    dateInterval.from = this.from;
    dateInterval.to = this.to;

    this.rabiesListPageService.getRabiesListByDate(dateInterval).subscribe((list) => {
      this.rabiesList = list;
    }, error => {

    }, () => {
      const currentDate = formatDate(new Date(), 'yyyy-MM-ddThh:mm:ss', 'he');
      this.printService.onDataReady('rabies-report-' + currentDate);
    });
  }

}
