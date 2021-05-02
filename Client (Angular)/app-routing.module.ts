import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {MainLayoutComponent} from './layout/main-layout/main-layout.component';
import {OwnerComponent} from './pages/owner/owner.component';
import {PageNotFoundComponent} from './pages/page-not-found/page-not-found.component';
import {AnimalComponent} from './pages/animal/animal.component';
import {VisitComponent} from './pages/visit/visit.component';
import {SearchOwnerComponent} from './pages/search/search-owner/search-owner.component';
import {SearchAnimalComponent} from './pages/search/search-animal/search-animal.component';
import {PrescriptionComponent} from './pages/prescription/prescription.component';
import {DebtComponent} from './pages/debt/debt.component';
import {FollowUpComponent} from './pages/follow-up/follow-up.component';
import {AuthGuard} from './auth/auth.guard';
import {LoginComponent} from './auth/login/login.component';
import {AccountLayoutComponent} from './layout/account-layout/account-layout.component';
import {RegisterComponent} from './auth/register/register.component';
import {PrintLayoutComponent} from './layout/print-layout/print-layout.component';
import {SterilizationPermitComponent} from './pages/print/sterilization-permit/sterilization-permit.component';
import {InvoiceComponent} from './layout/print-layout/invoice/invoice.component';
import {RabiesVaccineComponent} from './pages/print/rabies-vaccine/rabies-vaccine.component';
import {DogLicenceComponent} from './pages/print/dog-licence/dog-licence.component';
import {DebtsSheetPageComponent} from './pages/print/debts-sheet/debts-sheet-page/debts-sheet-page.component';
import {RabiesListByDatePrintComponent} from './pages/print/rabies-list-by-date/rabies-list-by-date-print/rabies-list-by-date-print.component';
import {RabiesListByDatePageComponent} from './pages/print/rabies-list-by-date/rabies-list-by-date-page/rabies-list-by-date-page.component';
import {DebtsSheetPrintComponent} from './pages/print/debts-sheet/debts-sheet-print/debts-sheet-print.component';
import {VisitedLastDaysPageComponent} from './pages/print/visited-last-days/visited-last-days-page/visited-last-days-page.component';
import {VisitedLastDaysPrintComponent} from './pages/print/visited-last-days/visited-last-days-print/visited-last-days-print.component';
import {NotVisitedLastDaysPageComponent} from './pages/print/visited-not-last-days/not-visited-last-days-page/not-visited-last-days-page.component';
import {NotVisitedLastDaysPrintComponent} from './pages/print/visited-not-last-days/not-visited-last-days-print/not-visited-last-days-print.component';
import {AnimalPrintComponent} from './pages/print/animal-print/animal-print.component';
import {VisitPrintComponent} from './pages/print/visit-print/visit-print.component';
import {FollowupAllComponent} from './pages/followup-all/followup-all.component';
import {AppointmentsComponent} from './pages/sms/appointments/appointments.component';
import {PreventiveReminderComponent} from './pages/sms/preventive-reminder/preventive-reminder.component';

const routes: Routes = [
  { path: '',
    component: MainLayoutComponent,
    children: [
      { path: '', component: SearchOwnerComponent, canActivate: [AuthGuard]},
      { path: 'owner', component: OwnerComponent, data: { kind: 'new' }, canActivate: [AuthGuard]},
      { path: 'owner/:id', component: OwnerComponent, data: { kind: 'edit' }, canActivate: [AuthGuard]},
      { path: 'animal', component: AnimalComponent, data: { kind: 'new' }, canActivate: [AuthGuard]},
      { path: 'animal/:id', component: AnimalComponent, data: { kind: 'edit' }, canActivate: [AuthGuard]},
      { path: 'visit', component: VisitComponent, data: { kind: 'new' }, canActivate: [AuthGuard]},
      { path: 'visit/:id', component: VisitComponent, data: { kind: 'edit' }, canActivate: [AuthGuard]},
      { path: 'search-owner', component: SearchOwnerComponent, canActivate: [AuthGuard]},
      { path: 'search-animal', component: SearchAnimalComponent, canActivate: [AuthGuard]},
      { path: 'prescription', component: PrescriptionComponent, canActivate: [AuthGuard]},
      { path: 'debt', component: DebtComponent, canActivate: [AuthGuard]},
      { path: 'followup', component: FollowUpComponent, canActivate: [AuthGuard]},
      { path: 'followup-all', component: FollowupAllComponent, canActivate: [AuthGuard]},
      { path: 'rabies-list', component: RabiesListByDatePageComponent, canActivate: [AuthGuard]},
      { path: 'debts-sheet', component: DebtsSheetPageComponent, canActivate: [AuthGuard]},
      { path: 'visited', component: VisitedLastDaysPageComponent, canActivate: [AuthGuard]},
      { path: 'not-visited', component: NotVisitedLastDaysPageComponent, canActivate: [AuthGuard]},
      { path: 'appointments', component: AppointmentsComponent, canActivate: [AuthGuard]},
      { path: 'preventive-reminder', component: PreventiveReminderComponent, canActivate: [AuthGuard]},
    ]
  },
  
  { path: 'sms',
    children: [
      { path: 'appointments', component: AppointmentsComponent, canActivate: [AuthGuard]},
    ]
  },

  { path: 'account',
    component: AccountLayoutComponent,
    children: [
      { path: 'login', component: LoginComponent},
      { path: 'register', component: RegisterComponent, canActivate: [AuthGuard]},
    ]
  },

  { path: 'print',
    outlet: 'print',
    component: PrintLayoutComponent,
    children: [
      { path: 'sterilization-permit', component: SterilizationPermitComponent, canActivate: [AuthGuard]},
      { path: 'rabies-vaccine', component: RabiesVaccineComponent, canActivate: [AuthGuard]},
      { path: 'dog-licence', component: DogLicenceComponent, canActivate: [AuthGuard]},
      { path: 'rabies-list-print', component: RabiesListByDatePrintComponent, canActivate: [AuthGuard]},
      { path: 'debtors-list-print', component: DebtsSheetPrintComponent, canActivate: [AuthGuard]},
      { path: 'visited-print', component: VisitedLastDaysPrintComponent, canActivate: [AuthGuard]},
      { path: 'not-visited-print', component: NotVisitedLastDaysPrintComponent, canActivate: [AuthGuard]},
      { path: 'animal-print', component: AnimalPrintComponent, canActivate: [AuthGuard]},
      { path: 'visit-print', component: VisitPrintComponent, canActivate: [AuthGuard]},
    ]
  },

  { path: '404', component: PageNotFoundComponent},
  { path: '**', component: PageNotFoundComponent}, // 404 page
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
