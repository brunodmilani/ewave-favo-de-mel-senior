import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ToastrModule } from 'ngx-toastr';
import { CreateAccountComponent } from './account/create-account/create-account.component';
import { LoginComponent } from './account/login/login.component';
import { ConfirmEqualValidatorDirective } from './account/shared/confirm-equal-validator.directive';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ComandaFormComponent } from './comanda/comanda-form/comanda-form.component';
import { ComandaListComponent } from './comanda/comanda-list/comanda-list.component';
import { httpInterceptorProviders } from './http-interceptors';
import { AuthenticationComponent } from './layout/authentication/authentication.component';
import { HomeComponent } from './layout/home/home.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { PedidoFormComponent } from './pedido/pedido-form/pedido-form.component';
import { PedidoListComponent } from './pedido/pedido-list/pedido-list.component';




@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LoginComponent,
    CreateAccountComponent,
    AuthenticationComponent,
    HomeComponent,
    PedidoListComponent,
    PedidoFormComponent,
    ComandaListComponent,
    ComandaFormComponent,
    ConfirmEqualValidatorDirective
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    BrowserAnimationsModule, // required animations module
    ModalModule.forRoot(),
    ToastrModule.forRoot({
      timeOut: 5000,
      positionClass: 'toast-top-left',
      preventDuplicates: true,
      autoDismiss: true,
      closeButton: true,
      progressBar: true
    }),
  ],
  providers: [httpInterceptorProviders],
  bootstrap: [AppComponent]
})
export class AppModule { }
