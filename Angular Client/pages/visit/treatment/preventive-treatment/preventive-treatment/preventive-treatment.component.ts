import {Component, forwardRef, Input, Output} from '@angular/core';
import {PreventiveTreatment} from '../../../../../models/preventive-treatment';
import {DialogService, DynamicDialogRef} from 'primeng/dynamicdialog';
import {NG_VALUE_ACCESSOR} from '@angular/forms';
import {PreventiveDialogComponent} from '../preventive-dialog/preventive-dialog.component';
import {EventEmitter} from '@angular/core';

@Component({
  selector: 'app-preventive-treatment',
  templateUrl: './preventive-treatment.component.html',
  styleUrls: ['./preventive-treatment.component.css'],
  providers: [DialogService,
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => PreventiveTreatmentComponent),
      multi: true
    }]
})
export class PreventiveTreatmentComponent implements OnInit {

  constructor(public dialogService: DialogService) {
    this.visitPreventiveTreatments = [];
    this.allPreventiveTreatments = [];
    this.addNewPreventiveTreatment = new EventEmitter<PreventiveTreatment>();
    this.addNewPreventiveTreatmentList = new EventEmitter<PreventiveTreatment[]>();
    this.deleteSelectedPreventiveTreatment = new EventEmitter<PreventiveTreatment[]>();
    this.selectedTreatments = [];
    this.ref = new DynamicDialogRef();
  }

  @Input() visitPreventiveTreatments: PreventiveTreatment[];
  @Input() allPreventiveTreatments: PreventiveTreatment[];
  @Output() addNewPreventiveTreatment: EventEmitter<PreventiveTreatment>;
  @Output() addNewPreventiveTreatmentList: EventEmitter<PreventiveTreatment[]>;
  @Output() deleteSelectedPreventiveTreatment: EventEmitter<PreventiveTreatment[]>;
  selectedTreatments: PreventiveTreatment[];
  ref: DynamicDialogRef;

  addPreventiveTreatment(): void {
    this.ref = this.dialogService.open(PreventiveDialogComponent, {
      header: 'הוסף טיפול מונע חדש',
      width: '40%',
      contentStyle: {'max-height': '550px', overflow: 'auto'},
      baseZIndex: 10000,
      closeOnEscape: true,
      dismissableMask: true,
      data: {
        allPreventiveTreatments: this.allPreventiveTreatments,
        visitPreventiveTreatmentsList: this.visitPreventiveTreatments
      }
    });

    this.ref.onClose.subscribe((treatments: PreventiveTreatment[]) => {
      if (treatments) {
        this.addNewPreventiveTreatmentList.emit(treatments);
      }
    });
  }

  changeCheck($event: any): void {
    this.selectedTreatments = $event as PreventiveTreatment[];
  }

  deleteSelectedTreatments(): void  {
    this.deleteSelectedPreventiveTreatment.emit(this.selectedTreatments);
    this.selectedTreatments = [];
  }
}
