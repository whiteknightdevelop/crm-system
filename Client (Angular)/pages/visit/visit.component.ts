import {Component, OnInit, OnDestroy} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import { distinctUntilChanged, switchMap } from 'rxjs/operators';
import { DiagnosisListComponent } from '../visit/diagnosis/diagnosis-list/diagnosis-list.component';
import { DynamicDialogRef} from 'primeng/dynamicdialog';
import {TreatmentListComponent} from './treatment/treatment-list/treatment-list.component';
import {Visit, VisitEntity} from '../../models/visit';
import {VisitService} from './visit.service';
import {HandleError, HttpErrorHandler} from '../../error-handlers/http-error-handler.service';
import {ActivatedRoute, Router} from '@angular/router';
import {ConfirmationService, MenuItem, MessageService, PrimeNGConfig} from 'primeng/api';
import {Observable} from 'rxjs';
import {VisitPage, VisitPageEntity} from '../../models/visit-page';
import {HelperService} from '../../helper.service';
import {PreventiveTreatment} from '../../models/preventive-treatment';
import {PreventiveTreatmentService} from './treatment/preventive-treatment/preventive-treatment/preventive-treatment.service';
import {Diagnosis} from '../../models/diagnosis';
import {DATE_REGEX, PULSE_REGEX, TEMPERATURE_REGEX, WEIGHT_REGEX} from '../../models/RegexPatterns';
import {formatDate} from '@angular/common';
import {PrintService} from '../print/print.service';
import {AuthService} from '../../auth/auth.service';

@Component({
  selector: 'app-visit',
  templateUrl: './visit.component.html',
  styleUrls: ['./visit.component.css'],
  providers: [
    VisitService,
    PreventiveTreatmentService,
    ConfirmationService,
  ]
})
export class VisitComponent implements OnInit, OnDestroy {

  private readonly handleError: HandleError;
  constructor(private fb: FormBuilder,
              private visitService: VisitService,
              private preventiveTreatmentService: PreventiveTreatmentService,
              private route: ActivatedRoute,
              private router: Router,
              private confirmationService: ConfirmationService,
              private messageService: MessageService,
              private helperService: HelperService,
              private httpErrorHandler: HttpErrorHandler,
              private primengConfig: PrimeNGConfig,
              private printService: PrintService,
              private authService: AuthService) {
    this.handleError = httpErrorHandler.createHandleError('VisitComponent');
    this.menuItems = [];
    this.pageIsLoaded = false;
    this.showProgress = false;
    this.inCreateNewMode = false;
    this.h1Header = '';
    this.h1Description = '';
    this.visitPage$ = new Observable<VisitPage>();
    this.visitPage = new VisitPageEntity();
    this.diagnosisList = [];
    this.ref = new DynamicDialogRef();
    this.errorMessage = '';
  }

  visitDetailsForm = this.fb.group({
    visitId: 0,
    animalId: 0,
    visitTime: [formatDate(0, 'yyyy-MM-dd', 'he'), Validators.compose([Validators.required, Validators.pattern(DATE_REGEX)])],
    labResults: [null, Validators.compose([
      Validators.maxLength(2000)])
    ],
    cause: [null, Validators.compose([
      Validators.required,
      Validators.maxLength(2000)])
    ],
    symptoms: [null, Validators.compose([
      Validators.maxLength(2000)])
    ],
    comment: [null, Validators.compose([
      Validators.maxLength(1000)])
    ],
    temperature: [0, Validators.compose([Validators.pattern(TEMPERATURE_REGEX)])],
    weight: [0, Validators.compose([Validators.pattern(WEIGHT_REGEX)])],
    pulse: [0, Validators.compose([Validators.pattern(PULSE_REGEX)])],
    diagnosis1: ['', Validators.compose([Validators.maxLength(280)])],
    diagnosis2: ['', Validators.compose([Validators.maxLength(280)])],
    diagnosis3: ['', Validators.compose([Validators.maxLength(280)])],
    treatment1: ['', Validators.compose([Validators.maxLength(280)])],
    treatment2: ['', Validators.compose([Validators.maxLength(280)])],
    treatment3: ['', Validators.compose([Validators.maxLength(280)])],
    treatment4: ['', Validators.compose([Validators.maxLength(280)])],
    treatment5: ['', Validators.compose([Validators.maxLength(280)])],
    treatment6: ['', Validators.compose([Validators.maxLength(280)])],
  });

  get visitId(): any { return this.visitDetailsForm.get('visitId'); }
  get animalId(): any { return this.visitDetailsForm.get('animalId'); }
  get visitTime(): any { return this.visitDetailsForm.get('visitTime'); }
  get labResults(): any { return this.visitDetailsForm.get('labResults'); }
  get cause(): any { return this.visitDetailsForm.get('cause'); }
  get symptoms(): any { return this.visitDetailsForm.get('symptoms'); }
  get comment(): any { return this.visitDetailsForm.get('comment'); }
  get temperature(): any { return this.visitDetailsForm.get('temperature'); }
  get weight(): any { return this.visitDetailsForm.get('weight'); }
  get pulse(): any { return this.visitDetailsForm.get('pulse'); }
  get diagnosis1(): any { return this.visitDetailsForm.get('diagnosis1'); }
  get diagnosis2(): any { return this.visitDetailsForm.get('diagnosis2'); }
  get diagnosis3(): any { return this.visitDetailsForm.get('diagnosis3'); }
  get treatment1(): any { return this.visitDetailsForm.get('treatment1'); }
  get treatment2(): any { return this.visitDetailsForm.get('treatment2'); }
  get treatment3(): any { return this.visitDetailsForm.get('treatment3'); }
  get treatment4(): any { return this.visitDetailsForm.get('treatment4'); }
  get treatment5(): any { return this.visitDetailsForm.get('treatment5'); }
  get treatment6(): any { return this.visitDetailsForm.get('treatment6'); }

  menuItems: MenuItem[];
  pageIsLoaded: boolean;
  showProgress: boolean;
  inCreateNewMode: boolean;
  h1Header: string;
  h1Description: string;
  private visitPage$: Observable<VisitPage>;
  visitPage: VisitPage;
  diagnosisList: Diagnosis[];
  ref: DynamicDialogRef;
  diagnosisListComponent: any;
  treatmentListComponent: any;
  errorMessage: string;

  ngOnInit(): void {
    this.primengConfig.ripple = true;
    this.showProgress = true;
    this.diagnosisListComponent = DiagnosisListComponent;
    this.treatmentListComponent = TreatmentListComponent;

    this.route.data.subscribe(data => {
      switch (data.kind) {
        case 'new': {
          this.inCreateNewMode = true;
          this.h1Header = 'ביקור חדש';
          this.h1Description = 'יצירת ביקור חדש';
          break;
        }
        case 'edit': {
          this.inCreateNewMode = false;
          this.h1Header = 'פרטי ביקור';
          this.h1Description = 'דף המכיל את פרטי הביקור';
          break;
        }
        default: {
          break;
        }
      }
    });

    if (this.inCreateNewMode){
      this.visitDetailsForm.patchValue({
        animalId: this.route.snapshot.paramMap.get('animalId'),
        visitTime: formatDate(new Date(), 'yyyy-MM-dd', 'he')
      });

      this.visitService.getVisitLists().subscribe(data => {
        this.visitPage.lists = {
          diagnosisList: data.diagnosisList,
          treatmentsList: data.treatmentsList,
          allPreventiveTreatmentsList: data.allPreventiveTreatmentsList,
        };
        this.showProgress = false;
        this.pageIsLoaded = true;
      }, error => {
        this.showProgress = false;
        this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
      });
    }else{
      this.visitPage$ = this.route.paramMap.pipe(
        switchMap(params => {
          const visitId = Number(params.get('id'));
          return this.visitService.getVisit(visitId);
        })
      );

      this.visitPage$.subscribe(data => {
        this.visitPage = data;
        this.visitDetailsForm.patchValue({
            visitId: this.visitPage.visit.visitId,
            animalId: this.visitPage.visit.animalId,
            visitTime: this.visitPage.visit.visitTime ? formatDate(this.visitPage.visit.visitTime, 'yyyy-MM-dd', 'he') : null,
            labResults: this.visitPage.visit.labResults,
            cause: this.visitPage.visit.cause,
            symptoms: this.visitPage.visit.symptoms,
            comment: this.visitPage.visit.comment,
            temperature: this.visitPage.visit.temperature,
            weight: this.visitPage.visit.weight,
            pulse: this.visitPage.visit.pulse,
            diagnosis1: this.visitPage.visit.diagnosis1,
            diagnosis2: this.visitPage.visit.diagnosis2,
            diagnosis3: this.visitPage.visit.diagnosis3,
            treatment1: this.visitPage.visit.treatment1,
            treatment2: this.visitPage.visit.treatment2,
            treatment3: this.visitPage.visit.treatment3,
            treatment4: this.visitPage.visit.treatment4,
            treatment5: this.visitPage.visit.treatment5,
            treatment6: this.visitPage.visit.treatment6,
        });

        this.changeDisabledMenuStatus(true);
        this.showProgress = false;
        this.pageIsLoaded = true;
        this.onChanges();
      }, error => {
        this.showProgress = false;
        this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
      });
    }
  }

  onChanges(): void {
    this.visitDetailsForm.valueChanges.pipe(
      distinctUntilChanged(),
    ).subscribe(formData => {
      if (this.visitDetailsForm.valid){
        this.changeDisabledMenuStatus(false);
      }else {
        this.changeDisabledMenuStatus(true);
      }
    }, error => {
      this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
    });
  }

  private changeDisabledMenuStatus(isDisabled: boolean): void {
    this.menuItems = [
      {
        label: 'שמור',
        icon: 'pi pi-fw pi-save',
        command: () => this.UpdateServerWithVisitChanges(),
        disabled: isDisabled
      },
      {
        label: 'מרשם',
        icon: 'pi pi-fw pi-book',
        styleClass: this.checkIfPrescriptionsExist(),
        command: () => this.goToPrescriptionpage(),
      },
      {
        label: 'אפשרויות',
        icon: 'pi pi-fw pi-cog',
        items: [
          {
            label: 'ביקור חדש',
            icon: 'pi pi-fw pi-plus',
            command: () => this.goToNewVisitpage(),
          },
          {
            label: 'מחיקה',
            icon: 'pi pi-fw pi-trash',
            command: () => this.onDelete()
          },
        ]
      },
      {
        label: 'הדפסה',
        icon: 'pi pi-fw pi-print',
        command: () => this.printVisit(),
      },
      {
        label: 'חזרה לבע"ח',
        icon: 'pi pi-fw pi-arrow-left',
        command: () => this.goBackToAnimalpage(),
      },
    ];
  }

  private checkIfPrescriptionsExist(): string {
    const prescriptionsNumber = this.visitPage.prescriptionsNumber;
    if (prescriptionsNumber > 0) {
      return 'red-alert';
    }else {
      return '';
    }
  }

  private UpdateServerWithVisitChanges(): void {
    this.visitService.updateVisit(this.FormToVisitMapper(this.visitDetailsForm)).subscribe(data => {
      if (data) {
        this.showSaveChangesMsg();
        this.changeDisabledMenuStatus(true);
      }
    }, error => {
      this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
    });
  }

  private goToNewVisitpage(): void {
    this.router.navigate(['/visit', { animalId: this.animalId.value }]);
  }

  private goToPrescriptionpage(): void {
    this.router.navigate(['/prescription', { visitId: this.visitId.value }]);
  }

  private goBackToAnimalpage(): void {
    this.router.navigate(['/animal', this.animalId.value]);
  }

  printVisit(): void  {
    this.printService
      .printDocument('visit-print', { visitId: this.visitId.value });
  }

  ngOnDestroy(): void {
    if (this.ref) {
      this.ref.close();
    }
  }

  onAdd(): void {
    if (this.visitDetailsForm.valid){
      const visit = this.FormToVisitMapper(this.visitDetailsForm);
      this.visitService.addVisit(visit).subscribe(visitIdAdded => {
        if (Number(visitIdAdded)){
          this.router.navigateByUrl('/visit/' + visitIdAdded);
        }
      }, error => {
        this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
      });
    }
  }

  addNewPreventiveTreatment(list: PreventiveTreatment[]): void {
    list.forEach(treatment => {
      treatment.visitId = this.visitPage.visit.visitId;
    });

    this.preventiveTreatmentService.addNewPreventiveTreatmentsList(list).subscribe( (ans) => {
      if (Number(ans)){
        this.showSaveChangesMsg();
        this.preventiveTreatmentService.getVisitPreventiveTreatmentsList(this.visitPage.visit.visitId).subscribe(data => {
          if (data){
            this.visitPage.preventiveTreatmentsList = data;
          }
        }, error => {
          this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
        });
      }
    }, error => {
      this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
    });
  }

  deleteSelectedPreventiveTreatment(list: PreventiveTreatment[]): void {
    this.confirmationService.confirm({
      header: 'מחיקת טיפולים מונעים',
      message: 'האם הינך בטוח כי ברצונך למחוק את ההטיפולים המסומנית?',
      icon: 'pi pi-exclamation-triangle',
      acceptLabel: 'מחק',
      rejectLabel: 'בטל',
      acceptIcon: 'pi pi-trash',
      acceptButtonStyleClass: 'confirmDialogAcceptButton',
      accept: () => {
        this.preventiveTreatmentService.deletePreventiveTreatments(list).subscribe(ans => {
          if (ans){
            this.showSaveChangesMsg();
            this.preventiveTreatmentService.getVisitPreventiveTreatmentsList(this.visitPage.visit.visitId).subscribe(data => {
              if (data){
                this.visitPage.preventiveTreatmentsList = data;
              }
            }, error => {
              this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
            });
          }
        }, error => {
          this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
        });
      });
  }

  showSaveChangesMsg(): void {
    this.messageService.add({key: 'showSaveChanges', severity: 'success', summary: 'השינויים נשמרו בהצלחה', life: 500});
  }

  onDelete(): void {
    this.confirmationService.confirm({
      header: 'מחיקת ביקור',
      message: 'האם הינך בטוח כי ברצונך למחוק ביקור זה?',
      icon: 'pi pi-exclamation-triangle',
      acceptLabel: 'מחק',
      rejectLabel: 'בטל',
      acceptIcon: 'pi pi-trash',
      acceptButtonStyleClass: 'confirmDialogAcceptButton',
      accept: () => {
        this.visitService.deleteVisit(this.visitPage.visit).subscribe(ans => {
          if (ans){

            this.messageService.add({severity: 'success', summary: 'הביקור נמחק!', detail: '' });
            setTimeout(() =>
            {
              this.router.navigateByUrl('/animal/' + this.visitPage.animal.animalId);
            }, 3000);
          }
        }, error => {
          this.messageService.add({severity: 'error', summary: error.error, detail: this.httpErrorHandler.getErrorMessage(error)});
        });
      });
  }

  FormToVisitMapper(form: FormGroup): Visit {
    const visit = new VisitEntity();
    visit.setData(form);
    visit.user = this.authService.getUser();
    return visit;
  }
}




