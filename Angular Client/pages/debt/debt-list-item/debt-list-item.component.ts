import {Component, Input} from '@angular/core';
import {Debt, DebtEntity} from '../../../models/debt';

@Component({
  selector: 'app-debt-list-item',
  templateUrl: './debt-list-item.component.html',
  styleUrls: ['./debt-list-item.component.css']
})
export class DebtListItemComponent implements OnInit {

  constructor() {
    this.debt = new DebtEntity();
    this.enableEdit = false;
  }

  @Input() debt: Debt;
  enableEdit: boolean;

  onEditButtonClick($event: any, debt: Debt): void {
    this.enableEdit = !this.enableEdit;
  }
}
