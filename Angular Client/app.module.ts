import {NgModule, LOCALE_ID, APP_INITIALIZER} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import '@angular/common/locales/global/he';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

// App
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainLayoutComponent } from './layout/main-layout/main-layout.component';
import { AccountLayoutComponent } from './layout/account-layout/account-layout.component';
import { PrintLayoutComponent } from './layout/print-layout/print-layout.component';
import { TopbarComponent } from './layout/main-layout/topbar/topbar.component';
import { MyCommentComponent } from './custom-components/my-comment/my-comment.component';
import { SavedMsgComponent } from './custom-components/saved-msg/saved-msg.component';
import {HttpErrorHandler} from './error-handlers/http-error-handler.service';
import {ErrorMessageService} from './error-handlers/error-message.service';
import {AnimalListComponent} from './pages/animal/animal-list/animal-list.component';
import {EmptyListErrorModule} from './custom-components/empty-list-error/empty-list-error.module';
import {PreventiveReminderListComponent} from './pages/animal/preventive-reminder/preventive-reminder-list/preventive-reminder-list.component';
import {PreventiveTreatmentModule} from './pages/visit/treatment/preventive-treatment/preventive-treatment.module';
import {VisitListComponent} from './pages/visit/visit-list/visit-list.component';
import {PickFromListTextareaComponent} from './custom-components/pick-from-list-textarea/pick-from-list-textarea.component';
import {AuthService} from './auth/auth.service';
import {AuthGuard} from './auth/auth.guard';

// Pages
import { PageNotFoundComponent } from './pages/page-not-found/page-not-found.component';
import { OwnerComponent } from './pages/owner/owner.component';
import {AnimalComponent} from './pages/animal/animal.component';
import { SearchOwnerComponent } from './pages/search/search-owner/search-owner.component';
import { HomeComponent } from './pages/home/home.component';
import {SearchOwnerService} from './pages/search/search-owner/search-owner.service';
import { SearchAnimalComponent } from './pages/search/search-animal/search-animal.component';
import {SearchAnimalService} from './pages/search/search-animal/search-animal.service';
import { PrescriptionComponent } from './pages/prescription/prescription.component';
import { DrugListItemComponent } from './pages/prescription/drug-list-item/drug-list-item.component';
import {EditModeDirective} from './pages/prescription/edit-mode.directive';
import {DebtComponent} from './pages/debt/debt.component';
import {VisitComponent} from './pages/visit/visit.component';
import {DiagnosisListComponent} from './pages/visit/diagnosis/diagnosis-list/diagnosis-list.component';
import {TreatmentListComponent} from './pages/visit/treatment/treatment-list/treatment-list.component';
import { DebtListItemComponent } from './pages/debt/debt-list-item/debt-list-item.component';
import { FollowUpComponent } from './pages/follow-up/follow-up.component';

// PrimeNG
import { RippleModule } from 'primeng/ripple';
import { ProgressBarModule } from 'primeng/progressbar';
import { MessagesModule } from 'primeng/messages';
import { MessageModule } from 'primeng/message';
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { AccordionModule } from 'primeng/accordion';
import {ConfirmationService, MessageService} from 'primeng/api';
import { CalendarModule } from 'primeng/calendar';
import { TableModule } from 'primeng/table';
import { MenubarModule } from 'primeng/menubar';
import { DropdownModule } from 'primeng/dropdown';
import { CheckboxModule } from 'primeng/checkbox';
import { DialogModule } from 'primeng/dialog';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { InputNumberModule } from 'primeng/inputnumber';
import { DividerModule } from 'primeng/divider';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { AvatarModule } from 'primeng/avatar';
import { AvatarGroupModule } from 'primeng/avatargroup';
import { ChipModule } from 'primeng/chip';
import { OverlayPanelModule } from 'primeng/overlaypanel';
import { FileUploadModule } from 'primeng/fileupload';
import {AuthModule} from './auth/auth.module';
import {PrintModule} from './pages/print/print.module';
import {PipesModule} from './pipes/pipes.module';
import { FollowupAllComponent } from './pages/followup-all/followup-all.component';
import {CardModule} from 'primeng/card';
import {FollowUpService} from './pages/follow-up/follow-up.service';
import {BadgeModule} from 'primeng/badge';
import {RegisterService} from './auth/register/register.service';
import {appInitializer} from './_helpers/app.initializer';
import {UserSessionStoreService} from './_helpers/user-session-store.service';
import {SessionRecoveryInterceptor} from './_helpers/session-recovery.interceptor';
import {Angulartics2Module} from 'angulartics2';
import {SmsModule} from './pages/sms/sms.module';
import {TooltipModule} from 'primeng/tooltip';

@NgModule({
  declarations: [
    AppComponent,
    MainLayoutComponent,
    AccountLayoutComponent,
    PrintLayoutComponent,
    TopbarComponent,
    PageNotFoundComponent,
    OwnerComponent,
    MyCommentComponent,
    SavedMsgComponent,
    AnimalComponent,
    AnimalListComponent,
    PreventiveReminderListComponent,
    VisitComponent,
    VisitListComponent,
    DiagnosisListComponent,
    TreatmentListComponent,
    PickFromListTextareaComponent,
    SearchOwnerComponent,
    HomeComponent,
    SearchAnimalComponent,
    PrescriptionComponent,
    DrugListItemComponent,
    EditModeDirective,
    DebtComponent,
    DebtListItemComponent,
    FollowUpComponent,
    FollowupAllComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule,
    ProgressBarModule,
    MessagesModule,
    MessageModule,
    ToastModule,
    ConfirmDialogModule,
    ToggleButtonModule,
    AccordionModule,
    CalendarModule,
    BrowserAnimationsModule,
    EmptyListErrorModule,
    TableModule,
    RippleModule,
    MenubarModule,
    DropdownModule,
    CheckboxModule,
    DialogModule,
    FormsModule,
    InputTextareaModule,
    InputNumberModule,
    PreventiveTreatmentModule,
    DividerModule,
    ProgressSpinnerModule,
    AvatarModule,
    AvatarGroupModule,
    ChipModule,
    OverlayPanelModule,
    FileUploadModule,
    AuthModule,
    PrintModule,
    PipesModule,
    CardModule,
    BadgeModule,
    Angulartics2Module.forRoot(),
    SmsModule,
    TooltipModule,
  ],
  providers: [
    HttpErrorHandler,
    ErrorMessageService,
    MessageService,
    ConfirmationService,
    SearchOwnerService,
    SearchAnimalService,
    FollowUpService,
    AuthService,
    RegisterService,
    AuthGuard,
    UserSessionStoreService,
    { provide: LOCALE_ID, useValue: 'he' },
    { provide: APP_INITIALIZER, useFactory: appInitializer, multi: true, deps: [AuthService] },
    { provide: HTTP_INTERCEPTORS, useClass: SessionRecoveryInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
