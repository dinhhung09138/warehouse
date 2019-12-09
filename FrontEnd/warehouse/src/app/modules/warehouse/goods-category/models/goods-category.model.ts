
export class GoodsCategoryModel {
  id: string;
  name: string;
  description: string;
  isEdit: boolean;
  isActive: boolean;
  rowVersion: any;

  constructor(isEdit: boolean) {
    this.id = '';
    this.name = '';
    this.description = '';
    this.isEdit = isEdit;
    this.isActive = true;
  }
}
