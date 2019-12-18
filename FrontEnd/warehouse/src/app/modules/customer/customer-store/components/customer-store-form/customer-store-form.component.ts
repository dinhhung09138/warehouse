import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { CustomerStoreResource } from '../../customer-store.resource';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CustomerStoreModel } from '../../models/customer-store.model';
import { CustomerStoreService } from '../../services/customer-store.service';
import { ResponseModel } from 'src/app/core/models/response.model';
import { ResponseStatus } from 'src/app/core/enums/response.enum';
import { LoadingService } from 'src/app/core/services/loading.service';
import { ShowMessageService } from 'src/app/core/services/show-message.service';
import { AppLicationSetting } from 'src/app/core/config/appication-setting.config';
import { ItemSelectModel } from 'src/app/core/models/item-select.model';
import { CountryService } from 'src/app/modules/admin/country/services/country.service';
import { CityService } from 'src/app/modules/admin/city/services/city.service';

@Component({
  selector: 'app-customer-store-form',
  templateUrl: './customer-store-form.component.html',
  styleUrls: ['./customer-store-form.component.css']
})
export class CustomerStoreFormComponent implements OnInit {

  @Input() isEdit = false;
  @Input() customerStore: CustomerStoreModel;
  submitted = false;
  isLoading = false;
  formMessage = CustomerStoreResource.form;
  customerStoreForm: FormGroup;
  warningMessage: any[];
  countryLoading = false;
  countryList: ItemSelectModel[] = [];
  cityLoading = false;
  cityList: ItemSelectModel[] = [];
  employeeLoading = false;
  employeeList: ItemSelectModel[] = [];

  vnDate = AppLicationSetting.timeZoneSetting;

  constructor(public activeModal: NgbActiveModal,
              private fb: FormBuilder,
              private loading: LoadingService,
              private messageService: ShowMessageService,
              private customerStoreService: CustomerStoreService,
              private countryService: CountryService,
              private cityService: CityService) { }

  ngOnInit() {
    this.getCountryList();
    if (this.isEdit == false) {
      this.customerStore = new CustomerStoreModel(this.isEdit);
    }
    this.initForm();
    this.reloadForm();
    this.getCityList();
  }

  onSubmitForm() {
    this.submitted = true;
    this.warningMessage = [];

    if (this.customerStoreForm.invalid) {
      return;
    }

    this.isLoading = true;
    this.loading.showLoading(true);

    this.customerStoreService.save(this.customerStoreForm.value).subscribe((response: ResponseModel) => {
      if (response.responseStatus === ResponseStatus.warning) {
        this.warningMessage.push({severity: 'warn', summary: 'Warning', detail: response.errors.join(',')});
        this.loading.showLoading(false);
      } else if (response.responseStatus === ResponseStatus.error) {
        this.loading.showLoading(false);
      } else if (response.responseStatus === ResponseStatus.success) {
        this.messageService.showSuccess(this.formMessage.message.saveSuccess);
        this.activeModal.close(true);
        this.loading.showLoading(false);
      }
      this.isLoading = false;
    }, err => {
      console.log(err);
      this.isLoading = false;
      this.loading.showLoading(false);
    });
  }

  onFormCloseClick() {
    this.activeModal.close(false);
  }

  onRefreshClick() {
    this.reloadForm();
  }

  private initForm() {
    this.customerStoreForm = this.fb.group({
      id: [this.customerStore.id ],
      name: [this.customerStore.name, [Validators.required, Validators.maxLength(300)]],
      primaryPhone: [this.customerStore.primaryPhone, [Validators.maxLength(20)]],
      secondaryPhone: [this.customerStore.secondaryPhone, [Validators.maxLength(20)]],
      fax: [this.customerStore.fax, [Validators.maxLength(20)]],
      email: [this.customerStore.email, [Validators.maxLength(50)]],
      storeManagerId: [this.customerStore.storeManagerId],
      startOn: [this.customerStore.startOn ? new Date(this.customerStore.startOn) : null],
      description: [this.customerStore.description],
      address: [this.customerStore.address, [Validators.maxLength(300)]],
      cityId: [this.customerStore.cityId],
      countryId: [this.customerStore.countryId],
      longtitue: [this.customerStore.longtitue],
      latitude: [this.customerStore.latitude],
      isEdit: [this.isEdit],
      isActive: [this.customerStore.isActive],
      rowVersion: [this.customerStore.rowVersion],
    });
  }

  private reloadForm() {
    if (this.isEdit === false) {
      return;
    }

    this.loading.showLoading(true);
    this.customerStoreService.detail(this.customerStore.id).subscribe((response: ResponseModel) => {
      if (response.responseStatus === ResponseStatus.warning) {
        this.messageService.showWarning(response.errors.join(','));
        this.loading.showLoading(false);
      } else if (response.responseStatus === ResponseStatus.error) {
        this.messageService.showError(response.errors.join(','));
        this.loading.showLoading(false);
      } else {
        this.customerStore = response.result;
        this.customerStoreForm.patchValue({
          id: response.result.id,
          name: response.result.name,
          primaryPhone: response.result.primaryPhone,
          secondaryPhone: response.result.secondaryPhone,
          fax: response.result.fax,
          email: response.result.email,
          storeManagerId: response.result.storeManagerId,
          startOn: response.result.startOn ? new Date(response.result.startOn) : null,
          description: response.result.description,
          address: response.result.address,
          cityId: response.result.cityId.toUpperCase(),
          countryId: response.result.countryId.toUpperCase(),
          longtitue: response.result.longtitue,
          latitude: response.result.latitude,
          isEdit: this.isEdit,
          isActive: response.result.isActive,
          rowVersion: response.result.rowVersion,
        });

        this.loading.showLoading(false);
      }
    }, err => {
      console.log(err);
      this.loading.showLoading(false);
    });
  }

  getCountryList() {
    this.countryLoading = true;
    this.countryService.listCombobox().subscribe((response: ResponseModel) => {

      if (response.responseStatus === ResponseStatus.warning) {
        this.messageService.showWarning(response.errors.join(','));
      } else if (response.responseStatus === ResponseStatus.error) {
        this.messageService.showError(response.errors.join(','));
      } else {
        this.countryList = response.result;
      }
      this.countryLoading = false;
    }, err => {
      console.log(err);
      this.countryLoading = false;
    });
  }

  countryChange() {
    this.customerStoreForm.patchValue({
      cityId: null,
    });
    this.cityList = [];
  }

  getCityList() {
    if (!this.customerStoreForm.value.countryId) {
      return;
    }
    this.cityLoading = true;
    this.cityService.listCityByCountryId(this.customerStoreForm.value.countryId).subscribe((response: ResponseModel) => {

      if (response.responseStatus === ResponseStatus.warning) {
        this.messageService.showWarning(response.errors.join(','));
      } else if (response.responseStatus === ResponseStatus.error) {
        this.messageService.showError(response.errors.join(','));
      } else {
        this.cityList = response.result;
      }
      this.cityLoading = false;
    }, err => {
      console.log(err);
      this.cityLoading = false;
    });
  }
}
