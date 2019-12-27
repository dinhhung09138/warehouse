import { Injectable } from '@angular/core';
import { ResponseModel } from 'src/app/core/models/response.model';
import { map } from 'rxjs/operators';
import { Observable, BehaviorSubject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ApiSetting } from 'src/app/core/api-setting';
import { ResponseStatus } from 'src/app/core/enums/response.enum';
import { GoodsModel } from '../models/goods.model';

@Injectable({
  providedIn: 'root'
})
export class GoodsService {

  private saveSuccess = new BehaviorSubject(false);
  reloadGrid = this.saveSuccess.asObservable();


  url = {
    list: ApiSetting.apiRoot + 'warehouse/goods/list',
    detail: ApiSetting.apiRoot + 'warehouse/goods/detail',
    save: ApiSetting.apiRoot + 'warehouse/goods/save',
    delete: ApiSetting.apiRoot + 'warehouse/goods/delete',
  };

  constructor(private http: HttpClient) { }

  list(filter: any): Observable<ResponseModel> {
    return this.http.post<ResponseModel>(this.url.list, filter).pipe(map((data: ResponseModel) => {
      return data;
    }));
  }

  detail(id: string): Observable<ResponseModel> {
    return this.http.get<ResponseModel>(this.url.detail + '/' + id).pipe(map((data: ResponseModel) => {
      return data;
    }));
  }

  save(model: GoodsModel, file: any): Observable<ResponseModel> {

    const formData = new FormData();
    formData.append('id', model.id);
    formData.append('code', model.code);
    formData.append('name', model.name);
    formData.append('brand', model.brand);
    formData.append('color', model.color);
    formData.append('size', model.size);
    formData.append('description', model.description);
    formData.append('unitId', model.unitId);
    formData.append('goodsCategoryId', model.goodsCategoryId);
    formData.append('fileId', model.fileId);
    formData.append('File', file);
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

  delete(model: GoodsModel): Observable<ResponseModel> {
    return this.http.put<ResponseModel>(this.url.delete, model).pipe(map((data: ResponseModel) => {
      return data;
    }));
  }

}
