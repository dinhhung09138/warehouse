import { FilterPagingModel } from './filter-paging.model';
import { FilterSortModel } from './filter-sort.model';

export class FilterModel {
  text: string;
  paging: FilterPagingModel;
  sort: FilterSortModel;

  constructor() {
    this.text = '';
    this.paging = new FilterPagingModel();
    this.sort = new FilterSortModel();
  }
}
