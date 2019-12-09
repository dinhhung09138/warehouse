import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { UnitResource } from '../../unit.resource';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UnitModel } from '../../models/unit.model';
import { UnitService } from '../../services/unit.service';
import { ResponseModel } from 'src/app/core/models/response.model';
import { ResponseStatus } from 'src/app/core/enums/response.enum';
import { LoadingService } from 'src/app/core/services/loading.service';
import { ShowMessageService } from 'src/app/core/services/show-message.service';

@Component({
  selector: 'app-unit-form',
  templateUrl: './unit-form.component.html',
  styleUrls: ['./unit-form.component.css']
})
export class UnitFormComponent implements OnInit {

  @Input() isEdit = false;
  @Input() unit: UnitModel;
  submitted = false;
  isLoading = false;
  formMessage = UnitResource.form;
  unitForm: FormGroup;
  warningMessage: any[];

  constructor(public activeModal: NgbActiveModal,
              private fb: FormBuilder,
              private loading: LoadingService,
              private messageService: ShowMessageService,
              private unitService: UnitService) { }

  ngOnInit() {
    if (this.isEdit === false) {
      this.unit = new UnitModel(this.isEdit);
    }
    this.initForm();
  }

  onSubmitForm() {
    this.submitted = true;
    this.warningMessage = [];

    if (this.unitForm.invalid) {
      return;
    }

    this.isLoading = true;
    this.loading.showLoading(true);

    this.unitService.save(this.unitForm.value).subscribe((response: ResponseModel) => {
      if (response.responseStatus === ResponseStatus.warning) {
        console.log(response.errors.join(','));
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
    this.unitService.detail(this.unit.id).subscribe((response: ResponseModel) => {
      if (response.responseStatus === ResponseStatus.warning) {
        this.messageService.showWarning(response.errors.join(','));
        this.loading.showLoading(false);
      } else if (response.responseStatus === ResponseStatus.error) {
        this.messageService.showError(response.errors.join(','));
        this.loading.showLoading(false);
      } else {
        this.unit = response.result;
        this.unitForm.patchValue({
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
    this.unitForm = this.fb.group({
      id: [this.unit.id],
      code: [this.unit.code, [Validators.required, Validators.maxLength(20)]],
      name: [this.unit.name, [Validators.required, Validators.maxLength(200)]],
      isEdit: [this.isEdit],
      isActive: [this.unit.isActive],
      rowVersion: [this.unit.rowVersion],
    });
  }

}
