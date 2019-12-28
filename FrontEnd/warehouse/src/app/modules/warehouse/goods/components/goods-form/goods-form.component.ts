import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { GoodsResource } from '../../goods.resource';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { GoodsModel } from '../../models/goods.model';
import { GoodsService } from '../../services/goods.service';
import { ResponseModel } from 'src/app/core/models/response.model';
import { ResponseStatus } from 'src/app/core/enums/response.enum';
import { LoadingService } from 'src/app/core/services/loading.service';
import { ShowMessageService } from 'src/app/core/services/show-message.service';
import { ItemSelectModel } from 'src/app/core/models/item-select.model';
import { UnitService } from '../../../unit/services/unit.service';
import { ThrowStmt, ReadVarExpr } from '@angular/compiler';
import { GoodsCategoryService } from '../../../goods-category/services/goods-category.service';
import { ImageFileService } from 'src/app/core/services/image-file.service';

@Component({
  selector: 'app-goods-form',
  templateUrl: './goods-form.component.html',
  styleUrls: ['./goods-form.component.css']
})
export class GoodsFormComponent implements OnInit, OnDestroy {

  @Input() isEdit = false;
  @Input() goods: GoodsModel;
  submitted = false;
  isLoading = false;
  formMessage = GoodsResource.form;
  goodsForm: FormGroup;
  warningMessage: any[];
  unitLoading = false;
  unitList: ItemSelectModel[] = [];
  categoryLoading = false;
  categoryList: ItemSelectModel[] = [];

  fileData: File = null;
  previewUrl: any = null;

  constructor(public activeModal: NgbActiveModal,
              private fb: FormBuilder,
              private loading: LoadingService,
              private messageService: ShowMessageService,
              private goodsService: GoodsService,
              private unitService: UnitService,
              private categoryService: GoodsCategoryService,
              private imageService: ImageFileService) { }

  ngOnInit() {
    if (this.isEdit === false) {
      this.goods = new GoodsModel(this.isEdit);
    }
    this.imageService.changeImagePreview.subscribe(image => {
      setTimeout(() => {
        this.previewUrl = image;
      }, 1);
    });
    this.initForm();
    this.getUnitList();
    this.getGoodsCategoryList();
    this.onRefreshClick();
  }

  ngOnDestroy() {
    this.previewUrl = null;
    this.fileData = null;
  }

  onSubmitForm() {
    this.submitted = true;
    this.warningMessage = [];

    if (this.goodsForm.invalid) {
      return;
    }

    this.isLoading = true;
    this.loading.showLoading(true);

    this.goodsService.save(this.goodsForm.value, this.fileData).subscribe((response: ResponseModel) => {
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

    this.fileData = null;
    this.previewUrl = null;

    this.goodsService.detail(this.goods.id).subscribe((response: ResponseModel) => {
      if (response.responseStatus === ResponseStatus.warning) {
        this.messageService.showWarning(response.errors.join(','));
        this.loading.showLoading(false);
      } else if (response.responseStatus === ResponseStatus.error) {
        this.messageService.showError(response.errors.join(','));
        this.loading.showLoading(false);
      } else {
        this.goods = response.result;

        if (this.goods.fileContent) {
          this.previewUrl = this.goods.fileContent;
        } else {
          this.previewUrl = null;
        }

        this.goodsForm.patchValue({
          id: response.result.id,
          code: response.result.code,
          name: response.result.name,
          brand: response.result.brand,
          color: response.result.color,
          size: response.result.size,
          fileId: response.result.fileId,
          description: response.result.description,
          unitId: response.result.unitId.toUpperCase(),
          goodsCategoryId: response.result.goodsCategoryId.toUpperCase(),
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
    this.goodsForm = this.fb.group({
      id: [this.goods.id],
      code: [this.goods.code, [Validators.required, Validators.maxLength(20)]],
      name: [this.goods.name, [Validators.required, Validators.maxLength(200)]],
      unitId: [this.goods.unitId, [Validators.required]],
      goodsCategoryId: [this.goods.goodsCategoryId, [Validators.required]],
      brand: [this.goods.brand, [Validators.maxLength(200)]],
      color: [this.goods.color, [Validators.maxLength(200)]],
      size: [this.goods.size, [Validators.maxLength(200)]],
      fileId: [this.goods.fileId],
      description: [this.goods.description, [Validators.maxLength(500)]],
      isEdit: [this.isEdit],
      isActive: [this.goods.isActive],
      rowVersion: [this.goods.rowVersion],
    });
  }

  getUnitList() {
    this.unitLoading = true;
    this.unitService.listCombobox().subscribe((response: ResponseModel) => {
      if (response.responseStatus === ResponseStatus.warning) {
        this.messageService.showWarning(response.errors.join(','));
      } else if (response.responseStatus === ResponseStatus.error) {
        this.messageService.showError(response.errors.join(','));
      } else {
        this.unitList = response.result;
      }
      this.unitLoading = false;
    }, err => {
      console.log(err);
      this.unitLoading = false;
    });
  }

  getGoodsCategoryList() {
    this.categoryLoading = true;
    this.categoryService.listCombobox().subscribe((response: ResponseModel) => {
      if (response.responseStatus === ResponseStatus.warning) {
        this.messageService.showWarning(response.errors.join(','));
      } else if (response.responseStatus === ResponseStatus.error) {
        this.messageService.showError(response.errors.join(','));
      } else {
        this.categoryList = response.result;
      }
      this.categoryLoading = false;
    }, err => {
      console.log(err);
      this.categoryLoading = false;
    });
  }

  fileProgress(fileInput: any) {
    this.fileData = fileInput.target.files[0] as File;
    this.imageService.preview(this.fileData);
  }

}
