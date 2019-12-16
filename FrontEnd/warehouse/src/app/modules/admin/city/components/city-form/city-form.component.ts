import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { CityResource } from '../../city.resource';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CityModel } from '../../models/city.model';
import { CityService } from '../../services/city.service';
import { ResponseModel } from 'src/app/core/models/response.model';
import { ResponseStatus } from 'src/app/core/enums/response.enum';
import { LoadingService } from 'src/app/core/services/loading.service';
import { ShowMessageService } from 'src/app/core/services/show-message.service';
import { CountryService } from '../../../country/services/country.service';
import { ItemSelectModel } from 'src/app/core/models/item-select.model';

@Component({
  selector: 'app-city-form',
  templateUrl: './city-form.component.html',
  styleUrls: ['./city-form.component.css']
})
export class CityFormComponent implements OnInit {

  @Input() isEdit = false;
  @Input() city: CityModel;
  submitted = false;
  isLoading = false;
  formMessage = CityResource.form;
  cityForm: FormGroup;
  warningMessage: any[];
  countryLoading = false;
  countryList: ItemSelectModel[] = [];

  constructor(public activeModal: NgbActiveModal,
              private fb: FormBuilder,
              private loading: LoadingService,
              private messageService: ShowMessageService,
              private cityService: CityService,
              private countryService: CountryService) { }

  ngOnInit() {
    if (this.isEdit === false) {
      this.city = new CityModel(this.isEdit);
    }
    this.getCountryList();
    this.initForm();
  }

  onSubmitForm() {
    this.submitted = true;
    this.warningMessage = [];

    if (this.cityForm.invalid) {
      return;
    }

    this.isLoading = true;
    this.loading.showLoading(true);

    this.cityService.save(this.cityForm.value).subscribe((response: ResponseModel) => {
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
    if (this.isEdit === false) {
      this.initForm();
      return;
    }

    this.loading.showLoading(true);
    this.cityService.detail(this.city.id).subscribe((response: ResponseModel) => {
      if (response.responseStatus === ResponseStatus.warning) {
        this.messageService.showWarning(response.errors.join(','));
        this.loading.showLoading(false);
      } else if (response.responseStatus === ResponseStatus.error) {
        this.messageService.showError(response.errors.join(','));
        this.loading.showLoading(false);
      } else {
        this.city = response.result;
        this.cityForm.patchValue({
          id: response.result.id,
          code: response.result.code,
          name: response.result.name,
          countryId: response.result.countryId,
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

  private initForm() {
    this.cityForm = this.fb.group({
      id: [this.city.id],
      code: [this.city.code, [Validators.required, Validators.maxLength(20)]],
      name: [this.city.name, [Validators.required, Validators.maxLength(200)]],
      countryId: [this.city.countryId, [Validators.required]],
      isEdit: [this.isEdit],
      isActive: [this.city.isActive],
      rowVersion: [this.city.rowVersion],
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

}
