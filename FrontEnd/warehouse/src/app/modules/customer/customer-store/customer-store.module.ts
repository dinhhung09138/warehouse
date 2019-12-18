import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerStoreListComponent } from './components/customer-store-list/customer-store-list.component';
import { CustomerStoreFormComponent } from './components/customer-store-form/customer-store-form.component';
import { Routes, RouterModule } from '@angular/router';
import { CustomerStoreService } from './services/customer-store.service';
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
  { path: '', component: CustomerStoreListComponent, pathMatch: 'full' }
];

@NgModule({
  declarations: [
    CustomerStoreListComponent,
    CustomerStoreFormComponent,
    DateTimeFormat,
  ],
  entryComponents: [
    CustomerStoreFormComponent,
  ],
  providers: [
    CustomerStoreService,
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
export class CustomerStoreModule { }
