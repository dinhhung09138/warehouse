import { NgModule } from '@angular/core';
import { DateTimeFormat } from '../core/pipes/datetime-format.pipe';

@NgModule({
  declarations: [
    DateTimeFormat,
  ],
  exports: [
    DateTimeFormat,
  ]
})
export class SharedService {}
