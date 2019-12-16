import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common';
import { AppLicationSetting } from '../config/appication-setting.config';

@Pipe({ name: 'dateTimeFormat'})
export class DateTimeFormat extends DatePipe implements PipeTransform {
  transform(date: Date, type: string): string {
    if (date) {
      switch (type) {
        case AppLicationSetting.dateType.day:
          return super.transform(date, AppLicationSetting.dateFormat.day);
        case AppLicationSetting.dateType.fullTime:
          return super.transform(date, AppLicationSetting.dateFormat.fullTime);
        default:
          return super.transform(date, AppLicationSetting.dateFormat.day);
      }
    }
    return '';
  }
}
