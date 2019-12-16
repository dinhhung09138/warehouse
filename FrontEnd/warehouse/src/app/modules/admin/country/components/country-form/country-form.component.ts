import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { CountryResource } from '../../country.resource';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CountryModel } from '../../models/country.model';
import { CountryService } from '../../services/country.service';
import { ResponseModel } from 'src/app/core/models/response.model';
import { ResponseStatus } from 'src/app/core/enums/response.enum';
import { LoadingService } from 'src/app/core/services/loading.service';
import { ShowMessageService } from 'src/app/core/services/show-message.service';

@Component({
  selector: 'app-country-form',
  templateUrl: './country-form.component.html',
  styleUrls: ['./country-form.component.css']
})
export class CountryFormComponent implements OnInit {

  @Input() isEdit = false;
  @Input() country: CountryModel;
  submitted = false;
  isLoading = false;
  formMessage = CountryResource.form;
  countryForm: FormGroup;
  warningMessage: any[];

  constructor(public activeModal: NgbActiveModal,
              private fb: FormBuilder,
              private loading: LoadingService,
              private messageService: ShowMessageService,
              private countryService: CountryService) { }

  ngOnInit() {
    if (this.isEdit === false) {
      this.country = new CountryModel(this.isEdit);
    }
    this.initForm();
  }

  onSubmitForm() {
    this.submitted = true;
    this.warningMessage = [];

    if (this.countryForm.invalid) {
      return;
    }

    this.isLoading = true;
    this.loading.showLoading(true);

    this.countryService.save(this.countryForm.value).subscribe((response: ResponseModel) => {
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
    this.countryService.detail(this.country.id).subscribe((response: ResponseModel) => {
      if (response.responseStatus === ResponseStatus.warning) {
        this.messageService.showWarning(response.errors.join(','));
        this.loading.showLoading(false);
      } else if (response.responseStatus === ResponseStatus.error) {
        this.messageService.showError(response.errors.join(','));
        this.loading.showLoading(false);
      } else {
        this.country = response.result;
        this.countryForm.patchValue({
          id: response.result.id,
          code: response.result.code,
          name: response.result.name,
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
    this.countryForm = this.fb.group({
      id: [this.country.id],
      code: [this.country.code, [Validators.required, Validators.maxLength(20)]],
      name: [this.country.name, [Validators.required, Validators.maxLength(200)]],
      isEdit: [this.isEdit],
      isActive: [this.country.isActive],
      rowVersion: [this.country.rowVersion],
    });
  }

}
