import {AfterViewInit, Component, ElementRef, OnInit} from '@angular/core';
import {Router} from '@angular/router';

@Component({
  selector: 'app-print-layout',
  templateUrl: './print-layout.component.html',
  styleUrls: ['./print-layout.component.css'],
})
export class PrintLayoutComponent implements OnInit, AfterViewInit  {

  constructor(private router: Router, private elementRef: ElementRef) { }
  
  currentDate = new Date();
  reportH1Title = 'אישור';
}
