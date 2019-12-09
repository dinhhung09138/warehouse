import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GoodsListComponent } from './components/goods-list/goods-list.component';
import { GoodsFormComponent } from './components/goods-form/goods-form.component';
import { Routes, RouterModule } from '@angular/router';
import { GoodsService } from './services/goods.service';
import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { ButtonModule } from 'primeng/button';
import { ToolbarModule } from 'primeng/toolbar';
import {CheckboxModule} from 'primeng/checkbox';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MessagesModule } from 'primeng/messages';
import { NgSelectModule } from '@ng-select/ng-select';
import { NgOptionHighlightModule } from '@ng-select/ng-option-highlight';
import { UnitService } from '../unit/services/unit.service';

const routes: Routes = [
  { path: '', component: GoodsListComponent, pathMatch: 'full' }
];

@NgModule({
  declarations: [
    GoodsListComponent,
    GoodsFormComponent,
  ],
  entryComponents: [
    GoodsFormComponent,
  ],
  providers: [
    GoodsService,
    UnitService,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    NgSelectModule,
    NgOptionHighlightModule,
    TableModule,
    NgbModalModule,
    PaginatorModule,
    CheckboxModule,
    ButtonModule,
    ToolbarModule,
    MessagesModule,
  ]
})
export class GoodsModule { }
