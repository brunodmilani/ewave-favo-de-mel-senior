import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../shared/account.service';
import { Role } from '../shared/role';

@Component({
  selector: 'app-create-account',
  templateUrl: './create-account.component.html',
  styleUrls: ['./create-account.component.css']
})
export class CreateAccountComponent implements OnInit {
  account  = {
    userName: '',
    email: '',
    password: '',
    passwordConfirm: '',
    role: ''
  }
  roles: Role[] = [];  

  constructor(
    private accountService: AccountService,
    private toastr: ToastrService,
    private router: Router
  ) { }

  ngOnInit() {
    this.accountService.getRoles().subscribe(roles => { 
      this.roles = roles;
    });    
  }

  async onSubmit() {
    try {
      const result = await this.accountService.createAccount(this.account);
      
      // navego para a rota vazia novamente
      if (result)
        this.router.navigate(['']);
      else
        this.toastr.warning('Erro no login! Verifique suas credenciais!');
    } catch (error) {
      this.toastr.error(error + '!');
    }
  }
}
