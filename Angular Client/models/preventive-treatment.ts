import {ListType} from './list';

export interface PreventiveTreatment extends ListType {
  visitId: number;
  treatmentId: number;
  name: string;
  remainingNumOfDays: number;
  nextTreatmentId: number;
  nextTreatmentName: string;
}
