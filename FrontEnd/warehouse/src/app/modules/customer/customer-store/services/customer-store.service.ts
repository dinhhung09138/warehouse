import { Injectable } from '@angular/core';
import { ResponseModel } from 'src/app/core/models/response.model';
import { map } from 'rxjs/operators';
import { Observable, BehaviorSubject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ApiSetting } from 'src/app/core/api-setting';
import { ResponseStatus } from 'src/app/core/enums/response.enum';
import { CustomerStoreModel } from '../models/customer-store.model';
import { FilterModel } from 'src/app/core/models/filter.model';

@Injectable({
  providedIn: 'root'
})
export class CustomerStoreService {

  private saveSuccess = new BehaviorSubject(false);
  reloadGrid = this.saveSuccess.asObservable();

  url = {
    list: ApiSetting.apiRoot + 'customer/store/list',
    listCombobox: ApiSetting.apiRoot + 'customer/store/list-combobox',
    detail: ApiSetting.apiRoot + 'customer/store/detail',
    save: ApiSetting.apiRoot + 'customer/store/save',
    delete: ApiSetting.apiRoot + 'customer/store/delete',
  };

  constructor(private http: HttpClient) { }

  list(filter: FilterModel): Observable<ResponseModel> {
    return this.http.post<ResponseModel>(this.url.list, filter).pipe(map((data: ResponseModel) => {
      return data;
    }));
  }

  listCombobox(): Observable<ResponseModel> {
    return this.http.get<ResponseModel>(this.url.listCombobox).pipe(map((data: ResponseModel) => {
      return data;
    }));
  }

  detail(id: string): Observable<ResponseModel> {
    return this.http.get<ResponseModel>(this.url.detail + '/' + id).pipe(map((data: ResponseModel) => {
      return data;
    }));
  }

  save(model: CustomerStoreModel): Observable<ResponseModel> {
    return this.http.post<ResponseModel>(this.url.save, model).pipe(map((data: ResponseModel) => {
      if (data.responseStatus === ResponseStatus.success) {
        this.saveSuccess.next(true);
      }
      return data;
    }));
  }

  delete(model: CustomerStoreModel): Observable<ResponseModel> {
    return this.http.put<ResponseModel>(this.url.delete, model).pipe(map((data: ResponseModel) => {
      return data;
    }));
  }

}
