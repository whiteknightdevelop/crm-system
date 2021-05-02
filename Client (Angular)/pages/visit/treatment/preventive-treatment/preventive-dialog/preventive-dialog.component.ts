import { Component, OnInit } from '@angular/core';
import {DynamicDialogConfig, DynamicDialogRef} from 'primeng/dynamicdialog';
import {PreventiveTreatment} from '../../../../../models/preventive-treatment';
import {PrimeNGConfig} from 'primeng/api';

@Component({
  selector: 'app-preventive-dialog',
  templateUrl: './preventive-dialog.component.html',
  styleUrls: ['./preventive-dialog.component.css']
})
export class PreventiveDialogComponent implements OnInit {

  constructor(public ref: DynamicDialogRef, public config: DynamicDialogConfig, private primengConfig: PrimeNGConfig) {
    this.preventiveTreatmentsList = [];
    this.filteredPreventiveTreatmentsList = [];
    this.visitPreventiveTreatmentsList = [];
    this.selectedTreatments = [];
  }

  preventiveTreatmentsList: PreventiveTreatment[];
  filteredPreventiveTreatmentsList: PreventiveTreatment[];
  visitPreventiveTreatmentsList: PreventiveTreatment[];
  selectedTreatments: PreventiveTreatment[];

  ngOnInit(): void {
    this.primengConfig.ripple = true;
    this.preventiveTreatmentsList = this.config.data.allPreventiveTreatments;
    this.filteredPreventiveTreatmentsList = this.preventiveTreatmentsList;
    this.visitPreventiveTreatmentsList = this.config.data.visitPreventiveTreatmentsList;
    const TreatmentsIdsArr = this.visitPreventiveTreatmentsList.map( i => i.treatmentId);
    this.filteredPreventiveTreatmentsList = this.filteredPreventiveTreatmentsList.filter(
      item => {
        return !TreatmentsIdsArr.includes(item.treatmentId);
      }
    );
  }

  onRowSelect(treatment: any): void {
    this.ref.close(treatment);
  }

  searchInputOnChange($event: Event): void {
    const searchString = ($event.target as HTMLInputElement).value.toLowerCase();
    const TreatmentsIdsArr = this.visitPreventiveTreatmentsList.map( i => i.treatmentId);
    this.filteredPreventiveTreatmentsList = this.preventiveTreatmentsList.filter(
      item => {
        return !TreatmentsIdsArr.includes(item.treatmentId);
      }
    );
    this.filteredPreventiveTreatmentsList = this.filteredPreventiveTreatmentsList.filter(d => d.name.toLowerCase().includes(searchString));
  }

  changeCheck($event: any): void {
    this.selectedTreatments = $event as PreventiveTreatment[];
  }

  onAddPreventiveTreatments(): void {
    this.ref.close(this.selectedTreatments);
  }
}
