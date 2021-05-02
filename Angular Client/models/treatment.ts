import {ListType} from './list';

export interface Treatment extends ListType {
  treatmentId: number;
  treatmentDays: number;
  remainingNumOfDays: number;
  nextTreatmentName: string;
  nextTreatmentId: number;
}
