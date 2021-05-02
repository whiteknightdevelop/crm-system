import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {DynamicDialogConfig, DynamicDialogRef} from 'primeng/dynamicdialog';
import { Treatment } from '../../../../models/treatment';
import { TreatmentsListService } from '../treatment-list.service';

@Component({
  selector: 'app-treatment-list',
  templateUrl: './treatment-list.component.html',
  styleUrls: ['./treatment-list.component.css']
})
export class TreatmentListComponent implements OnInit {

  constructor(private service: TreatmentsListService,
              private route: ActivatedRoute,
              public config: DynamicDialogConfig,
              public ref: DynamicDialogRef) {
    this.treatments = [];
    this.filteredTreatmentsList = [];
  }

  treatments: Treatment[];
  filteredTreatmentsList: Treatment[];

  ngOnInit(): void {
    this.treatments = this.config.data as Treatment[];
    this.filteredTreatmentsList = this.treatments;

  }

  onRowSelect(event: { data: any; }): void {
    this.ref.close(event.data);
  }

  searchInputOnChange($event: Event): void {
    const searchString = ($event.target as HTMLInputElement).value.toLowerCase();
    this.filteredTreatmentsList = this.treatments.filter(d => d.name.toLowerCase().includes(searchString));
  }
}
