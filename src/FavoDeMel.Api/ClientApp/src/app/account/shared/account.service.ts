import { environment } from './../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import jwt_decode from "jwt-decode";
import { Role } from './role';
import { delay } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http: HttpClient) { }

  async login(user: any) {
    const result = await this.http.post<any>(`${environment.api}/Account/login`, user).toPromise();
    if (result && result.success) {
      window.localStorage.setItem('token', result.token);
      return true;
    }

    return false;
  }

  async createAccount(account: any) {    
    const result = await this.http.post<any>(`${environment.api}/Account/register`, account).toPromise();
    if (result && result.success) {
      window.localStorage.setItem('token', result.token);
      return true;
    }

    return false;
  }

  getRoles() {
    const result = this.http.get<Role[]>(`${environment.api}/Account/roles`);
    return result;
  }

  logout() {
    window.localStorage.removeItem('token');
    window.localStorage.removeItem('perfil');
  }

  getAuthorizationToken() {
    const token = window.localStorage.getItem('token');
    return token;
  }

  getTokenExpirationDate(token: string): Date {
    const decoded: any = jwt_decode(token);

    if (decoded.exp === undefined) {
      return null;
    }

    const date = new Date(0);
    date.setUTCSeconds(decoded.exp);
    return date;
  }

  isTokenExpired(token?: string): boolean {
    if (!token) {
      return true;
    }

    const date = this.getTokenExpirationDate(token);
    if (date === undefined) {
      return false;
    }

    return !(date.valueOf() > new Date().valueOf());
  }

  getRolePermission(role: string): Boolean {
    const token = this.getAuthorizationToken();
    const decoded: any = jwt_decode(token);

    if (decoded.role === role) {
      return true;
    }

    return false;
  }

  getRole(): string {
    const token = this.getAuthorizationToken();
    const decoded: any = jwt_decode(token);

    if (decoded.exp === undefined) {
      return null;
    }

    return decoded.role;
  }

  isUserLoggedIn() {
    const token = this.getAuthorizationToken();
    if (!token) {
      return false;
    } else if (this.isTokenExpired(token)) {
      return false;
    }

    return true;
  }
}
