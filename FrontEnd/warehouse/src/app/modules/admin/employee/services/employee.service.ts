import { Injectable } from '@angular/core';
import { ResponseModel } from 'src/app/core/models/response.model';
import { map } from 'rxjs/operators';
import { Observable, BehaviorSubject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ApiSetting } from 'src/app/core/api-setting';
import { ResponseStatus } from 'src/app/core/enums/response.enum';
import { EmployeeModel } from '../models/employee.model';
import { FilterModel } from 'src/app/core/models/filter.model';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  private saveSuccess = new BehaviorSubject(false);
  reloadGrid = this.saveSuccess.asObservable();


  url = {
    list: ApiSetting.apiRoot + 'admin/employee/list',
    listCombobox: ApiSetting.apiRoot + 'admin/employee/list-combobox',
    detail: ApiSetting.apiRoot + 'admin/employee/detail',
    save: ApiSetting.apiRoot + 'admin/employee/save',
    delete: ApiSetting.apiRoot + 'admin/employee/delete',
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

  save(model: EmployeeModel): Observable<ResponseModel> {
    return this.http.post<ResponseModel>(this.url.save, model).pipe(map((data: ResponseModel) => {
      if (data.responseStatus === ResponseStatus.success) {
        this.saveSuccess.next(true);
      }
      return data;
    }));
  }

  delete(model: EmployeeModel): Observable<ResponseModel> {
    return this.http.put<ResponseModel>(this.url.delete, model).pipe(map((data: ResponseModel) => {
      return data;
    }));
  }

}
