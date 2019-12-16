
export class CityModel {
  id: string;
  code: string;
  name: string;
  countryId: string;
  isEdit: boolean;
  isActive: boolean;
  rowVersion: any;

  constructor(isEdit: boolean) {
    this.id = '';
    this.code = '';
    this.name = '';
    this.countryId = '';
    this.isEdit = isEdit;
    this.isActive = true;
  }
}
