import { Comanda } from "src/app/comanda/shared/comanda";

export class Pedido {
    id: number;
    comandaId: number;
    descricao: string;
    status: number;
    comanda: Comanda
}
