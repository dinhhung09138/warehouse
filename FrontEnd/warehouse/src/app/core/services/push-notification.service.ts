import { ApiSetting } from './../api-setting';
import { Injectable } from '@angular/core';

import { HubConnection, HubConnectionBuilder, LogLevel, HttpTransportType } from '@aspnet/signalr';

import { TokenContext } from './../context/token.context';
import { Router } from '@angular/router';

@Injectable()
export class PushNotificationService {

  public isConnected: boolean;
  private reconnectCount = 10;
  public hubConnection: HubConnection = null;

  constructor(private context: TokenContext, private router: Router) {}

  public init() {
    if (!this.hubConnection) {
      const url = ApiSetting.hubUrl + '?token=' + this.context.getToken();
      this.hubConnection = new HubConnectionBuilder().withUrl(url, {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets,
      }).configureLogging(LogLevel.Error).build();
      this.onClosed();
      this.start();
    }
  }

  private start() {
    if (!this.isConnected) {
        this.hubConnection.start().then(() => {
            this.isConnected = true;
            console.log('Connection start');
        }).catch(err => {
          console.log(err);
        });
    }
  }

  private onClosed() {
    this.hubConnection.onclose(() => {
      this.isConnected = false;
      if (this.context.isAuthenticated()) {
        for (let i = 1; i <= this.reconnectCount; i++) {
          setTimeout(() => {
              if (!this.isConnected) {
                  this.reconnect(i);
              }
          }, 10000 * i);
        }
      }
    });
  }

  private reconnect(i: number) {
    this.hubConnection.start().then(() => {
        this.isConnected = true;
        console.log('Reconnect');
    }).catch(err => {
        if (i === this.reconnectCount) {
            this.logout();
        }
        console.log(err);
    });
  }

  public logout() {
    this.context.removeAll();
    this.disconnect();
    this.router.navigate(['/login']).then(() => {
        this.hubConnection = null;
    });
  }

  private disconnect() {
    this.isConnected = false;
    if (this.hubConnection) {
      this.hubConnection.stop();
      console.log('Disconnect');
    }
  }
}
