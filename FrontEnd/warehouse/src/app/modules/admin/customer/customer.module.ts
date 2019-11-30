import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FormCustomerComponent } from './components/form-customer/form-customer.component';
import { ListCustomerComponent } from './components/list-customer/list-customer.component';



@NgModule({
  declarations: [
    ListCustomerComponent,
    FormCustomerComponent,
  ],
  entryComponents: [
    FormCustomerComponent,
  ],
  providers: [

  ],
  imports: [
    CommonModule
  ]
})
export class CustomerModule { }
