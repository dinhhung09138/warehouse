import { SharedResource } from 'src/app/shared/shared.message';

export class CountryResource {
  static readonly list = {
    title: 'Country',
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
    titleCreate: 'Create Country',
    titleEdit: 'Edit Country',
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
