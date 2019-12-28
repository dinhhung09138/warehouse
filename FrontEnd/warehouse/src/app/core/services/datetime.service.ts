import { DatePipe } from '@angular/common';
import { AppLicationSetting } from '../config/appication-setting.config';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DateTimeService {

  constructor(private datePipe: DatePipe) {}

  convertDateToString(date?: Date): string {
    if (date) {
      return this.datePipe.transform(date, AppLicationSetting.dateFormat.day);
    }
    return '';
  }
}
