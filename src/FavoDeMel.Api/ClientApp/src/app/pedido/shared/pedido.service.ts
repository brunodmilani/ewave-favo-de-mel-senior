import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { delay, tap } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { Pedido } from './pedido';

@Injectable({
  providedIn: 'root'
})
export class PedidoService {
  
  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get<Pedido[]>(`${environment.api}/pedido`);
  }

  getAllComanda(comandaId: string) {
    return this.http.get<Pedido[]>(`${environment.api}/pedido/comanda/${comandaId}`)
      .pipe(
        delay(1000),
        tap(console.log)
      );
  }

  getById(id: string) {
    return this.http.get<Pedido>(`${environment.api}/pedido/${id}`);
  }

  save(pedido: Pedido) {
    const pedidoBody = {
      id: pedido.id,
      comandaId: pedido.comandaId,
      descricao: pedido.descricao,
      status: pedido.status
    };

    if (pedido.id) {
      return this.http.put<Pedido>(`${environment.api}/pedido`, pedidoBody);
    } else {
      return this.http.post<Pedido>(`${environment.api}/pedido`, pedidoBody);
    }
  }

  delete(id: string) {
    return this.http.delete(`${environment.api}/pedido/${id}`);
  }
}
