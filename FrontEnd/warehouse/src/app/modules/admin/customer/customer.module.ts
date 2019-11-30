import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CustomerService } from './services/customer.service';
import { FormCustomerComponent } from './components/form-customer/form-customer.component';
import { ListCustomerComponent } from './components/list-customer/list-customer.component';

const routes: Routes = [
  {
    path: '',
    component: ListCustomerComponent,
    pathMatch: 'full',
  }
];

@NgModule({
  declarations: [
    ListCustomerComponent,
    FormCustomerComponent,
  ],
  entryComponents: [
    FormCustomerComponent,
  ],
  providers: [
    CustomerService,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
  ]
})
export class CustomerModule { }
