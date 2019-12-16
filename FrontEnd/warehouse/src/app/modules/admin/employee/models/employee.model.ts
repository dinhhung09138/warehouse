
export class EmployeeModel {
  id: string;
  code: string;
  name: string;
  avatarFileId: string;
  mobile: string;
  workPhone: string;
  fax: string;
  dateOfJoin: Date;
  dateOfLeaving: Date;
  email: string;
  departmentId: string;
  departmentName: string;
  isEdit: boolean;
  isActive: boolean;
  rowVersion: any;

  constructor(isEdit: boolean) {
    this.id = '';
    this.code = '';
    this.name = '';
    this.avatarFileId = '';
    this.mobile = '';
    this.workPhone = '';
    this.fax = '';
    this.dateOfJoin = new Date();
    this.dateOfLeaving = null;
    this.email = '';
    this.departmentId = '';
    this.departmentName = '';
    this.isEdit = isEdit;
    this.isActive = true;
  }
}
