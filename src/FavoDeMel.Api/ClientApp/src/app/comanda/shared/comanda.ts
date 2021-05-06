import { Pedido } from "src/app/pedido/shared/pedido";

export class Comanda {
    id: number;
    nome: string;
    datahoraAbertura: string;
    datahoraFechamento: string;
    totalEmAndamento: number;
    totalFinalizado: number;
    totalPedidos: number;
    pedidos: Pedido
}
