import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { EmployeeResource } from '../../employee.resource';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { EmployeeModel } from '../../models/employee.model';
import { EmployeeService } from '../../services/employee.service';
import { ResponseModel } from 'src/app/core/models/response.model';
import { ResponseStatus } from 'src/app/core/enums/response.enum';
import { LoadingService } from 'src/app/core/services/loading.service';
import { ShowMessageService } from 'src/app/core/services/show-message.service';

@Component({
  selector: 'app-employee-form',
  templateUrl: './employee-form.component.html',
  styleUrls: ['./employee-form.component.css']
})
export class EmployeeFormComponent implements OnInit {

  @Input() isEdit = false;
  @Input() employee: EmployeeModel;
  submitted = false;
  isLoading = false;
  formMessage = EmployeeResource.form;
  employeeForm: FormGroup;
  warningMessage: any[];

  constructor(public activeModal: NgbActiveModal,
              private fb: FormBuilder,
              private loading: LoadingService,
              private messageService: ShowMessageService,
              private employeeService: EmployeeService) { }

  ngOnInit() {
    if (this.isEdit == false) {
      this.employee = new EmployeeModel(this.isEdit);
    }
    this.initForm();
    this.reloadForm();
  }

  onSubmitForm() {
    this.submitted = true;
    this.warningMessage = [];

    if (this.employeeForm.invalid) {
      return;
    }

    this.isLoading = true;
    this.loading.showLoading(true);

    this.employeeService.save(this.employeeForm.value).subscribe((response: ResponseModel) => {
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
    this.employeeForm = this.fb.group({
      id: [this.employee.id ],
      code: [this.employee.code, [Validators.required, Validators.maxLength(20)]],
      name: [this.employee.name, [Validators.required, Validators.maxLength(200)]],
      mobile: [this.employee.mobile, [Validators.maxLength(20)]],
      workPhone: [this.employee.workPhone, [Validators.maxLength(20)]],
      fax: [this.employee.fax, [Validators.maxLength(20)]],
      email: [this.employee.email, [Validators.maxLength(50)]],
      dateOfJoin: [this.employee.dateOfJoin ? new Date(this.employee.dateOfJoin) : null],
      dateOfLeaving: [this.employee.dateOfLeaving ? new Date(this.employee.dateOfLeaving) : null],
      isEdit: [this.isEdit],
      isActive: [this.employee.isActive],
      rowVersion: [this.employee.rowVersion],
    });
  }

  private reloadForm() {
    if (this.isEdit === false) {
      return;
    }

    this.loading.showLoading(true);
    this.employeeService.detail(this.employee.id).subscribe((response: ResponseModel) => {
      if (response.responseStatus === ResponseStatus.warning) {
        this.messageService.showWarning(response.errors.join(','));
        this.loading.showLoading(false);
      } else if (response.responseStatus === ResponseStatus.error) {
        this.messageService.showError(response.errors.join(','));
        this.loading.showLoading(false);
      } else {
        this.employee = response.result;
        console.log(this.employee);
        this.employeeForm.patchValue({
          id: response.result.id,
          code: response.result.code,
          name: response.result.name,
          mobile: response.result.mobile,
          workPhone: response.result.workPhone,
          fax: response.result.fax,
          email: response.result.email,
          dateOfJoin: new Date(response.result.dateOfJoin),
          dateOfLeaving: response.result.dateOfLeaving ? new Date(response.result.dateOfLeaving) : null,
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

}
