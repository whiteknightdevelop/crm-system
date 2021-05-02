import {NgModule} from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import {HttpErrorHandler} from '../../error-handlers/http-error-handler.service';
import {ErrorMessageService} from '../../error-handlers/error-message.service';
import {SterilizationPermitComponent} from './sterilization-permit/sterilization-permit.component';
import {InvoiceComponent} from '../../layout/print-layout/invoice/invoice.component';
import {PipesModule} from '../../pipes/pipes.module';
import {PrintService} from './print.service';
import {RabiesVaccineComponent} from './rabies-vaccine/rabies-vaccine.component';
import {DogLicenceComponent} from './dog-licence/dog-licence.component';
import {ButtonModule} from 'primeng/button';
import {CardModule} from 'primeng/card';
import {TableModule} from 'primeng/table';
import {EmptyListErrorModule} from '../../custom-components/empty-list-error/empty-list-error.module';
import {ProgressBarModule} from 'primeng/progressbar';
import {MessagesModule} from 'primeng/messages';
import {RabiesListByDatePrintComponent} from './rabies-list-by-date/rabies-list-by-date-print/rabies-list-by-date-print.component';
import {RabiesListByDatePageService} from './rabies-list-by-date/rabies-list-by-date-page/rabies-list-by-date-page.service';
import {RabiesListByDatePageComponent} from './rabies-list-by-date/rabies-list-by-date-page/rabies-list-by-date-page.component';
import {RabiesListByDatePrintService} from './rabies-list-by-date/rabies-list-by-date-print/rabies-list-by-date-print.service';
import { DebtsSheetPageComponent } from './debts-sheet/debts-sheet-page/debts-sheet-page.component';
import { DebtsSheetPrintComponent } from './debts-sheet/debts-sheet-print/debts-sheet-print.component';
import {DebtsSheetPageService} from './debts-sheet/debts-sheet-page/debts-sheet-page.service';
import { VisitedLastDaysPageComponent } from './visited-last-days/visited-last-days-page/visited-last-days-page.component';
import { VisitedLastDaysPrintComponent } from './visited-last-days/visited-last-days-print/visited-last-days-print.component';
import {VisitedLastDaysPageService} from './visited-last-days/visited-last-days-page/visited-last-days-page.service';
import { NotVisitedLastDaysPageComponent } from './visited-not-last-days/not-visited-last-days-page/not-visited-last-days-page.component';
import { NotVisitedLastDaysPrintComponent } from './visited-not-last-days/not-visited-last-days-print/not-visited-last-days-print.component';
import {NotVisitedLastDaysPageService} from './visited-not-last-days/not-visited-last-days-page/not-visited-last-days-page.service';
import { AnimalPrintComponent } from './animal-print/animal-print.component';
import { VisitPrintComponent } from './visit-print/visit-print.component';
import {AnimalPrintService} from './animal-print/animal-print.service';

@NgModule({
  imports: [
    CommonModule,
    PipesModule,
    ButtonModule,
    CardModule,
    ReactiveFormsModule,
    FormsModule,
    TableModule,
    EmptyListErrorModule,
    ProgressBarModule,
    MessagesModule,
  ],
  declarations: [
    SterilizationPermitComponent,
    RabiesVaccineComponent,
    InvoiceComponent,
    DogLicenceComponent,
    RabiesListByDatePrintComponent,
    RabiesListByDatePageComponent,
    DebtsSheetPageComponent,
    DebtsSheetPrintComponent,
    VisitedLastDaysPageComponent,
    VisitedLastDaysPrintComponent,
    NotVisitedLastDaysPageComponent,
    NotVisitedLastDaysPrintComponent,
    AnimalPrintComponent,
    VisitPrintComponent,
  ],
  providers: [
    HttpErrorHandler,
    ErrorMessageService,
    PrintService,
    RabiesListByDatePageService,
    RabiesListByDatePrintService,
    DebtsSheetPageService,
    VisitedLastDaysPageService,
    NotVisitedLastDaysPageService,
    AnimalPrintService,
  ],
})
export class PrintModule {}
