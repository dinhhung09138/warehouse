import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DepartmentListComponent } from './components/department-list/department-list.component';
import { DepartmentFormComponent } from './components/department-form/department-form.component';
import { Routes, RouterModule } from '@angular/router';
import { DepartmentService } from './services/department.service';
import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { ButtonModule } from 'primeng/button';
import { ToolbarModule } from 'primeng/toolbar';
import {CheckboxModule} from 'primeng/checkbox';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MessagesModule } from 'primeng/messages';

const routes: Routes = [
  { path: '', component: DepartmentListComponent, pathMatch: 'full' }
];

@NgModule({
  declarations: [
    DepartmentListComponent,
    DepartmentFormComponent,
  ],
  entryComponents: [
    DepartmentFormComponent,
  ],
  providers: [
    DepartmentService,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    TableModule,
    NgbModalModule,
    PaginatorModule,
    CheckboxModule,
    ButtonModule,
    ToolbarModule,
    MessagesModule,
  ]
})
export class DepartmentModule { }
