import { Component, OnInit } from '@angular/core';
import {HandleError, HttpErrorHandler} from '../../error-handlers/http-error-handler.service';
import {ConfirmationService, MenuItem, MessageService, PrimeNGConfig} from 'primeng/api';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {PRESCRIPTIONDOSAGE_REGEX} from '../../models/RegexPatterns';
import {PrescriptionPage, PrescriptionPageEntity} from '../../models/prescription-page';
import {PrescriptionService} from './prescription.service';
import {ActivatedRoute, Router} from '@angular/router';
import {Prescription, PrescriptionEntity} from '../../models/prescription';

@Component({
  selector: 'app-prescription',
  templateUrl: './prescription.component.html',
  styleUrls: ['./prescription.component.css'],
  providers: [PrescriptionService],
})
export class PrescriptionComponent implements OnInit {

  constructor(private fb: FormBuilder, private httpErrorHandler: HttpErrorHandler, private primengConfig: PrimeNGConfig,
              private prescriptionService: PrescriptionService, private route: ActivatedRoute,
              private router: Router,
              private confirmationService: ConfirmationService,
              private messageService: MessageService) {
    this.handleError = httpErrorHandler.createHandleError('PrescriptionComponent');
    this.menuItems = [];
    this.pageIsLoaded = false;
    this.showProgress = false;
    this.h1Header = '';
    this.h1Description = '';
    this.prescriptionPage = new PrescriptionPageEntity();
    this.drugsList = [];
    this.dosagesList = [];
    this.frequencysList = [];
    this.periodsList = [];
    this.rowInEditMode = false;
    this.currentVisitId = 0;
    this.addNewOpened = false;
    this.tableDataLoaded = false;
  }
  private readonly handleError: HandleError;
  menuItems: MenuItem[];
  pageIsLoaded: boolean;
  showProgress: boolean;
  h1Header: string;
  h1Description: string;
  prescriptionPage: PrescriptionPage;
  drugsList: {drugName: string}[];
  dosagesList: {drugDosage: string}[];
  frequencysList: {drugFrequency: string}[];
  periodsList: {drugPeriod: string}[];
  rowInEditMode: boolean;
  currentVisitId: number;
  addNewOpened: boolean;
  tableDataLoaded: boolean;

  prescriptionForm = this.fb.group({
    visitId: 0,
    drugName: ['', Validators.compose([Validators.required])],
    drugDosage: ['', Validators.compose([Validators.maxLength(45), Validators.pattern(PRESCRIPTIONDOSAGE_REGEX)])],
    drugFrequency: ['', Validators.compose([Validators.required])],
    drugPeriod: ['', Validators.compose([Validators.required])],
    drugComment: ['', Validators.compose([Validators.pattern(PRESCRIPTIONDOSAGE_REGEX)])],
  });

  get prescriptionId(): any { return this.prescriptionForm.get('prescriptionId'); }
  get visitId(): any { return this.prescriptionForm.get('visitId'); }
  get prescriptionListIndex(): any { return this.prescriptionForm.get('prescriptionListIndex'); }
  get drugName(): any { return this.prescriptionForm.get('drugName'); }
  get drugFrequency(): any { return this.prescriptionForm.get('drugFrequency'); }
  get drugDosage(): any { return this.prescriptionForm.get('drugDosage'); }
  get drugPeriod(): any { return this.prescriptionForm.get('drugPeriod'); }
  get drugComment(): any { return this.prescriptionForm.get('drugComment'); }

  ngOnInit(): void {
    this.primengConfig.ripple = true;
    this.showProgress = true;
    this.h1Header = 'מרשם';
    this.h1Description = 'דף המכיל פרטי מרשם של הביקור';
    this.currentVisitId = Number(this.route.snapshot.paramMap.get('visitId'));
    this.prescriptionService.getPrescriptionPage(this.currentVisitId).subscribe(data => {
      if (data) {
        this.prescriptionPage = data;
        this.prescriptionForm.patchValue({
          visitId: this.prescriptionPage.visit.visitId,
        });
        this.drugsList = this.prescriptionPage.drugsList.map((item: any) => ({drugName: item}));
        this.dosagesList = this.prescriptionPage.dosagesList.map((item: any) => ({drugDosage: item}));
        this.frequencysList = this.prescriptionPage.frequencysList.map((item: any) => ({drugFrequency: item}));
        this.periodsList = this.prescriptionPage.periodsList.map((item: any) => ({drugPeriod: item}));
      }
    }, error => {
      this.showProgress = false;
      this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
    }, () => {
      this.changeDisabledMenuStatus(true);
      this.tableDataLoaded = true;
      this.showProgress = false;
      this.pageIsLoaded = true;
    });
  }

  private changeDisabledMenuStatus(isDisabled: boolean): void {
    this.menuItems = [
      {
        label: 'חזרה לביקור',
        icon: 'pi pi-fw pi-arrow-left',
        command: () => this.goBackToVisitpage(),
      },
    ];
  }
  private goBackToVisitpage(): void {
    this.router.navigate(['/visit', this.prescriptionPage.visit.visitId]);
  }

  getPrescriptionsTableData(): void {
    this.tableDataLoaded = false;
    this.prescriptionService.getPrescriptionsList(this.currentVisitId).subscribe(list => {
      if (list) {
        this.prescriptionPage.prescriptionsList = list;
        this.tableDataLoaded = true;
      }
    }, error => {
      this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
    });
  }

  onAddPrescription(): void  {
    if (this.prescriptionForm.valid){
      const prescription = this.FormToPrescriptionIdAddedMapper(this.prescriptionForm);
      this.prescriptionService.addPrescription(prescription).subscribe(prescriptionIdAdded => {
        if (Number(prescriptionIdAdded)){
          this.initializeFormData();
          this.getPrescriptionsTableData();
          this.showSaveChangesMsg();
          this.addNewOpened = false;
        }
      }, error => {
        this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
      });
    }
  }

  onEditButtonClick(): void {
    this.rowInEditMode = !this.rowInEditMode;
  }

  initializeFormData(): void {
    this.prescriptionForm.patchValue({
      drugName: '',
      drugDosage: '',
      drugFrequency: '',
      drugPeriod: '',
      drugComment: '',
    });
  }

  showSaveChangesMsg(): void {
    this.messageService.add({key: 'showSaveChanges', severity: 'success', summary: 'השינויים נשמרו בהצלחה', life: 1000});
  }

  onDelete($event: any, prescription: Prescription): void {
    this.confirmationService.confirm({
      header: 'מחיקת תרופה',
      message: 'האם הינך בטוח כי ברצונך למחוק תרופה זו?',
      icon: 'pi pi-exclamation-triangle',
      acceptLabel: 'מחק',
      rejectLabel: 'בטל',
      acceptIcon: 'pi pi-trash',
      acceptButtonStyleClass: 'confirmDialogAcceptButton',
      accept: () => {
        this.prescriptionService.deletePrescription(prescription).subscribe(ans => {
          if (ans){
            this.messageService.add({key: 'showSaveChanges', severity: 'success', summary: 'התרופה נמחקה!', life: 1000});
            this.getPrescriptionsTableData();
          }
        }, error => {
          this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
        });
      },
      reject: () => {}
    });
  }

  FormToPrescriptionIdAddedMapper(form: FormGroup): Prescription {
    const prescription = new PrescriptionEntity();
    prescription.setData(form);

    return prescription;
  }

  showAddNewRow(): void {
    this.addNewOpened = true;
  }
}


