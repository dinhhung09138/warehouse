import { ResponseStatus } from '../enums/response.enum';

export class ResponseModel {
  responseStatus: ResponseStatus;
  errors: any[];
  result: any;
  extra: any[];
}
