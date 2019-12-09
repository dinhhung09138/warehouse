import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UnitListComponent } from './components/unit-list/unit-list.component';
import { UnitFormComponent } from './components/unit-form/unit-form.component';
import { Routes, RouterModule } from '@angular/router';
import { UnitService } from './services/unit.service';
import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { ButtonModule } from 'primeng/button';
import { ToolbarModule } from 'primeng/toolbar';
import {CheckboxModule} from 'primeng/checkbox';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MessagesModule } from 'primeng/messages';

const routes: Routes = [
  { path: '', component: UnitListComponent, pathMatch: 'full' }
];

@NgModule({
  declarations: [
    UnitListComponent,
    UnitFormComponent,
  ],
  entryComponents: [
    UnitFormComponent,
  ],
  providers: [
    UnitService,
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
export class UnitModule { }
