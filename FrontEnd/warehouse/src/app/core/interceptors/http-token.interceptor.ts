import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpHeaders
} from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { TokenContext } from './../context/token.context';

@Injectable()
export class HttptokenInterceptor implements HttpInterceptor {

  constructor(
    private context: TokenContext) {
}

  setHeaders(request, token) {
    let headers = new HttpHeaders();
    headers = headers.append('Authorization', token);

    return request.clone({ headers });
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    if (req.url.includes('/authentication') ||
        req.url.includes('/appconfig.json') ||
        req.url.includes('/file')) {
        return next.handle(req);
    }

    const token = this.context.getToken();
    const authReq = this.setHeaders(req, this.jwtToken(token));

    return next.handle(authReq).pipe(catchError(err => {
        if ([401, 403].indexOf(err.status) !== -1) {
            // this.authenticationService.resetLocal(); TODO
        } else {
            const error = err.error.message || err.statusText;
            return throwError(error);
        }
    }));
  }

  jwtToken(token): string {
      return 'bearer ' + token;
  }
}
