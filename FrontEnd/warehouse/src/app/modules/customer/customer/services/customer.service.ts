import { Injectable } from '@angular/core';
import { ResponseModel } from 'src/app/core/models/response.model';
import { map } from 'rxjs/operators';
import { Observable, BehaviorSubject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ApiSetting } from 'src/app/core/api-setting';
import { ResponseStatus } from 'src/app/core/enums/response.enum';
import { CustomerModel } from '../models/customer.model';
import { FilterModel } from 'src/app/core/models/filter.model';
import { DateTimeService } from 'src/app/core/services/datetime.service';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  private saveSuccess = new BehaviorSubject(false);
  reloadGrid = this.saveSuccess.asObservable();


  url = {
    list: ApiSetting.apiRoot + 'customer/list',
    listCombobox: ApiSetting.apiRoot + 'customer/list-combobox',
    detail: ApiSetting.apiRoot + 'customer/detail',
    save: ApiSetting.apiRoot + 'customer/save',
    delete: ApiSetting.apiRoot + 'customer/delete',
  };

  constructor(private http: HttpClient, private dateService: DateTimeService) { }

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

  save(model: CustomerModel, file: any): Observable<ResponseModel> {

    const formData = new FormData();
    formData.append('file', file);
    formData.append('id', model.id);
    formData.append('name', model.name);
    formData.append('logoFileId', model.logoFileId);
    formData.append('primaryPhone', model.primaryPhone || '');
    formData.append('secondaryPhone', model.secondaryPhone || '');
    formData.append('fax', model.fax || '');
    formData.append('website', model.website || '');
    formData.append('taxCode', model.taxCode || '');
    formData.append('email', model.email || '');
    formData.append('isCompany', model.isCompany ? '1' : '0');
    formData.append('startOnString', this.dateService.convertDateToString(model.startOn));
    formData.append('description', model.description || '');
    formData.append('address', model.address || '');
    formData.append('cityId', model.cityId);
    formData.append('countryId', model.countryId);
    formData.append('longtitue', '0');
    formData.append('latitude', '0');
    formData.append('contactName', model.contactName || '');
    formData.append('contactPhone', model.contactPhone || '');
    formData.append('contactEmail', model.contactEmail || '');
    formData.append('userName', model.userName);
    formData.append('password', model.password);
    formData.append('isEdit', model.isEdit ? '1' : '0');
    formData.append('isActive', model.isActive ? '1' : '0');
    formData.append('rowVersion', model.rowVersion);

    return this.http.post<ResponseModel>(this.url.save, formData).pipe(map((data: ResponseModel) => {
      if (data.responseStatus === ResponseStatus.success) {
        this.saveSuccess.next(true);
      }
      return data;
    }));
  }

  delete(model: CustomerModel): Observable<ResponseModel> {
    return this.http.put<ResponseModel>(this.url.delete, model).pipe(map((data: ResponseModel) => {
      return data;
    }));
  }

}
