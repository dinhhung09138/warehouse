
export class GoodsModel {
  id: string;
  code: string;
  name: string;
  brand: string;
  color: string;
  size: string;
  fileId: string;
  fileContent: string;
  fileName: string;
  description: string;
  unitId: string;
  unitName: string;
  goodsCategoryId: string;
  goodsCategoryName: string;
  isEdit: boolean;
  isActive: boolean;
  rowVersion: any;

  constructor(isEdit: boolean) {
    this.id = '';
    this.code = '';
    this.name = '';
    this.brand = '';
    this.color = '';
    this.size = '';
    this.fileId = '';
    this.fileContent = '';
    this.fileName = '';
    this.description = '';
    this.unitId = '';
    this.unitName = '';
    this.goodsCategoryId = '';
    this.goodsCategoryName = '';
    this.isEdit = isEdit;
    this.isActive = true;
  }
}
