import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule } from '@angular/forms';
import { AccountModule } from 'src/app/modules/account/account.module';
import { MaterialModule } from './material.module';
import { AdminModule } from 'src/app/modules/admin/admin.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LogErrorHandler } from './infrastructure/log-error-handler';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { HttpErrorInterceptor } from './infrastructure/http-error.interceptor';
import { UserModule } from 'src/app/modules/user/user.module';


@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    AccountModule,
    MaterialModule,
    AdminModule,
    UserModule,
    AppRoutingModule,
  ],
  // providers: [
  //   { provide: ErrorHandler, useClass: LogErrorHandler },
  //   {
  //     provide: HTTP_INTERCEPTORS,
  //     useClass: HttpErrorInterceptor,
  //     multi: true,
  //   }
  // ],
  bootstrap: [AppComponent]
})
export class AppModule { }
