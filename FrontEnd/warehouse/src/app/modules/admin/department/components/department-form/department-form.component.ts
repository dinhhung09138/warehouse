import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { DepartmentResource } from '../../department.resource';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { DepartmentModel } from '../../models/department.model';
import { DepartmentService } from '../../services/department.service';
import { ResponseModel } from 'src/app/core/models/response.model';
import { ResponseStatus } from 'src/app/core/enums/response.enum';
import { LoadingService } from 'src/app/core/services/loading.service';
import { ShowMessageService } from 'src/app/core/services/show-message.service';

@Component({
  selector: 'app-department-form',
  templateUrl: './department-form.component.html',
  styleUrls: ['./department-form.component.css']
})
export class DepartmentFormComponent implements OnInit {

  @Input() isEdit = false;
  @Input() department: DepartmentModel;
  submitted = false;
  isLoading = false;
  formMessage = DepartmentResource.form;
  departmentForm: FormGroup;
  warningMessage: any[];

  constructor(public activeModal: NgbActiveModal,
              private fb: FormBuilder,
              private loading: LoadingService,
              private messageService: ShowMessageService,
              private departmentService: DepartmentService) { }

  ngOnInit() {
    if (this.isEdit === false) {
      this.department = new DepartmentModel(this.isEdit);
    }
    this.initForm();
  }

  onSubmitForm() {
    this.submitted = true;
    this.warningMessage = [];

    if (this.departmentForm.invalid) {
      return;
    }

    this.isLoading = true;
    this.loading.showLoading(true);

    this.departmentService.save(this.departmentForm.value).subscribe((response: ResponseModel) => {
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
    this.departmentService.detail(this.department.id).subscribe((response: ResponseModel) => {
      if (response.responseStatus === ResponseStatus.warning) {
        this.messageService.showWarning(response.errors.join(','));
        this.loading.showLoading(false);
      } else if (response.responseStatus === ResponseStatus.error) {
        this.messageService.showError(response.errors.join(','));
        this.loading.showLoading(false);
      } else {
        this.department = response.result;
        this.departmentForm.patchValue({
          id: response.result.id,
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
    this.departmentForm = this.fb.group({
      id: [this.department.id],
      name: [this.department.name, [Validators.required, Validators.maxLength(200)]],
      isEdit: [this.isEdit],
      isActive: [this.department.isActive],
      rowVersion: [this.department.rowVersion],
    });
  }

}
