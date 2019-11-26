import { ApiSetting } from './../../../core/api-setting';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { DeviceDetectorService } from 'ngx-device-detector';

import { ResponseModel } from 'src/app/core/models/response.model';

import { LoginModel } from './../models/login.model';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  deviceInfo = null;

  url = {
    login: ApiSetting.apiRoot + 'authentication/login',
  };

  constructor(
    private http: HttpClient,
    private deviceService: DeviceDetectorService) { }

  login(model: LoginModel): Observable<ResponseModel> {
    this.deviceInfo = this.deviceService.getDeviceInfo();
    let platform = 'unknown';
    if (this.deviceService.isMobile()) {
      platform = 'Mobile';
    } else if (this.deviceService.isTablet()) {
        platform = 'Tablet';
    } else if (this.deviceService.isDesktop()) {
      platform = 'Desktop';
    }

    model.browser = this.deviceInfo.browser + '/' + this.deviceInfo.browser_version;
    model.osName = this.deviceInfo.os;
    model.platform = platform;

    return this.http.post<ResponseModel>(this.url.login, model).pipe(map((data) => {
      return data;
    }));
  }
}
