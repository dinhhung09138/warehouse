import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Car } from '../models/Car';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataTableService {

  constructor(private http: HttpClient) { }

  getCarsSmall(): Observable<Car[]> {
    return this.http.get<any>('https://www.primefaces.org/primeng/assets/showcase/data/cars-small.json')
    .pipe((data) => {
      return data;
    });
  }

  getCarsMedium(): Observable<Car[]> {
    return this.http.get<any>('https://www.primefaces.org/primeng/assets/showcase/data/cars-medium.json')
    .pipe((data) => {
      return data;
    });
  }

  getCarsLarge(): Observable<Car[]> {
  return this.http.get<any>('https://www.primefaces.org/primeng/assets/showcase/data/cars-large.json')
    .pipe((data) => {
      return data;
    });
  }

  getCarsHuge(): Observable<Car[]> {
    return this.http.get<any>('https://www.primefaces.org/primeng/assets/showcase/data/cars-huge.json')
    .pipe((data) => {
      return data;
    });
  }
}
