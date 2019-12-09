import { SharedResource } from 'src/app/shared/shared.message';

export class UnitResource {
  static readonly list = {
    title: 'Units',
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
    title: 'Unit',
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
