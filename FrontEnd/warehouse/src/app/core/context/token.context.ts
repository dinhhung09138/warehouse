import { Injectable } from '@angular/core';
import { TokenModel } from './../models/token.model';
import { UserModel } from '../models/user.model';

@Injectable()
export class TokenContext {

  constructor() {

  }

  removeAll() {
    sessionStorage.removeItem('tokenContext');
    sessionStorage.removeItem('userContext');
  }

  saveToken(token: TokenModel) {
    sessionStorage.removeItem('tokenContext');
    sessionStorage.setItem('tokenContext', JSON.stringify({
      accessToken: token.accessToken,
      expiration: token.expiration,
      refreshToken: token.refreshToken,
     }));
    this.saveUser(token.userInfo);
  }

  saveUser(userInfo: UserModel) {
    sessionStorage.removeItem('userContext');
    sessionStorage.setItem('userContext', JSON.stringify({ userInfo }));
  }

  isAuthenticated(): boolean {
    if (sessionStorage.getItem('tokenContext')) {
        return true;
    }
    return false;
  }

  getWorkcontext(): any {
      const workcontext = JSON.parse(sessionStorage.getItem('tokenContext'));
      if (workcontext != null) {
          return workcontext;
      }
      return '';
  }

  getToken(): string {
      const workcontext = JSON.parse(sessionStorage.getItem('tokenContext'));
      if (workcontext != null) {
          return workcontext.accessToken;
      }
      return '';
  }

  getCurrentUser(): UserModel {
      const user = JSON.parse(sessionStorage.getItem('userContext'));
      if (user != null) {
          return user.userInfo;
      }
      return null;
  }

  isTokenExpired(needToLogout: boolean): boolean {
    const tokenContext = JSON.parse(sessionStorage.getItem('tokenContext'));
    if (tokenContext) {
        const t = Math.round((new Date()).getTime() / 1000);
        // tslint:disable-next-line: radix
        const expiredDate = parseInt(tokenContext.expiration);
        if (needToLogout) {
            if (t >= expiredDate) {
                return true;
            }
        } else {
            if (t + 300 >= expiredDate) {
                return true;
            }
        }
        return false;
    }
    return false;
  }

}
