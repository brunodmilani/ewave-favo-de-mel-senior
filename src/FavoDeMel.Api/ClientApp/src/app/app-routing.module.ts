import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateAccountComponent } from './account/create-account/create-account.component';
import { LoginComponent } from './account/login/login.component';
import { AuthGuard } from './account/shared/auth.guard';
import { ComandaFormComponent } from './comanda/comanda-form/comanda-form.component';
import { ComandaListComponent } from './comanda/comanda-list/comanda-list.component';
import { AuthenticationComponent } from './layout/authentication/authentication.component';
import { HomeComponent } from './layout/home/home.component';
import { PedidoFormComponent } from './pedido/pedido-form/pedido-form.component';
import { PedidoListComponent } from './pedido/pedido-list/pedido-list.component';


const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    children: [
      { path: '', redirectTo: 'comanda', pathMatch: 'full' },
      { path: 'comanda', component: ComandaListComponent },
      { path: 'comanda/:comanda/pedido', component: PedidoListComponent }
    ],
    canActivate: [AuthGuard]
  },
  {
    path: '',
    component: HomeComponent,
    children: [
      { path: '', redirectTo: 'comanda', pathMatch: 'full' },
      { path: 'comanda/create', component: ComandaFormComponent },
      { path: 'comanda/edit/:id', component: ComandaFormComponent },
      { path: 'pedido/create/:comanda', component: PedidoFormComponent },      
    ],
    data: { roles: 'Garcom' },
    canActivate: [AuthGuard]
  },
  {
    path: '',
    component: AuthenticationComponent,
    children: [
      { path: '', redirectTo: 'login', pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'create-account', component: CreateAccountComponent },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
