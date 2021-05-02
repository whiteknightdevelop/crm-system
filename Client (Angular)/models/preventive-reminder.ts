export interface PreventiveReminder {
  reminderId: number;
  animalId: number;
  visitId: number;
  treatmentId: number;
  preventiveReminderName: string;
  reminderDate: Date;
  remainingNumOfDays: number;
  isReminderChecked: boolean;
  isReminderSent: boolean;
  isReminderDeleted: boolean;
  preventiveTreatmentType: boolean;
  userId: number;
}
