import { Component, OnInit, OnDestroy } from '@angular/core';
import { PushNotificationService } from 'src/app/core/services/push-notification.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit, OnDestroy {

  constructor(private pushNotificationService: PushNotificationService) { }

  ngOnInit() {
    console.log('init layout');
    this.pushNotificationService.init();
    this.loginSuccessListener();
  }

  ngOnDestroy() {
    this.pushNotificationService.hubConnection.off('loginSuccess');
  }

  loginSuccessListener() {
    this.pushNotificationService.hubConnection.on('loginSuccess', () => {
      console.log('Login success');
    });
  }

}
