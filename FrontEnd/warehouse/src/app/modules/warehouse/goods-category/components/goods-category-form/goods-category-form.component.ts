import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { GoodsCategoryResource } from '../../goods-category.resource';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { GoodsCategoryModel } from '../../models/goods-category.model';
import { GoodsCategoryService } from '../../services/goods-category.service';
import { ResponseModel } from 'src/app/core/models/response.model';
import { ResponseStatus } from 'src/app/core/enums/response.enum';
import { LoadingService } from 'src/app/core/services/loading.service';
import { ShowMessageService } from 'src/app/core/services/show-message.service';

@Component({
  selector: 'app-goods-category-form',
  templateUrl: './goods-category-form.component.html',
  styleUrls: ['./goods-category-form.component.css']
})
export class GoodsCategoryFormComponent implements OnInit {

  @Input() isEdit = false;
  @Input() category: GoodsCategoryModel;
  submitted = false;
  isLoading = false;
  formMessage = GoodsCategoryResource.form;
  categoryForm: FormGroup;
  warningMessage: any[];

  constructor(public activeModal: NgbActiveModal,
              private fb: FormBuilder,
              private loading: LoadingService,
              private messageService: ShowMessageService,
              private categoryService: GoodsCategoryService) { }

  ngOnInit() {
    if (this.isEdit === false) {
      this.category = new GoodsCategoryModel(this.isEdit);
    }
    this.initForm();
  }

  onSubmitForm() {
    this.submitted = true;
    this.warningMessage = [];

    if (this.categoryForm.invalid) {
      return;
    }

    this.isLoading = true;
    this.loading.showLoading(true);

    this.categoryService.save(this.categoryForm.value).subscribe((response: ResponseModel) => {
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
    this.loading.showLoading(true);
    this.categoryService.detail(this.category.id).subscribe((response: ResponseModel) => {
      if (response.responseStatus === ResponseStatus.warning) {
        this.messageService.showWarning(response.errors.join(','));
        this.loading.showLoading(false);
      } else if (response.responseStatus === ResponseStatus.error) {
        this.messageService.showError(response.errors.join(','));
        this.loading.showLoading(false);
      } else {
        this.category = response.result;
        this.categoryForm.patchValue({
          id: response.result.id,
          name: response.result.name,
          description: response.result.description,
          isEdit: response.result.isEdit,
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
    this.categoryForm = this.fb.group({
      id: [this.category.id],
      name: [this.category.name, [Validators.required, Validators.maxLength(200)]],
      description: [this.category.description, [Validators.required, Validators.maxLength(250)]],
      isEdit: [this.isEdit],
      isActive: [this.category.isActive],
      rowVersion: [this.category.rowVersion],
    });
  }

}
