import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {PrintService} from '../print.service';
import {VisitService} from '../../visit/visit.service';
import {VisitPage, VisitPageEntity} from '../../../models/visit-page';
import {formatDate} from '@angular/common';

@Component({
  selector: 'app-visit-print',
  templateUrl: './visit-print.component.html',
  styleUrls: ['./visit-print.component.css']
})
export class VisitPrintComponent implements OnInit {

  constructor(private route: ActivatedRoute,
              private visitService: VisitService,
              private printService: PrintService) {
    this.visitId = 0;
    this.visitPage = new VisitPageEntity();
  }

  visitId: number;
  visitPage: VisitPage;

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.visitId = params.visitId;
      this.visitService.getVisit(this.visitId).subscribe((data) => {
        this.visitPage = data;
      }, () => {
        const currentDate = formatDate(new Date(), 'yyyy-MM-ddThh:mm:ss', 'he');
        this.printService.onDataReady('visit-summary-' + currentDate);
      });
    });
  }
}
