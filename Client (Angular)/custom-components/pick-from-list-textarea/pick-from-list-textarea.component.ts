import {Component, Input, forwardRef, EventEmitter} from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR} from '@angular/forms';
import {DialogService, DynamicDialogRef} from 'primeng/dynamicdialog';
import {ListType} from '../../models/list';

@Component({
  selector: 'app-pick-from-list-textarea',
  templateUrl: './pick-from-list-textarea.component.html',
  styleUrls: ['./pick-from-list-textarea.component.css'],
  providers: [DialogService,
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => PickFromListTextareaComponent),
      multi: true
    }]
})
export class PickFromListTextareaComponent implements ControlValueAccessor {

  constructor(public dialogService: DialogService) {
    this.list = [];
    this.labelText = '';
    this.title = '';
    this.maxlength = 0;
    this.ref = new DynamicDialogRef();
    this.showProgressBar = new EventEmitter<boolean>();
  }

  @Input() list: any[];
  @Input() labelText: string;
  @Input() title: string;
  @Input() componentType: any;
  @Input() maxlength: number;
  @Input()
  get inputValue(): string { return this.pInputValue; }
  set inputValue(val: string) { this.pInputValue = val; }
  private pInputValue = '';
  ref: DynamicDialogRef;
  showProgressBar: EventEmitter<boolean>;

  showDialog(): void {
    this.ref = this.dialogService.open(this.componentType, {
      header: this.title,
      width: '20%',
      contentStyle: {'max-height': '500px', overflow: 'auto'},
      baseZIndex: 10000,
      dismissableMask: true,
      data: this.list
    });

    this.ref.onClose.subscribe((obj: ListType) => {
      if (obj) {
        this.inputValue = this.fieldValueBuilder(this.inputValue, obj.name);
        this.propagateChange(this.inputValue);
      }
    });
  }

  private fieldValueBuilder(oldValue: string, newValue: string): string{
    if (oldValue === ''){
      return newValue;
    }
    if (oldValue.slice(-1) === ','){
      return oldValue + ' ' + newValue;
    }
    return oldValue + ', ' + newValue;
  }

  propagateChange = (_: any) => {};

  registerOnChange(fn: any): void {
    this.propagateChange = fn;
  }

  registerOnTouched(fn: any): void {}

  writeValue(obj: any): void {
    if (obj !== undefined) {
      this.inputValue = obj;
    }
  }

  onChangetext($event: Event): void {
    this.propagateChange(($event.target as HTMLTextAreaElement).value);
  }
}
