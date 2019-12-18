import { SharedResource } from 'src/app/shared/shared.message';

export class CustomerStoreResource {
  static readonly list = {
    title: `Customer's Store`,
    table: {
      name: 'Name',
      primaryPhone: 'Phone',
      fax: 'Fax',
      storeManagerId: 'Manager',
      address: 'Address',
      email: 'Email',
      startOn: 'Start on',
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
    titleCreate: `Create Customer's Store`,
    titleEdit: `Edit Customer's Store`,
    form: {
      name: 'Name',
      logoFileId: '',
      primaryPhone: 'Primary Phone',
      secondaryPhone: 'Secondary phone',
      fax: 'Fax',
      email: 'Email',
      storeManagerId: 'Manager',
      isCompany: 'Company',
      startOn: 'Start on',
      description: 'Description',
      address: 'Address',
      cityId: 'City',
      countryId: 'Country',
      longtitue: 'Longtitue',
      latitude: 'Latitude',
      contactName: 'Contact Name',
      contactPhone: 'Contact Phone',
      contactEmail: 'Contact Email',
      isActive: 'Active',
    },
    message: {
      name: 'Name is required',
      saveSuccess: SharedResource.formMessage.saveSuccess,
    },
  };
}
