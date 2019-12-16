import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CityListComponent } from './components/city-list/city-list.component';
import { CityFormComponent } from './components/city-form/city-form.component';
import { Routes, RouterModule } from '@angular/router';
import { CityService } from './services/city.service';
import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { ButtonModule } from 'primeng/button';
import { ToolbarModule } from 'primeng/toolbar';
import {CheckboxModule} from 'primeng/checkbox';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MessagesModule } from 'primeng/messages';
import { NgSelectModule } from '@ng-select/ng-select';

const routes: Routes = [
  { path: '', component: CityListComponent, pathMatch: 'full' }
];

@NgModule({
  declarations: [
    CityListComponent,
    CityFormComponent,
  ],
  entryComponents: [
    CityFormComponent,
  ],
  providers: [
    CityService,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    TableModule,
    NgSelectModule,
    NgbModalModule,
    PaginatorModule,
    CheckboxModule,
    ButtonModule,
    ToolbarModule,
    MessagesModule,
  ]
})
export class CityModule { }
