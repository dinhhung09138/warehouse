
export class FeeModel {
  id: string;
  name: string;
  value: number;
  isEdit: boolean;
  isActive: boolean;
  rowVersion: any;

  constructor(isEdit: boolean) {
    this.id = '';
    this.name = '';
    this.value = 0;
    this.isEdit = isEdit;
    this.isActive = true;
  }
}
