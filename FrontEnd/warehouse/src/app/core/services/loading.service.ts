import { Injectable } from "@angular/core";
import { BehaviorSubject } from 'rxjs';

@Injectable()
export class LoadingService {

  private loading = new BehaviorSubject(false);

  loadingStatus = this.loading.asObservable();

  constructor() {

  }

  showLoading(loading: boolean) {
    this.loading.next(loading);
  }

}
