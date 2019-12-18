import { SharedResource } from 'src/app/shared/shared.message';

export class FeeResource {
  static readonly list = {
    title: 'Fee',
    table: {
      name: 'Name',
      value: 'Value',
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
    titleCreate: 'Create Fee',
    titleEdit: 'Edit Fee',
    form: {
      name: 'Name',
      value: 'Value',
      active: 'Active',
    },
    message: {
      value: 'Fee invalid',
      name: 'Name is required',
      saveSuccess: SharedResource.formMessage.saveSuccess,
    },
  };
}
