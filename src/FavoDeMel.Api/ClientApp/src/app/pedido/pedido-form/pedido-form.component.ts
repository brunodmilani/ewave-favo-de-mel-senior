import { PedidoService } from '../shared/pedido.service';
import { Component, OnInit } from '@angular/core';
import { Pedido } from '../shared/pedido';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-pedido-form',
  templateUrl: './pedido-form.component.html',
  styleUrls: ['./pedido-form.component.css']
})
export class PedidoFormComponent implements OnInit {
  pedido: Pedido = new Pedido();
  comanda: string;
  
  constructor(
    private activatedRoute: ActivatedRoute,
    private toastr: ToastrService,
    private router: Router,
    private pedidoService: PedidoService
  ) { }

  ngOnInit() {
    this.comanda = this.activatedRoute.snapshot.paramMap.get('comanda');
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (id) {
      this.pedidoService.getById(id).subscribe(pedido => {
        this.pedido = pedido;
      });
    }
  }

  onSubmit() {    
    if (this.comanda)
      this.pedido.comandaId = parseInt(this.comanda);
    this.pedidoService.save(this.pedido).subscribe(pedido => {
      //this.toastr.success('Pedido realizado com sucesso!');
      this.router.navigate(['/comanda', this.comanda, 'pedido']);
    });
  }
}
