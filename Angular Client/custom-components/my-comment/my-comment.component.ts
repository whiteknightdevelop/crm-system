import {Component, Input, forwardRef, OnInit} from '@angular/core';
import {ControlValueAccessor, NG_VALUE_ACCESSOR} from '@angular/forms';

@Component({
  selector: 'app-my-comment',
  templateUrl: './my-comment.component.html',
  styleUrls: ['./my-comment.component.css'],
  providers: [
    {
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => MyCommentComponent),
    multi: true
    }
  ]
})
export class MyCommentComponent implements ControlValueAccessor, OnInit {

  constructor() {
    this.commentValue = '';
    this.labelText = '';
    this.btnOnLabel = '';
    this.btnOffLabel = '';
    this.inCreateNewMode = false;
    this.maxlength = 0;
    this.showComment = false;
    this.commentNotEmptyIconStr = 'pi pi-star';
    this.commentNotEmptyIcon = '';
  }

  @Input() commentValue = '';
  @Input() labelText: string;
  @Input() btnOnLabel: string;
  @Input() btnOffLabel: string;
  @Input() inCreateNewMode: boolean;
  @Input() maxlength: number;
  showComment: boolean;
  commentNotEmptyIconStr: string;
  commentNotEmptyIcon: string;
  propagateChange = (_: any) => {};
  toggleShowComment($event: any): void {
    this.showComment = !this.showComment;
    if (this.commentValue.length > 0){
      this.commentNotEmptyIcon = this.commentNotEmptyIconStr;
    }else {
      this.commentNotEmptyIcon = '';
    }
  }

  registerOnChange(fn: any): void {
    this.propagateChange = fn;
  }

  registerOnTouched(fn: any): void {}

  writeValue(obj: any): void {
    if (obj !== undefined && obj) {
      this.commentValue = obj;
      if (this.commentValue.length > 0){
        this.commentNotEmptyIcon = this.commentNotEmptyIconStr;
      }
    }
  }

  onChangetext($event: Event): void {
    const str = ($event.target as HTMLTextAreaElement).value;
    this.commentValue = str;
    this.propagateChange(this.commentValue);
  }

  ngOnInit(): void {
    if (this.inCreateNewMode){
      this.showComment = this.inCreateNewMode;
    }
  }
}
