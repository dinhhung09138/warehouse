import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CommonActionComponent } from './components/common-action.component';
import { RouterModule, Routes } from '@angular/router';
import { ConfirmationService } from 'src/app/core/services/confirmation.service';
import { ConfirmDialogComponent } from '../../confirm-dialog/confirm-dialog.component';

const routes: Routes = [
  {
    path: '',
    component: CommonActionComponent,
    pathMatch: 'full',
  }
];

@NgModule({
  declarations: [
    CommonActionComponent,
  ],
  providers: [
    ConfirmationService,
  ],
  entryComponents: [
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
  ]
})
export class CommonActionModule { }
