import { SharedResource } from 'src/app/shared/shared.message';

export class CustomerResource {
  static readonly list = {
    title: 'Customer',
    table: {
      name: 'Name',
      companyContact: 'Company contact',
      primaryPhone: 'Phone',
      fax: 'Fax',
      website: 'Website',
      address: 'Address',
      email: 'Email',
      isCompany: 'Company',
      contact: 'Contact Info',
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
    titleCreate: 'Create Customer',
    titleEdit: 'Edit Customer',
    form: {
      name: 'Name',
      logoFileId: '',
      primaryPhone: 'Primary Phone',
      secondaryPhone: 'Secondary phone',
      fax: 'Fax',
      website: 'Website',
      taxCode: 'Tax Code',
      email: 'Email',
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
      userName: 'User Name',
      password: 'Password',
      confirmPassword: 'Confirm Password',
      isActive: 'Active',
    },
    message: {
      name: 'Name is required',
      userName: 'User Name is required',
      password: 'Password is required',
      confirmPassword: `Password doesn't match`,
      saveSuccess: SharedResource.formMessage.saveSuccess,
    },
  };
}
