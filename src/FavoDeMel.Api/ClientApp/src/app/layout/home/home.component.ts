import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/account/shared/account.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  role: string;

  constructor(
    private accountService: AccountService,
    private router: Router
  ) { }

  ngOnInit() {
    if (this.accountService.getRole() === 'Garcom')
      this.role = 'Garçom';
    else if (this.accountService.getRole() === 'Cozinha')
      this.role = 'Cozinheiro(a)';
  }

  async onSubmit() {
    try {
      await this.accountService.logout();
      console.log(`Logout efetuado`);

      // navego para a rota vazia novamente
      this.router.navigate(['login']);
    } catch (error) {
      console.error(error);
    }
  }
}
