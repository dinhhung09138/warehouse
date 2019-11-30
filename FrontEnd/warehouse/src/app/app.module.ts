import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { NgbModule, NgbModalModule } from '@ng-bootstrap/ng-bootstrap';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { AppLoadModule } from './core/app-load.module';
import { TokenContext } from './core/context/token.context';
import { AuthenticationGuard } from './core/guards/authentication.guard';
import { ApplicationInterceptor } from './core/interceptors/interceptor';
import { ConfirmDialogComponent } from './shared/components/confirm-dialog/confirm-dialog.component';
import { PageNotfoundComponent } from './shared/components/page-notfound/page-notfound.component';

@NgModule({
  declarations: [
    AppComponent,
    ConfirmDialogComponent,
    PageNotfoundComponent,
  ],
  entryComponents: [
    ConfirmDialogComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    NgbModule,
    NgbModalModule,
    AppLoadModule,
  ],
  providers: [
    TokenContext,
    ApplicationInterceptor,
    AuthenticationGuard,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
