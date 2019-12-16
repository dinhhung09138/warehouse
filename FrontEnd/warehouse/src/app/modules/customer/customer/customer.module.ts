import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerListComponent } from './components/customer-list/customer-list.component';
import { CustomerFormComponent } from './components/customer-form/customer-form.component';
import { Routes, RouterModule } from '@angular/router';
import { CustomerService } from './services/customer.service';
import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { ButtonModule } from 'primeng/button';
import { ToolbarModule } from 'primeng/toolbar';
import { CheckboxModule } from 'primeng/checkbox';
import { NgbModalModule, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MessagesModule } from 'primeng/messages';
import { CalendarModule } from 'primeng/calendar';
import { NgSelectModule } from '@ng-select/ng-select';
import { DateTimeFormat } from 'src/app/core/pipes/datetime-format.pipe';


const routes: Routes = [
  { path: '', component: CustomerListComponent, pathMatch: 'full' }
];

@NgModule({
  declarations: [
    CustomerListComponent,
    CustomerFormComponent,
    DateTimeFormat,
  ],
  entryComponents: [
    CustomerFormComponent,
  ],
  providers: [
    CustomerService,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    NgSelectModule,
    NgbModule,
    TableModule,
    CalendarModule,
    NgbModalModule,
    PaginatorModule,
    CheckboxModule,
    ButtonModule,
    ToolbarModule,
    MessagesModule,
  ]
})
export class CustomerModule { }
