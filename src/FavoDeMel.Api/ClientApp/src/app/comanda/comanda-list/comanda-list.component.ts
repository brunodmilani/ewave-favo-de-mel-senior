import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import Pusher from 'pusher-js';
import { Observable, Subject } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { AccountService } from '../../account/shared/account.service';
import { Comanda } from '../shared/comanda';
import { ComandaService } from '../shared/comanda.service';

@Component({
  selector: 'app-comanda-list',
  templateUrl: './comanda-list.component.html',
  styleUrls: ['./comanda-list.component.css']
})
export class ComandaListComponent implements OnInit {
  id: string;
  mostraFechada: boolean = false;
  comandas: Observable<Comanda[]>;
  error = new Subject<boolean>();
  modalRef: BsModalRef;
  pusher: any;
  channel: any;

  constructor(
    private modalService: BsModalService,
    private accountService: AccountService,
    private comandaService: ComandaService,
    private toastr: ToastrService,
  ) {
    this.pusher = new Pusher(environment.pusher.key, {
      cluster: environment.pusher.cluster
    });

    this.channel = this.pusher.subscribe('favo_de_mel');
  }

  ngOnInit() {
    this.refresh();
    this.channel.bind('comanda', data => {
      this.refresh();
      if (data.message)
        this.toastr.warning(data.message);
    });
  }

  openModal(template: TemplateRef<any>, id: string) {
    this.id = id;
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  decline(): void {
    this.modalRef.hide();
  }

  fecharComanda() {
    this.comandaService.fecharComanda(this.id).subscribe(comandas => {
      //this.refresh();
      this.modalRef.hide();
      //this.toastr.success('Comanda fechada com sucesso!');
    });
  }

  refresh() {
    this.comandas = this.comandaService.getAll()
      .pipe(
        map(itens => {
          itens.forEach(e => {
            e.totalEmAndamento = e.pedidos.filter(a => a.status == 1).length,
              e.totalFinalizado = e.pedidos.filter(a => a.status == 2).length,
              e.totalPedidos = e.pedidos.filter(a => a.status != 3).length
          });
          return itens.filter(item => item.datahoraFechamento == null) // Não carregar comandas fechadas
        }),
        catchError(error => {
          this.error.next(true);
          this.toastr.error('Erro ao Carregar. Tente novamente mais tarde.');
          return "";
        })
      );
  }
}
