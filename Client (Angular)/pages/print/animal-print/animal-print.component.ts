import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {PrintService} from '../print.service';
import {formatDate} from '@angular/common';
import {AnimalPrintService} from './animal-print.service';
import {AnimalPagePrint, AnimalPagePrintEntity} from '../../../models/animal-page-print';

@Component({
  selector: 'app-animal-print',
  templateUrl: './animal-print.component.html',
  styleUrls: ['./animal-print.component.css']
})
export class AnimalPrintComponent implements OnInit {

  constructor(private route: ActivatedRoute,
              private animalPrintService: AnimalPrintService,
              private printService: PrintService) {
    this.animalId = 0;
    this.animalPage = new AnimalPagePrintEntity();
    this.ageYears = 0;
    this.ageMonths = 0;
  }

  animalId: number;
  animalPage: AnimalPagePrint;
  ageYears: number;
  ageMonths: number;

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.animalId = params.animalid;
    });
    this.animalPrintService.getAnimalPrint(this.animalId).subscribe((data) => {
      this.animalPage = data;
    }, error => {}, () => {
      const currentDate = formatDate(new Date(), 'yyyy-MM-ddThh:mm:ss', 'he');
      this.printService.onDataReady('animal-summary-' + currentDate);
    });
  }
}
