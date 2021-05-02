import {Component, EventEmitter, forwardRef, Input, OnInit, Output} from '@angular/core';
import {DialogService, DynamicDialogRef} from 'primeng/dynamicdialog';
import {FormBuilder, NG_VALUE_ACCESSOR, Validators} from '@angular/forms';
import {PreventiveReminder} from '../../../../models/preventive-reminder';
import {ConfirmationService, MessageService, PrimeNGConfig} from 'primeng/api';
import { distinctUntilChanged} from 'rxjs/operators';
import {PreventiveReminderListService} from './preventive-reminder-list.service';
import {Animal, AnimalEntity} from '../../../../models/animal';
import {formatDate} from '@angular/common';
import {DATE_REGEX} from '../../../../models/RegexPatterns';
import {HttpErrorHandler} from '../../../../error-handlers/http-error-handler.service';
import {Reminder} from '../../../../models/reminder';
import {Router} from '@angular/router';

@Component({
  selector: 'app-preventive-reminder-list',
  templateUrl: './preventive-reminder-list.component.html',
  styleUrls: ['./preventive-reminder-list.component.css'],
  providers: [DialogService,
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => PreventiveReminderListComponent),
      multi: true
    }]
})
export class PreventiveReminderListComponent implements OnInit {

  constructor(public dialogService: DialogService,
              private confirmationService: ConfirmationService,
              private fb: FormBuilder,
              private reminderService: PreventiveReminderListService,
              private messageService: MessageService,
              private httpErrorHandler: HttpErrorHandler,
              private router: Router,
              private primengConfig: PrimeNGConfig) {
    this.inputAnimal = new AnimalEntity();
    this.list = [];
    this.remindersList = [];
    this.showSaveChangesMsg = new EventEmitter();
    this.preventiveRemindersList = [];
    this.selectedReminders = [];
    this.ref = new DynamicDialogRef();
    this.reminderDialogVisible = false;
    this.confirmBtnenabled = false;
    this.remainingNumOfDaysNegative = false;
  }

  @Input() inputAnimal: Animal;
  @Input() list: PreventiveReminder[];
  @Input() remindersList: { reminder: Reminder }[];
  @Output() showSaveChangesMsg: any;
  preventiveRemindersList: PreventiveReminder[];
  selectedReminders: PreventiveReminder[];
  ref: DynamicDialogRef;
  reminderDialogVisible: boolean;
  confirmBtnenabled: boolean;
  remainingNumOfDaysNegative: boolean;

  reminderDialogForm = this.fb.group({
    dateOfReminder: [formatDate(0, 'yyyy-MM-dd', 'he'), Validators.compose([Validators.required, Validators.pattern(DATE_REGEX)])],
    textOfReminder: [null, Validators.compose([Validators.required, Validators.maxLength(20)])],
  });

  get dateOfReminder(): any { return this.reminderDialogForm.get('dateOfReminder'); }
  get textOfReminder(): any { return this.reminderDialogForm.get('textOfReminder'); }

  ngOnInit(): void {
    this.primengConfig.ripple = true;
    this.onChanges();
    this.confirmBtnenabled = false;
    this.preventiveRemindersList = this.list;
    this.reminderDialogForm.patchValue({
      dateOfReminder: new Date(Date.now())
    });
  }

  onChanges(): void {
    this.reminderDialogForm.valueChanges.pipe(
      distinctUntilChanged(),
    ).subscribe(formData => {
      if (this.reminderDialogForm.valid){
        this.confirmBtnenabled = true;
      }else {
        this.confirmBtnenabled = false;
      }
    });
  }

  openDialogPreventiveReminder(): void {
    this.reminderDialogForm.reset();
    this.reminderDialogVisible = true;
  }

  addPreventiveReminder(): void {
    this.reminderDialogVisible = false;
    this.reminderService.addPreventiveReminder({
      animalId: this.inputAnimal.animalId,
      isReminderChecked: false,
      isReminderDeleted: false,
      isReminderSent: false,
      preventiveReminderName: this.textOfReminder.value.reminder,
      preventiveTreatmentType: false,
      remainingNumOfDays: 0,
      reminderDate: new Date(this.dateOfReminder.value),
      reminderId: 0,
      treatmentId: 0,
      userId: 0,
      visitId: 0
    }).subscribe(), error => {
      this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
    }, () => {
      this.showSaveChangesMsg.emit();
      this.reminderService.getPreventiveReminderList(this.inputAnimal.animalId).subscribe(data => {
        this.preventiveRemindersList = data;
      }, error => {
        this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
      });
    });
  }

  deletePreventiveReminder(): void {
    this.confirmationService.confirm({
      header: 'מחיקת תזכורת',
      message: 'האם הינך בטוח כי ברצונך למחוק את התזכורות המסומנות?',
      icon: 'pi pi-exclamation-triangle',
      acceptLabel: 'מחק',
      rejectLabel: 'בטל',
      acceptIcon: 'pi pi-trash',
      acceptButtonStyleClass: 'confirmDialogAcceptButton',
      closeOnEscape: true,
      dismissableMask: true,
      accept: () => {
        this.reminderService.deletePreventiveReminder(this.selectedReminders).subscribe(ans => {
          if (ans){
            this.showSaveChangesMsg.emit();
            this.selectedReminders = [];
            this.reminderService.getPreventiveReminderList(this.inputAnimal.animalId).subscribe(data => {
              this.preventiveRemindersList = data;
            }, error => {
              this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
            });
          }
        }, error => {
          this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
        });
      },
      reject: () => {}
    });
  }

  changeCheck($event: any): void {
    this.selectedReminders = $event as PreventiveReminder[];
  }

  goToVisit(reminder: PreventiveReminder): void {
    if (reminder.preventiveTreatmentType) {
      this.router.navigate(['/visit', reminder.visitId]);
    }
  }
}

