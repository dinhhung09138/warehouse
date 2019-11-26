import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';

import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { ButtonModule } from 'primeng/button';
import { ToolbarModule } from 'primeng/toolbar';
import {SplitButtonModule} from 'primeng/splitbutton';
import { DataTableComponent } from './components/datatable.component';

const routes: Routes = [
  {
    path: '',
    component: DataTableComponent,
    pathMatch: 'full',
  },
];

@NgModule({
  declarations: [
    DataTableComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    TableModule,
    PaginatorModule,
    ButtonModule,
    ToolbarModule,
    SplitButtonModule,
  ]
})
export class DataTableModule { }
