import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FeeListComponent } from './components/fee-list/fee-list.component';
import { FeeFormComponent } from './components/fee-form/fee-form.component';
import { Routes, RouterModule } from '@angular/router';
import { FeeService } from './services/fee.service';
import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { ButtonModule } from 'primeng/button';
import { ToolbarModule } from 'primeng/toolbar';
import {CheckboxModule} from 'primeng/checkbox';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MessagesModule } from 'primeng/messages';
import { InputRestrictionDirective } from 'src/app/core/directives/input-retriction.directive';
import { NumberFormat } from 'src/app/core/pipes/number-format.pipe';

const routes: Routes = [
  { path: '', component: FeeListComponent, pathMatch: 'full' }
];

@NgModule({
  declarations: [
    FeeListComponent,
    FeeFormComponent,
    InputRestrictionDirective,
    NumberFormat,
  ],
  entryComponents: [
    FeeFormComponent,
  ],
  providers: [
    FeeService,
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
export class FeeModule { }
