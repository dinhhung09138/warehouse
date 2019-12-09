
export class UnitModel {
  id: string;
  code: string;
  name: string;
  isEdit: boolean;
  isActive: boolean;
  rowVersion: any;

  constructor(isEdit: boolean) {
    this.id = '';
    this.code = '';
    this.name = '';
    this.isEdit = isEdit;
    this.isActive = true;
  }
}
