
export class CustomerModel {
  id: string;
  name: string;
  logoFileId: string;
  logoFileContent: string;
  primaryPhone: string;
  secondaryPhone: string;
  fax: string;
  website: string;
  taxCode: string;
  email: string;
  isCompany: boolean;
  startOn: Date;
  description: string;
  address: string;
  cityId: string;
  cityName: string;
  countryId: string;
  countryName: string;
  longtitue: number;
  latitude: number;
  contactName: string;
  contactPhone: string;
  contactEmail: string;
  createUser: boolean;
  userName: string;
  password: string;
  confirmPassword: string;
  isEdit: boolean;
  isActive: boolean;
  rowVersion: any;

  constructor(isEdit: boolean) {
    this.id = '';
    this.name = '';
    this.logoFileId = '';
    this.logoFileContent = '';
    this.primaryPhone = '';
    this.secondaryPhone = '';
    this.fax = '';
    this.website = '';
    this.taxCode = '';
    this.email = '';
    this.isCompany = true;
    this.startOn = null;
    this.description = '';
    this.address = '';
    this.cityId = '';
    this.cityName = '';
    this.countryId = '';
    this.countryName = '';
    this.longtitue = 0;
    this.latitude = 0;
    this.contactName = '';
    this.contactPhone = '';
    this.contactEmail = '';
    this.createUser = false;
    this.userName = '';
    this.password = '';
    this.confirmPassword = '';
    this.isEdit = isEdit;
    this.isActive = true;
  }
}
