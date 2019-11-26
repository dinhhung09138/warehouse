import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutRoutingModule } from './layout-routing.module';
import { LayoutComponent } from './layout.component';
import { NavigationComponent } from './navigation/navigation.component';
import { AccountComponent } from './navigation/account/account.component';
import { NotificationComponent } from './navigation/notification/notification.component';
import { EmailComponent } from './navigation/email/email.component';
import { TaskComponent } from './navigation/task/task.component';
import { LeftSidebarComponent } from './left-sidebar/left-sidebar.component';
import { FooterComponent } from './footer/footer.component';
import { ContentComponent } from './content/content.component';
import { PushNotificationService } from 'src/app/core/services/push-notification.service';

import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { ShowMessageService } from 'src/app/core/services/show-message.service';
import { LoadingComponent } from './loading/loading.component';
import { LoadingService } from 'src/app/core/services/loading.service';


@NgModule({
  declarations: [
    LayoutComponent,
    NavigationComponent,
    AccountComponent,
    NotificationComponent,
    EmailComponent,
    TaskComponent,
    LeftSidebarComponent,
    FooterComponent,
    ContentComponent,
    LoadingComponent,
  ],
  providers: [
    PushNotificationService,
    MessageService,
    ShowMessageService,
    LoadingService,
  ],
  imports: [
    CommonModule,
    LayoutRoutingModule,
    ToastModule,
  ]
})
export class LayoutModule { }
