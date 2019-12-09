import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GoodsCategoryListComponent } from './components/goods-category-list/goods-category-list.component';
import { GoodsCategoryFormComponent } from './components/goods-category-form/goods-category-form.component';
import { Routes, RouterModule } from '@angular/router';
import { GoodsCategoryService } from './services/goods-category.service';
import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { ButtonModule } from 'primeng/button';
import { ToolbarModule } from 'primeng/toolbar';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MessagesModule } from 'primeng/messages';

const routes: Routes = [
  { path: '', component: GoodsCategoryListComponent, pathMatch: 'full' }
];

@NgModule({
  declarations: [
    GoodsCategoryListComponent,
    GoodsCategoryFormComponent,
  ],
  entryComponents: [
    GoodsCategoryFormComponent,
  ],
  providers: [
    GoodsCategoryService,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    TableModule,
    NgbModalModule,
    PaginatorModule,
    ButtonModule,
    ToolbarModule,
    MessagesModule,
  ]
})
export class GoodsCategoryModule { }
