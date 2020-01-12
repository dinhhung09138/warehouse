import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { ApiSetting } from './../api-setting';
import { ISetting } from './../interfaces/setting.interface';

@Injectable()
export class AppLoadService {

  constructor(private http: HttpClient) {
  }

  getSettingSetting(): Promise<any> {
    return this.http.get<ISetting>('assets/config/appconfig.json').toPromise().then(response => {
      ApiSetting.apiRoot = response.apiRoot + '/api/';
      ApiSetting.hubUrl = response.apiRoot + response.hubUrl;
    });
  }

  getSetting() {
    ApiSetting.apiRoot = 'https://localhost:44301/api/';
    ApiSetting.hubUrl = 'https://localhost:44301/hubs/notification/';
  }

}
