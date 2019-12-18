import { Component, OnInit } from '@angular/core';
import { CustomerStoreResource } from '../../customer-store.resource';
import { CustomerStoreModel } from '../../models/customer-store.model';
import { TableColumnModel } from 'src/app/core/models/table-column.model';
import { ISortEvent } from 'src/app/core/interfaces/sort-event.interface';
import { CustomerStoreService } from '../../services/customer-store.service';
import { LoadingService } from 'src/app/core/services/loading.service';
import { ResponseModel } from 'src/app/core/models/response.model';
import { ResponseStatus } from 'src/app/core/enums/response.enum';
import { ShowMessageService } from 'src/app/core/services/show-message.service';
import { FilterModel } from 'src/app/core/models/filter.model';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CustomerStoreFormComponent } from '../customer-store-form/customer-store-form.component';
import { retryWhen } from 'rxjs/operators';
import { ConfirmationService } from 'src/app/core/services/confirmation.service';
import { AppLicationSetting } from 'src/app/core/config/appication-setting.config';

@Component({
  selector: 'app-customer-store-list',
  templateUrl: './customer-store-list.component.html',
  styleUrls: ['./customer-store-list.component.css']
})
export class CustomerStoreListComponent implements OnInit {

  formMessage = CustomerStoreResource.list;
  filter: FilterModel;
  list: CustomerStoreModel[] = [];
  totalRow = 0;

  constructor(private customerStoreService: CustomerStoreService,
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
    this.customerStoreService.reloadGrid.subscribe(response => {
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
    const modalRef = this.modal.open(CustomerStoreFormComponent, AppLicationSetting.modalOptions.modalExtraOptions);
    modalRef.componentInstance.isEdit = false;
  }

  onUpdateClick(item: CustomerStoreModel) {
    const modalRef = this.modal.open(CustomerStoreFormComponent, AppLicationSetting.modalOptions.modalExtraOptions);
    modalRef.componentInstance.isEdit = true;
    modalRef.componentInstance.customerStore = item;
  }

  onDeleteClick(item: CustomerStoreModel) {
    this.confirmMessage.open().then(response => {
      if (response) {
        this.customerStoreService.delete(item).subscribe((response: ResponseModel) => {
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

  getFullAddress(address: string, cityName?: string, countryName?: string): string {
    return (address ? address : '...') + '/' +
            (cityName ? cityName : '...')  + '/' +
            (countryName ? countryName : '...');
  }

  private getlist() {
    this.loading.showLoading(true);
    this.customerStoreService.list(this.filter).subscribe((response: ResponseModel) => {
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
