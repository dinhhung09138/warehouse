export class FilterPagingModel {
  pageSize: number;
  pageIndex: number;

  constructor() {
    this.pageSize = 30;
    this.pageIndex = 1;
  }
}
