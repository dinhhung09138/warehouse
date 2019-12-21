import { Injectable } from '@angular/core';
import { ValidatorFn, AbstractControl } from '@angular/forms';

@Injectable()
export class ValidationService {

  constructor() {}

  startBeforeEnd(startKey: string, endKey: string, errorKey: string): ValidatorFn {
    return (control: AbstractControl): { [key: string]: boolean } | null => {
      if (!control) { return null; }

      const start = control.get(startKey);
      const end = control.get(endKey);
      if (!start.value || !end.value) {
        return null;
      }

      const startDate = new Date(start.value);
      const endDate = new Date(end.value);

      if (!this.isValidDate(startDate) || !this.isValidDate(endDate)) {
        return null;
      } else if (startDate > endDate) {
        const obj = {};
        obj[errorKey] = true;
        return obj;
      }
      return null;
    };
  }

  /**
   * allowCheckKey: controlName set check password or not
   * passwordKey: Password form key name
   * confirmPasswordKey: Confirm passowrd form key name
   * allowCompare: true: compare two fields. false: neither.
   * https://itnext.io/angular-custom-form-validation-bc513b45ccfa
   * https://github.com/orange-bees/angular-concepts-tutorials/blob/master/custom-form-validators/src/app/services/custom-validators.service.ts
   */
  passwordMatch(passwordKey: string, confirmPasswordKey: string, allowCompare: boolean): ValidatorFn {

    return (control: AbstractControl): { [key: string]: boolean } | null => {
      if (!control) { return null; }

      if (allowCompare === false) {
        return null;
      }
      // const allow = control.get(allowCheckKey);

      // // Return
      // if (!allow || allow.value === true) {
      //   return { passwordMismatch: true };
      // }


      const password = control.get(passwordKey);
      const confirmPassword = control.get(confirmPasswordKey);

      console.log(password);
      console.log(confirmPasswordKey);
      console.log('');

      if (!password.value || !confirmPassword.value) {
        return null;
      }

      if (password.value !== confirmPassword.value) {
        return { passwordMismatch: true };
      }

      return null;
    }
  }

  private isValidDate(date: any): boolean {
    return date instanceof Date && !isNaN(Number(date));
  }

}
