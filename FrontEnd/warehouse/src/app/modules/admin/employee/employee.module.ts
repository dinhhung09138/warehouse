import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeListComponent } from './components/employee-list/employee-list.component';
import { EmployeeFormComponent } from './components/employee-form/employee-form.component';
import { Routes, RouterModule } from '@angular/router';
import { EmployeeService } from './services/employee.service';
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
import { DepartmentService } from '../department/services/department.service';


const routes: Routes = [
  { path: '', component: EmployeeListComponent, pathMatch: 'full' }
];

@NgModule({
  declarations: [
    EmployeeListComponent,
    EmployeeFormComponent,
  ],
  entryComponents: [
    EmployeeFormComponent,
  ],
  providers: [
    EmployeeService,
    DepartmentService,
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
export class EmployeeModule { }
