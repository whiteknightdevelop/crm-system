import { Component } from '@angular/core';
import {PrintService} from '../../pages/print/print.service';

@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.css']
})
export class MainLayoutComponent {

  constructor(public printService: PrintService) { }

}
