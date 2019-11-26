import { HTTP_INTERCEPTORS } from '@angular/common/http';

import { HeaderInterceptor } from './header.interceptor';
import { HttptokenInterceptor } from './http-token.interceptor';

export const ApplicationInterceptor = [
  {
    provide: HTTP_INTERCEPTORS,
    useClass: HeaderInterceptor,
    multi: true
  },
  {
    provide: HTTP_INTERCEPTORS,
    useClass: HttptokenInterceptor,
    multi: true
  }
];
