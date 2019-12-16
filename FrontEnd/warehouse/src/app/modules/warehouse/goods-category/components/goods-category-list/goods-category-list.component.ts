import { Component, OnInit } from '@angular/core';
import { GoodsCategoryResource } from '../../goods-category.resource';
import { GoodsCategoryModel } from '../../models/goods-category.model';
import { TableColumnModel } from 'src/app/core/models/table-column.model';
import { ISortEvent } from 'src/app/core/interfaces/sort-event.interface';
import { GoodsCategoryService } from '../../services/goods-category.service';
import { LoadingService } from 'src/app/core/services/loading.service';
import { ResponseModel } from 'src/app/core/models/response.model';
import { ResponseStatus } from 'src/app/core/enums/response.enum';
import { ShowMessageService } from 'src/app/core/services/show-message.service';
import { FilterModel } from 'src/app/core/models/filter.model';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { GoodsCategoryFormComponent } from '../goods-category-form/goods-category-form.component';
import { ConfirmationService } from 'src/app/core/services/confirmation.service';
import { AppLicationSetting } from 'src/app/core/config/appication-setting.config';

@Component({
  selector: 'app-goods-category-list',
  templateUrl: './goods-category-list.component.html',
  styleUrls: ['./goods-category-list.component.css']
})
export class GoodsCategoryListComponent implements OnInit {

  formMessage = GoodsCategoryResource.list;
  filter: FilterModel;
  list: GoodsCategoryModel[] = [];
  totalRow = 0;

  constructor(private categoryService: GoodsCategoryService,
              private loading: LoadingService,
              private messageService: ShowMessageService,
              private confirmMessage: ConfirmationService,
              private modal: NgbModal) { }

  ngOnInit() {
    this.filter = new FilterModel();
    this.getlist();
    this.reloadDataListener();
  }

  reloadDataListener() {
    this.categoryService.reloadGrid.subscribe(response => {
      if (response === true) {
        this.getlist();
      }
    });
  }

  onFilter(text: string) {
    this.filter.text = text;
    this.filter.paging.pageIndex = 1;
    this.getlist();
  }

  onSort(event: ISortEvent) {
    event.data.sort((data1, data2) => {
      let value1 = data1[event.field];
      let value2 = data2[event.field];
      let result = null;

      if (value1 == null && value2 != null)
          result = -1;
      else if (value1 != null && value2 == null)
          result = 1;
      else if (value1 == null && value2 == null)
          result = 0;
      else if (typeof value1 === 'string' && typeof value2 === 'string')
          result = value1.localeCompare(value2);
      else
          result = (value1 < value2) ? -1 : (value1 > value2) ? 1 : 0;

      return (event.order * result);
    });
  }

  onPageChange(page: any) {
    if (page) {
      this.filter.paging.pageIndex = page.page + 1;
      this.filter.paging.pageSize = page.rows;
      this.getlist();
    }
  }

  onAddNewClick() {
    const modalRef = this.modal.open(GoodsCategoryFormComponent, AppLicationSetting.modalOptions.modalSmallOptions);
    modalRef.componentInstance.isEdit = false;
  }

  onUpdateClick(item: GoodsCategoryModel) {
    const modalRef = this.modal.open(GoodsCategoryFormComponent, AppLicationSetting.modalOptions.modalSmallOptions);
    modalRef.componentInstance.isEdit = true;
    modalRef.componentInstance.category = item;
  }

  onDeleteClick(item: GoodsCategoryModel) {
    this.confirmMessage.open().then(response => {
      if (response) {
        this.categoryService.delete(item).subscribe((response: ResponseModel) => {
          if (response.responseStatus === ResponseStatus.warning) {
            this.messageService.showWarning(response.errors.join(','));
          } else if (response.responseStatus === ResponseStatus.error) {
            this.messageService.showError(response.errors.join(','));
          } else if (response.responseStatus === ResponseStatus.success) {
            this.messageService.showSuccess(this.formMessage.message.deleteSuccess);
            this.getlist();
          }
        });
      }
    });
  }

  private getlist() {
    this.loading.showLoading(true);
    this.categoryService.list(this.filter).subscribe((response: ResponseModel) => {
      if (response.responseStatus === ResponseStatus.warning) {
        this.messageService.showWarning(response.errors.join(','));
        this.loading.showLoading(false);
      } else if (response.responseStatus === ResponseStatus.error) {
        this.messageService.showError(response.errors.join(','));
        this.loading.showLoading(false);
      } else {
        this.list = response.result.items;
        this.totalRow = response.result.totalItems;
        this.loading.showLoading(false);

      }
    }, err => {
      console.log(err);
      this.loading.showLoading(false);
    });
  }
}
