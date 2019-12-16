import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { CustomerResource } from '../../customer.resource';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CustomerModel } from '../../models/customer.model';
import { CustomerService } from '../../services/customer.service';
import { ResponseModel } from 'src/app/core/models/response.model';
import { ResponseStatus } from 'src/app/core/enums/response.enum';
import { LoadingService } from 'src/app/core/services/loading.service';
import { ShowMessageService } from 'src/app/core/services/show-message.service';
import { AppLicationSetting } from 'src/app/core/config/appication-setting.config';
import { ItemSelectModel } from 'src/app/core/models/item-select.model';
import { CountryService } from 'src/app/modules/admin/country/services/country.service';
import { CityService } from 'src/app/modules/admin/city/services/city.service';

@Component({
  selector: 'app-customer-form',
  templateUrl: './customer-form.component.html',
  styleUrls: ['./customer-form.component.css']
})
export class CustomerFormComponent implements OnInit {

  @Input() isEdit = false;
  @Input() customer: CustomerModel;
  submitted = false;
  isLoading = false;
  formMessage = CustomerResource.form;
  customerForm: FormGroup;
  warningMessage: any[];
  countryLoading = false;
  countryList: ItemSelectModel[] = [];
  cityLoading = false;
  cityList: ItemSelectModel[] = [];

  vnDate = AppLicationSetting.timeZoneSetting;

  constructor(public activeModal: NgbActiveModal,
              private fb: FormBuilder,
              private loading: LoadingService,
              private messageService: ShowMessageService,
              private customerService: CustomerService,
              private countryService: CountryService,
              private cityService: CityService) { }

  ngOnInit() {
    this.getCountryList();
    if (this.isEdit == false) {
      this.customer = new CustomerModel(this.isEdit);
    }
    this.initForm();
    this.reloadForm();
    this.getCityList();
  }

  onSubmitForm() {
    this.submitted = true;
    this.warningMessage = [];

    if (this.customerForm.invalid) {
      return;
    }

    this.isLoading = true;
    this.loading.showLoading(true);

    this.customerService.save(this.customerForm.value).subscribe((response: ResponseModel) => {
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
    this.customerForm = this.fb.group({
      id: [this.customer.id ],
      name: [this.customer.name, [Validators.required, Validators.maxLength(300)]],
      primaryPhone: [this.customer.primaryPhone, [Validators.maxLength(20)]],
      secondaryPhone: [this.customer.secondaryPhone, [Validators.maxLength(20)]],
      fax: [this.customer.fax, [Validators.maxLength(20)]],
      website: [this.customer.website, [Validators.maxLength(50)]],
      taxCode: [this.customer.taxCode, [Validators.maxLength(50)]],
      email: [this.customer.email, [Validators.maxLength(50)]],
      isCompany: [this.customer.isCompany],
      startOn: [this.customer.startOn ? new Date(this.customer.startOn) : null],
      description: [this.customer.description],
      address: [this.customer.address, [Validators.maxLength(300)]],
      cityId: [this.customer.cityId],
      countryId: [this.customer.countryId],
      longtitue: [this.customer.longtitue],
      latitude: [this.customer.latitude],
      contactName: [this.customer.contactName, [Validators.maxLength(50)]],
      contactPhone: [this.customer.contactPhone, [Validators.maxLength(50)]],
      contactEmail: [this.customer.contactEmail, [Validators.maxLength(50)]],
      isEdit: [this.isEdit],
      isActive: [this.customer.isActive],
      rowVersion: [this.customer.rowVersion],
    });
  }

  private reloadForm() {
    if (this.isEdit === false) {
      return;
    }

    this.loading.showLoading(true);
    this.customerService.detail(this.customer.id).subscribe((response: ResponseModel) => {
      if (response.responseStatus === ResponseStatus.warning) {
        this.messageService.showWarning(response.errors.join(','));
        this.loading.showLoading(false);
      } else if (response.responseStatus === ResponseStatus.error) {
        this.messageService.showError(response.errors.join(','));
        this.loading.showLoading(false);
      } else {
        this.customer = response.result;
        console.log(response.result);
        this.customerForm.patchValue({
          id: response.result.id,
          name: response.result.name,
          primaryPhone: response.result.primaryPhone,
          secondaryPhone: response.result.secondaryPhone,
          fax: response.result.fax,
          website: response.result.website,
          taxCode: response.result.taxCode,
          email: response.result.email,
          isCompany: response.result.isCompany,
          mobile: response.result.mobile,
          workPhone: response.result.workPhone,
          startOn: response.result.startOn ? new Date(response.result.startOn) : null,
          description: response.result.description,
          address: response.result.address,
          cityId: response.result.cityId.toUpperCase(),
          countryId: response.result.countryId.toUpperCase(),
          longtitue: response.result.longtitue,
          latitude: response.result.latitude,
          contactName: response.result.contactName,
          contactPhone: response.result.contactPhone,
          contactEmail: response.result.contactEmail,
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
    this.customerForm.patchValue({
      cityId: null,
    });
    this.cityList = [];
  }

  getCityList() {
    if (!this.customerForm.value.countryId) {
      return;
    }
    this.cityLoading = true;
    this.cityService.listCityByCountryId(this.customerForm.value.countryId).subscribe((response: ResponseModel) => {

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
