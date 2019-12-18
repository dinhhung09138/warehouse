import { Injectable } from '@angular/core';
import { ValidatorFn, AbstractControl } from '@angular/forms';

@Injectable()
export class ValidationService {

  constructor() {}

  /**
   * allowCheckKey: controlName set check password or not
   * passwordKey: Password form key name
   * confirmPasswordKey: Confirm passowrd form key name
   * https://itnext.io/angular-custom-form-validation-bc513b45ccfa
   * https://github.com/orange-bees/angular-concepts-tutorials/blob/master/custom-form-validators/src/app/services/custom-validators.service.ts
   */
  passwordMatch(allowCheckKey: string, passwordKey: string, confirmPasswordKey: string): ValidatorFn {

    return (control: AbstractControl): { [key: string]: boolean } | null => {
      if (!control) { return null; }

      const allow = control.get(allowCheckKey);

      // Return
      if (!allow || allow.value === true) {
        return { passwordMismatch: true };
      }

      const password = control.get(passwordKey);
      const confirmPassword = control.get(confirmPasswordKey);

      if (!password.value || !confirmPassword.value) {
        return null;
      }

      if (password.value !== confirmPassword.value) {
        return { passwordMismatch: true };
      }

      return null;
    }
  }
}
