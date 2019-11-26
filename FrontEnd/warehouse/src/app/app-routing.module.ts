import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthenticationGuard } from './core/guards/authentication.guard';
import { PageNotfoundComponent } from './shared/components/page-notfound/page-notfound.component';

const routes: Routes = [
  {
    path: 'login',
    loadChildren: () => import('../app/modules/login/login.module').then(m => m.LoginModule),
    data: { title: 'Login' },
  },
  {
    path: '',
    canActivate: [AuthenticationGuard],
    loadChildren: () => import('../app/shared/components/layout/layout.module').then(m => m.LayoutModule),
    data: { title: 'Dashboard' },
  },
  {
    path: '**',
    component: PageNotfoundComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
