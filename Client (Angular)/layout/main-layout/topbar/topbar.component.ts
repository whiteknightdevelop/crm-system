import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import { trigger, style, transition, animate, AnimationEvent } from '@angular/animations';
import {AuthService} from '../../../auth/auth.service';
import {HelperService} from '../../../helper.service';
import {SignalrService} from '../../../services/signalr.service';
import {PrimeNGConfig} from 'primeng/api';
import {Router} from '@angular/router';

@Component({
  selector: 'app-topbar',
  templateUrl: './topbar.component.html',
  styleUrls: ['./topbar.component.css'],
  animations: [
    trigger('overlayMenuAnimation', [
      transition(':enter', [
        style({opacity: 0, transform: 'scaleY(0.8)'}),
        animate('.12s cubic-bezier(0, 0, 0.2, 1)', style({ opacity: 1, transform: '*' })),
      ]),
      transition(':leave', [
        animate('.1s linear', style({ opacity: 0 }))
      ])
    ])
  ]
})
export class TopbarComponent implements OnInit {

  constructor(public authService: AuthService,
              private primengConfig: PrimeNGConfig,
              private helperService: HelperService,
              public signalrService: SignalrService,
              private router: Router) {
    this.activeMenuIndex = null;
    this.topbarMenu = new ElementRef<any>(null);
    this.displayBackupDialog = false;
    this.displayRestoreDialog = false;
    this.uploadUrl = './api/system/restore' + '?connectionId=' + this.signalrService.connectionId;
    this.showUploadingProgressBar = false;
    this.showUploadButton = true;
    this.restoreCompleted = false;
    this.progressMessage = '';
  }

  @ViewChild('topbarMenu') topbarMenu: ElementRef;
  outsideClickListener: any;
  activeMenuIndex: number | null;
  displayBackupDialog: boolean;
  displayRestoreDialog: boolean;
  uploadUrl: string;
  showUploadingProgressBar: boolean;
  showUploadButton: boolean;
  restoreCompleted: boolean;
  progressMessage: string;

  ngOnInit(): void {
    this.primengConfig.ripple = true;
    this.signalrService.progressMessageString$.subscribe(
      data => {
        this.progressMessage = data;
      });
  }

  bindOutsideClickListener(): void {
    if (!this.outsideClickListener) {
      this.outsideClickListener = (event: any) => {
        if (this.isOutsideTopbarMenuClicked(event)) {
          this.activeMenuIndex =  null;
        }
      };
      document.addEventListener('click', this.outsideClickListener);
    }
  }

  unbindOutsideClickListener(): void {
    if (this.outsideClickListener) {
      document.removeEventListener('click', this.outsideClickListener);
      this.outsideClickListener = null;
    }
  }

  isOutsideTopbarMenuClicked(event: any): boolean {
    return !(this.topbarMenu.nativeElement.isSameNode(event.target) || this.topbarMenu.nativeElement.contains(event.target));
  }

  toggleMenu(event: Event, index: number): void {
    this.activeMenuIndex = this.activeMenuIndex === index ? null : index;
    event.preventDefault();
  }

  onOverlayMenuEnter(event: AnimationEvent): void {
    switch (event.toState) {
      case 'visible':
        this.bindOutsideClickListener();
        break;

      case 'void':
        this.unbindOutsideClickListener();
        break;
    }
  }

  logout(): void {
    this.authService.logout();
  }

  backup(): void {
    this.displayBackupDialog = true;
    this.signalrService.startConnection().then( () => {
      this.signalrService.addReportBackupProgress().then( () => {
        this.signalrService.getBackupFile().subscribe((blob) => {
          const today = new Date(Date.now());
          const link = document.createElement('a');
          link.download = 'backup-' + today.toLocaleString() + '.bin';
          link.href = window.URL.createObjectURL(blob);
          link.click();
          this.displayBackupDialog = false;
          this.signalrService.closeConnection();
        });
      });
    });
  }

  restore(): void {
    this.displayRestoreDialog = true;
  }

  uploadHandler($event: any): void {
    this.showUploadButton = false;
    this.showUploadingProgressBar = true;
    this.signalrService.startConnection().then( () => {
      this.signalrService.addReportRestoreProgress().then( () => {
        this.signalrService.UploadRestoreFile($event).subscribe((ans) => {
          this.showUploadingProgressBar = false;
          this.restoreCompleted = true;
        });
      });
    });
  }

  closeRestoreDialog(): void {
    this.signalrService.closeConnection();
    this.progressMessage = '';
    this.showUploadButton = true;
    this.restoreCompleted = false;
    this.displayRestoreDialog = false;
  }
}
