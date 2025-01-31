import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { HttpInterceptorService } from './helpers/interceptor';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxUiLoaderModule, NgxUiLoaderRouterModule, NgxUiLoaderHttpModule, NgxUiLoaderConfig, POSITION, SPINNER } from 'ngx-ui-loader';
import { AppConfig } from './config/config';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './layout/app.component';
import { PrivateLayoutComponent } from './layout/private-layout/private-layout.component';
import { PublicLayoutComponent } from './layout/public-layout/public-layout.component';
import { LoginComponent } from './components/login/login.component';
import { HeaderComponent } from './components/header/header.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { SidenavComponent } from './components/sidenav/sidenav.component';
import { FormsModule } from '@angular/forms';
import { MultiSelectDropdownComponent } from './components/common/multi-select-dropdown/multi-select-dropdown.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { CrudComponent } from './components/crud/crud.component';
import { CrudTsComponent } from './components/crud/crud-ts/crud-ts.component';
import { PagingComponent } from './components/paging/paging.component';
import { UserManagementComponent } from './user-management/user-management.component';

const ngxUiLoaderConfig: NgxUiLoaderConfig = {
  bgsPosition: POSITION.centerCenter,
  bgsColor: '#2AA476',
  fgsColor: '#2AA476',
  bgsOpacity: 0.85,
  bgsSize: 70,
  fgsSize: 70,
  fgsType: SPINNER.chasingDots,
  bgsType: SPINNER.rectangleBounce
}

@NgModule({
  declarations: [
    AppComponent,
    PrivateLayoutComponent,
    PublicLayoutComponent,
    LoginComponent,
    HeaderComponent,
    DashboardComponent,
    SidenavComponent,
    MultiSelectDropdownComponent,
    ForgotPasswordComponent,
    UserManagementComponent,
    CrudComponent,
    CrudTsComponent,
    PagingComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    NgxUiLoaderModule.forRoot(ngxUiLoaderConfig),
    NgxUiLoaderRouterModule.forRoot({ showForeground: false }),
    NgxUiLoaderHttpModule.forRoot({ showForeground: false }),
  ],
  providers: [
    AppConfig,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpInterceptorService,
      multi: true
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
