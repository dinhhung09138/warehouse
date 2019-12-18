
export class CustomerStoreModel {
  id: string;
  name: string;
  primaryPhone: string;
  secondaryPhone: string;
  fax: string;
  email: string;
  storeManagerId: string;
  storeManagerName: string;
  startOn: Date;
  description: string;
  address: string;
  cityId: string;
  cityName: string;
  countryId: string;
  countryName: string;
  longtitue: number;
  latitude: number;
  isEdit: boolean;
  isActive: boolean;
  rowVersion: any;

  constructor(isEdit: boolean) {
    this.id = '';
    this.name = '';
    this.primaryPhone = '';
    this.secondaryPhone = '';
    this.fax = '';
    this.email = '';
    this.storeManagerId = '';
    this.storeManagerName = '';
    this.startOn = null;
    this.description = '';
    this.address = '';
    this.cityId = '';
    this.cityName = '';
    this.countryId = '';
    this.countryName = '';
    this.longtitue = 0;
    this.latitude = 0;
    this.isEdit = isEdit;
    this.isActive = true;
  }
}
