import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import Pusher from 'pusher-js';
import { Observable, Subject } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { AccountService } from '../../account/shared/account.service';
import { Pedido } from '../shared/pedido';
import { PedidoService } from '../shared/pedido.service';

@Component({
  selector: 'app-pedido-list',
  templateUrl: './pedido-list.component.html',
  styleUrls: ['./pedido-list.component.css']
})
export class PedidoListComponent implements OnInit {
  pedidos: Observable<Pedido[]>;
  pedido: Pedido;
  modalRef: BsModalRef;
  error = new Subject<boolean>();
  comanda: string = "";
  pusher: any;
  channel: any;

  constructor(
    private modalService: BsModalService,
    private accountService: AccountService,
    private activatedRoute: ActivatedRoute,
    private toastr: ToastrService,
    private pedidoService: PedidoService
  ) {
    this.pusher = new Pusher(environment.pusher.key, {
      cluster: environment.pusher.cluster
    });

    this.channel = this.pusher.subscribe('favo_de_mel');
  }

  ngOnInit() {
    this.comanda = this.activatedRoute.snapshot.paramMap.get('comanda');
    if (this.comanda) {
      this.refresh();
      this.channel.bind('pedido', data => {
        this.refresh();
        if (data.message)
          this.toastr.warning(data.message);
      });
    }
  }

  openModal(template: TemplateRef<any>, pedido: Pedido) {
    this.pedido = pedido;
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  decline(): void {
    this.modalRef.hide();
  }

  cancelar() {
    this.pedido.status = 3;
    this.pedidoService.save(this.pedido).subscribe(pedidos => {
      //this.refresh();
      this.modalRef.hide();
      //this.toastr.success('Pedido cancelado.');
    });
  }

  finalizar() {
    this.pedido.status = 2;
    this.pedidoService.save(this.pedido).subscribe(pedidos => {
      //this.refresh();
      this.modalRef.hide();
      //this.toastr.success('Pedido finalizado.');
    });
  }

  refresh() {
    this.pedidos = this.pedidoService.getAllComanda(this.comanda)
      .pipe(
        map(itens => {
          return itens.filter(item => item.status != 3) // não carregar pedidos cancelados
        }),
        catchError(error => {
          this.error.next(true);
          this.toastr.error('Erro ao Carregar. Tente novamente mais tarde.');
          return "";
        })
      );
  }
}
