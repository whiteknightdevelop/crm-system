import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { PreventiveTreatmentComponent } from './preventive-treatment/preventive-treatment.component';
import { PreventiveDialogComponent } from './preventive-dialog/preventive-dialog.component';
import {FormsModule} from '@angular/forms';
import {EmptyListErrorModule} from '../../../../custom-components/empty-list-error/empty-list-error.module';


@NgModule({
  imports: [
    CommonModule,
    TableModule,
    ButtonModule,
    CheckboxModule,
    FormsModule,
    EmptyListErrorModule,
  ],
  declarations: [
    PreventiveTreatmentComponent,
    PreventiveDialogComponent,
  ],
  exports: [
    PreventiveTreatmentComponent
  ]
})
export class PreventiveTreatmentModule { }

