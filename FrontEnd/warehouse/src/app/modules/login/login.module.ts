import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { DeviceDetectorModule } from 'ngx-device-detector';

import { LoginComponent } from './components/login.component';
import { LoginService } from './services/login.service';
import { MessagesModule } from 'primeng/messages';
import { MessageService } from 'primeng/api';

const routes: Routes = [
  { path: '', component: LoginComponent, pathMatch: 'full' }
];

@NgModule({
  declarations: [
    LoginComponent
  ],
  providers: [
    LoginService,
    MessageService,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MessagesModule,
    RouterModule.forChild(routes),
    DeviceDetectorModule.forRoot(),
  ]
})
export class LoginModule { }
