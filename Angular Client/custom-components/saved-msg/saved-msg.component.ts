import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-saved-msg',
  templateUrl: './saved-msg.component.html',
  styleUrls: ['./saved-msg.component.css'],
})
export class SavedMsgComponent {

  @Input() visibilityStatus: boolean;

  constructor() {
    this.visibilityStatus = false;
  }
}
