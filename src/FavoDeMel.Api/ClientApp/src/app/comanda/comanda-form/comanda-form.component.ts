import { ComandaService } from '../shared/comanda.service';
import { Component, OnInit } from '@angular/core';
import { Comanda } from '../shared/comanda';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-comanda-form',
  templateUrl: './comanda-form.component.html',
  styleUrls: ['./comanda-form.component.css']
})
export class ComandaFormComponent implements OnInit {
  comanda: Comanda = new Comanda();
  title: string;

  constructor(
    private activatedRoute: ActivatedRoute,
    private toastr: ToastrService,
    private router: Router,
    private comandaService: ComandaService
  ) { }

  ngOnInit() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    this.title = "Nova Comanda";
    if (id) {
      this.title = "Alterar Comanda";
      this.comandaService.getById(id).subscribe(comanda => {
        this.comanda = comanda;
      });
    }
  }

  onSubmit() {
    this.comandaService.save(this.comanda).subscribe(
      success => {
        //if (this.comanda.id) {
        //  this.toastr.success('Comanda alterada com sucesso!');
        //} else {
        //  this.toastr.success('Comanda criada com sucesso!');
        //}
        this.router.navigate(['/comanda']);
      },
      error => error.error ? this.toastr.warning(error.error) : this.toastr.error('Ocorreu um erro: ' + error.message)
    );
  }
}
