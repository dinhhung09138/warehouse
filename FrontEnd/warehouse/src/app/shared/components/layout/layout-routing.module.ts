import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './layout.component';


const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    pathMatch: 'full',
  },
  {
    path: 'demo/datatable',
    component: LayoutComponent,
    loadChildren: () => import('../demo/datatable/datatable.module').then(m => m.DataTableModule),
  },
  {
    path: 'demo/common-action',
    component: LayoutComponent,
    loadChildren: () => import('../demo/common-action/common-action.module').then(m => m.CommonActionModule),
  },
  {
    path: 'warehouse/goods-unit',
    component: LayoutComponent,
    loadChildren: () => import('../../../modules/warehouse/unit/unit.module').then(m => m.UnitModule),
  },
  {
    path: 'warehouse/goods-category',
    component: LayoutComponent,
    loadChildren: () => import('../../../modules/warehouse/goods-category/goods-category.module').then(m => m.GoodsCategoryModule),
  },
  {
    path: 'warehouse/goods',
    component: LayoutComponent,
    loadChildren: () => import('../../../modules/warehouse/goods/goods.module').then(m => m.GoodsModule),
  },
  {
    path: 'admin/employee',
    component: LayoutComponent,
    loadChildren: () => import('../../../modules/admin/employee/employee.module').then(m => m.EmployeeModule),
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LayoutRoutingModule { }
