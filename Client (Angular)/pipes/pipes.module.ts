import {NgModule} from '@angular/core';
import {ShortStringPipe} from './short-str.pipe';
import {AbsoluteValuePipe} from './absolute-value.pipe';
import {EmptyStringPipe} from './empty-string.pipe';
import {MeasurementsPipe} from './measurements.pipe';
import {SmsSentStatusPipe} from './sms-sent-status.pipe';
import {SmsSentStatusIsDeliveredPipe} from './sms-sent-status-is-delivered.pipe';
import {SmsSentStatusIsNotDeliveredPipe} from './sms-sent-status-is-not-delivered.pipe';
import {AnimalImgUrlByTypePipe} from './animal-img-url-by-type.pipe';
import {BooleanStringToBoolPipe} from './boolean-string-to-bool.pipe';

@NgModule({
  imports: [],
  declarations: [
    ShortStringPipe,
    AbsoluteValuePipe,
    EmptyStringPipe,
    MeasurementsPipe,
    SmsSentStatusPipe,
    SmsSentStatusIsDeliveredPipe,
    SmsSentStatusIsNotDeliveredPipe,
    AnimalImgUrlByTypePipe,
    BooleanStringToBoolPipe,
  ],
  providers: [],
  exports: [
    ShortStringPipe,
    AbsoluteValuePipe,
    EmptyStringPipe,
    MeasurementsPipe,
    SmsSentStatusPipe,
    SmsSentStatusIsDeliveredPipe,
    SmsSentStatusIsNotDeliveredPipe,
    AnimalImgUrlByTypePipe,
    BooleanStringToBoolPipe,
  ]
})
export class PipesModule {}
