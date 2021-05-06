import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(
    private router: Router,
    private accountService: AccountService,
  ) { }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const token = window.localStorage.getItem('token');
    if (token) {
      let roles = next.data["roles"];
      if (roles) {
        var match = this.accountService.getRolePermission(roles);
        if (match)
          return true;
        else {
          this.router.navigate(['/comanda']);
          return false;
        }
      }
      else
        return true;
    } else {
      this.router.navigate(['login']);
      return false;
    }
  }  
}
