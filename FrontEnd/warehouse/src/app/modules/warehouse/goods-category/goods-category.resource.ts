import { SharedResource } from 'src/app/shared/shared.message';

export class GoodsCategoryResource {
  static readonly list = {
    title: 'Goods Category',
    table: {
      name: 'Name',
      description: 'Description',
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
    title: 'Goods Category',
    form: {
      name: 'Name',
      description: 'Description',
      active: 'Active',
    },
    message: {
      code: 'Code is required',
      name: 'Name is required',
      saveSuccess: SharedResource.formMessage.saveSuccess,
    },
  };
}
