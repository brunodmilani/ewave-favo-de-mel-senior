using FavorDeMel.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace FavoDeMel.Domain.Interfaces
{
    public interface IPedidoRepository : IRepositoryBase<Pedido>
    {
        IQueryable<Pedido> GetAllByComanda(long comandaId);
    }
}
