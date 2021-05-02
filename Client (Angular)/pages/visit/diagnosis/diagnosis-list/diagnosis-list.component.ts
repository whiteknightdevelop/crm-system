import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {DynamicDialogConfig, DynamicDialogRef} from 'primeng/dynamicdialog';
import { Diagnosis } from '../../../../models/diagnosis';
import { DiagnosisListService } from '../../../visit/diagnosis/diagnosis-list/diagnosis-list.service';

@Component({
  selector: 'app-diagnosis-list',
  templateUrl: './diagnosis-list.component.html',
  styleUrls: ['./diagnosis-list.component.css'],
})
export class DiagnosisListComponent implements OnInit {

  constructor(private service: DiagnosisListService,
              private route: ActivatedRoute,
              public config: DynamicDialogConfig,
              public ref: DynamicDialogRef) {
    this.diagnoses = [];
    this.filteredDiagnosesList = [];
  }

  diagnoses: Diagnosis[];
  filteredDiagnosesList: Diagnosis[];

  ngOnInit(): void {
    this.diagnoses = this.config.data as Diagnosis[];
    this.filteredDiagnosesList = this.diagnoses;
  }

  onRowSelect(event: { data: any; }): void {
    this.ref.close(event.data);
  }

  searchInputOnChange($event: Event): void {
    const searchString = ($event.target as HTMLInputElement).value.toLowerCase();
    this.filteredDiagnosesList = this.diagnoses.filter(d => d.name.toLowerCase().includes(searchString));
  }
}
