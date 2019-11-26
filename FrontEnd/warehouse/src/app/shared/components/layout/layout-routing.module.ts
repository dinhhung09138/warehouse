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
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LayoutRoutingModule { }
