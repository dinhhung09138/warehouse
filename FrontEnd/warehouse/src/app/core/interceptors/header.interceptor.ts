import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpRequest,
  HttpHandler,
  HttpInterceptor,
  HttpHeaders
} from '@angular/common/http';

import { Observable } from 'rxjs';

@Injectable()
export class HeaderInterceptor implements HttpInterceptor {

  setHeaders(request) {
    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    headers = headers.append('Cache-Control', 'no-cache');
    headers = headers.append('Pragma', 'no-cache');
    return request.clone({ headers });
  }


  intercept( req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (req.url.includes('/authentication') ||
        req.url.includes('/appconfig.json') ||
        req.url.includes('/file')) {
      return next.handle(req);
    }
    const modified = this.setHeaders(req);
    console.log('set header');
    console.log(modified);
    return next.handle(modified);
  }
}
