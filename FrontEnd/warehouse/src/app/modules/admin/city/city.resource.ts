import { SharedResource } from 'src/app/shared/shared.message';

export class CityResource {
  static readonly list = {
    title: 'City',
    table: {
      countryName: 'Country',
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
    titleCreate: 'Create City',
    titleEdit: 'Edit City',
    form: {
      countryId: 'Country',
      code: 'Code',
      name: 'Name',
      active: 'Active',
    },
    message: {
      code: 'Code is required',
      name: 'Name is required',
      countryId: 'Country is required',
      saveSuccess: SharedResource.formMessage.saveSuccess,
    },
  };
}
