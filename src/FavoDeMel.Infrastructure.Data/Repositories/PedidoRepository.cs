using FavoDeMel.Domain.Interfaces;
using FavorDeMel.Domain.Entities;
using FavorDeMel.Infrastructure.Data.Context;
using System.Linq;

namespace FavoDeMel.Infrastructure.Data.Repositories
{
    public class PedidoRepository : RepositoryBase<Pedido>, IPedidoRepository
    {
        protected readonly FavoDeMelDbContext _context;

        public PedidoRepository(FavoDeMelDbContext context)
            : base(context)
        {
            _context = context;
        }

        public virtual IQueryable<Pedido> GetAllByComanda(long comandaId)
        {
            return _context.Pedidos.Where(x => x.ComandaId == comandaId);
        }
    }
}
