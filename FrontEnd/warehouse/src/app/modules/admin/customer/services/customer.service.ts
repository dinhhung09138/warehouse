import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ResponseModel } from 'src/app/core/models/response.model';
import { ApiSetting } from './../../../../core/api-setting';
import { CustomerModel } from '../models/customer.model';

@Injectable()
export class CustomerService {

  url = {
    list: ApiSetting.apiRoot + 'admin/customer/list',
    save: ApiSetting.apiRoot + 'admin/customer/save',
    delete: ApiSetting.apiRoot + 'admin/customer/delete',
  };

  constructor(private http: HttpClient) { }

  list(filter: any): Observable<ResponseModel> {
    return this.http.post<ResponseModel>(this.url.list, filter).pipe(map((data: ResponseModel) => {
      return data;
    }));
  }

  save(model: CustomerModel): Observable<ResponseModel> {
    return this.http.post<ResponseModel>(this.url.save, model).pipe(map((data: ResponseModel) => {
      return data;
    }));
  }

  delete(model: CustomerModel): Observable<ResponseModel> {
    return this.http.post<ResponseModel>(this.url.delete, model).pipe(map((data: ResponseModel) => {
      return data;
    }));
  }

}
