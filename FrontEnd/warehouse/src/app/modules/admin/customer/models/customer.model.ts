import { Guid } from 'guid-typescript';

export class CustomerModel {
  id: Guid;
  name: string;
  logoFileId: Guid;
  primaryPhone: string;
  secondaryPhone: string;
  fax: string;
  website: string;
  taxCode: string;
  isCompany: boolean;
  startOn: Date;
  description: string;
  address: string;
  citiId: Guid;
  countryId: Guid;
  Longtitue: number;
  Latitude: number;
  isActive: boolean;
  rowVersion: any;
  IsEdit: string;
}
