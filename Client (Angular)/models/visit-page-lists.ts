import {Diagnosis} from './diagnosis';
import {Treatment} from './treatment';
import {PreventiveTreatment} from './preventive-treatment';

export interface VisitPageLists {
  diagnosisList: Diagnosis[];
  treatmentsList: Treatment[];
  allPreventiveTreatmentsList: PreventiveTreatment[];
}

export class VisitPageListsEntity implements VisitPageLists{
  constructor() {
    this.allPreventiveTreatmentsList = [];
    this.diagnosisList = [];
    this.treatmentsList = [];
  }
  allPreventiveTreatmentsList: PreventiveTreatment[];
  diagnosisList: Diagnosis[];
  treatmentsList: Treatment[];
}
