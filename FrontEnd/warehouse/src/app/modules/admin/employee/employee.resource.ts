import { SharedResource } from 'src/app/shared/shared.message';

export class EmployeeResource {
  static readonly list = {
    title: 'Employee list',
    table: {
      code: 'Code',
      name: 'Name',
      departmentName: 'Department',
      mobile: 'Mobile',
      email: 'Email',
      active: 'Active',
    },
    button: {

    },
    message: {
      nodata: SharedResource.table.nodata,
      deleteSuccess: SharedResource.formMessage.deleteSuccess,
    },
  };

  static readonly form = {
    titleCreate: 'Create Employee',
    titleEdit: 'Edit Employee',
    form: {
      code: 'Code',
      name: 'Name',
      mobile: 'Mobile',
      email: 'Email',
      workPhone: 'WorkPhone',
      fax: 'Fax',
      dateOfJoin: 'Date Of Join',
      dateOfLeaving: 'Date Of Leaving',
      departmentId: 'Department',
      active: 'Active',
    },
    message: {
      code: 'Code is required',
      name: 'Name is required',
      departmentId: 'Department is required',
      dateOfJoin: 'Date of join is required',
      saveSuccess: SharedResource.formMessage.saveSuccess,
    },
  };
}
