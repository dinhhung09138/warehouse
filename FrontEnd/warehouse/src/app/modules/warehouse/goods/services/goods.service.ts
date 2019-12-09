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
    formData.append('file', file, file.name);
    model.rowVersion  = file;

    return this.http.post<ResponseModel>(this.url.save, model).pipe(map((data: ResponseModel) => {
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
