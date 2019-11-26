import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { TokenContext } from '../context/token.context';

@Injectable()
export class AuthenticationGuard implements CanActivate {

  constructor(private router: Router, private context: TokenContext) {

  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const user = this.context.getCurrentUser();
    if (user) {
      return true;
    }
    this.router.navigate(['login'], { queryParams: { returnUrl: state.url}});
    return false;
  }
}
