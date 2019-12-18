import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FeeResource } from '../../fee.resource';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { FeeModel } from '../../models/fee.model';
import { FeeService } from '../../services/fee.service';
import { ResponseModel } from 'src/app/core/models/response.model';
import { ResponseStatus } from 'src/app/core/enums/response.enum';
import { LoadingService } from 'src/app/core/services/loading.service';
import { ShowMessageService } from 'src/app/core/services/show-message.service';

@Component({
  selector: 'app-fee-form',
  templateUrl: './fee-form.component.html',
  styleUrls: ['./fee-form.component.css']
})
export class FeeFormComponent implements OnInit {

  @Input() isEdit = false;
  @Input() fee: FeeModel;
  submitted = false;
  isLoading = false;
  formMessage = FeeResource.form;
  feeForm: FormGroup;
  warningMessage: any[];

  constructor(public activeModal: NgbActiveModal,
              private fb: FormBuilder,
              private loading: LoadingService,
              private messageService: ShowMessageService,
              private feeService: FeeService) { }

  ngOnInit() {
    if (this.isEdit === false) {
      this.fee = new FeeModel(this.isEdit);
    }
    this.initForm();
  }

  onSubmitForm() {
    this.submitted = true;
    this.warningMessage = [];

    if (this.feeForm.invalid) {
      return;
    }

    this.isLoading = true;
    this.loading.showLoading(true);

    this.feeService.save(this.feeForm.value).subscribe((response: ResponseModel) => {
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
    this.feeService.detail(this.fee.id).subscribe((response: ResponseModel) => {
      if (response.responseStatus === ResponseStatus.warning) {
        this.messageService.showWarning(response.errors.join(','));
        this.loading.showLoading(false);
      } else if (response.responseStatus === ResponseStatus.error) {
        this.messageService.showError(response.errors.join(','));
        this.loading.showLoading(false);
      } else {
        this.fee = response.result;
        this.feeForm.patchValue({
          id: response.result.id,
          value: response.result.value,
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
    this.feeForm = this.fb.group({
      id: [this.fee.id],
      name: [this.fee.name, [Validators.required, Validators.maxLength(200)]],
      value: [this.fee.value, [Validators.required]],
      isEdit: [this.isEdit],
      isActive: [this.fee.isActive],
      rowVersion: [this.fee.rowVersion],
    });
  }

}
