
export class DepartmentModel {
  id: string;
  name: string;
  isEdit: boolean;
  isActive: boolean;
  rowVersion: any;

  constructor(isEdit: boolean) {
    this.id = '';
    this.name = '';
    this.isEdit = isEdit;
    this.isActive = true;
  }
}
