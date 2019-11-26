import { UserModel } from './user.model';

export class TokenModel {
  accessToken: string;
  expiration: number;
  refreshToken: string;
  userInfo: UserModel;
}
