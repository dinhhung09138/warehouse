import { Pipe, PipeTransform } from '@angular/core';
import { DecimalPipe } from '@angular/common';
import { AppLicationSetting } from '../config/appication-setting.config';

@Pipe({ name: 'numberFormat'})
export class NumberFormat extends DecimalPipe implements PipeTransform {
  transform(value: number, type: string): string {
    if (value) {
      switch (type) {
        case AppLicationSetting.numberType.decimal:
          return super.transform(value, AppLicationSetting.numberFormat.decimal);
        case AppLicationSetting.numberType.money:
          return super.transform(value, AppLicationSetting.numberFormat.money);
        default:
          return super.transform(value, AppLicationSetting.numberFormat.decimal);
      }
    }
    return '';
  }
}
