import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { delay, tap } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { Comanda } from './comanda';

@Injectable({
  providedIn: 'root'
})
export class ComandaService {
  
  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get<Comanda[]>(`${environment.api}/comanda`)
      .pipe(
        delay(1000),
        tap(console.log)
      );
  }

  getById(id: string) {
    return this.http.get<Comanda>(`${environment.api}/comanda/${id}`);
  }

  save(comanda: Comanda) {
    const comandaBody = {
      id: comanda.id,
      nome: comanda.nome,
      datahoraAbertura: comanda.datahoraAbertura,
      datahoraFechamento: comanda.datahoraFechamento
    };

    if (comanda.id) {
      return this.http.put<Comanda>(`${environment.api}/comanda/${comanda.id}`, comandaBody);
    } else {
      return this.http.post<Comanda>(`${environment.api}/comanda`, comandaBody);
    }
  }

  fecharComanda(id: string) {    
    if (id) {
      return this.http.put<Comanda>(`${environment.api}/comanda/Finalizar/${id}`, '');
    } 
  }

  delete(id: string) {
    return this.http.delete(`${environment.api}/comanda/${id}`);
  }
}
