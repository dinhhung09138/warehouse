import { SharedResource } from 'src/app/shared/shared.message';

export class DepartmentResource {
  static readonly list = {
    title: 'Department List',
    table: {
      code: 'Code',
      name: 'Name',
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
    titleCreate: 'Create Department',
    titleEdit: 'Edit Department',
    form: {
      code: 'Code',
      name: 'Name',
      active: 'Active',
    },
    message: {
      code: 'Code is required',
      name: 'Name is required',
      saveSuccess: SharedResource.formMessage.saveSuccess,
    },
  };
}
