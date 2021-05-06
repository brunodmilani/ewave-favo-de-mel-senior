import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../shared/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  login = {
    email: '',
    password: ''
  };

  constructor(
    private accountService: AccountService,
    private toastr: ToastrService,
    private router: Router
  ) { }

  ngOnInit() {
  }

  async onSubmit() {
    try {
      const result = await this.accountService.login(this.login);
      if(!result)
        this.toastr.warning('Erro no login! Verifique suas credenciais!');

      // navego para a rota vazia novamente
      this.router.navigate(['']);
    } catch (error) {
      if(error)
        this.toastr.error(error + '!');
    }
  }  
}
