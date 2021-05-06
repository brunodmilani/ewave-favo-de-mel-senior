using FavoDeMel.Domain.Interfaces;
using FavorDeMel.Domain.Entities;
using FavorDeMel.Infrastructure.Data.Context;

namespace FavoDeMel.Infrastructure.Data.Repositories
{
    public class ComandaRepository : RepositoryBase<Comanda>, IComandaRepository
    {
        public ComandaRepository(FavoDeMelDbContext context)
            : base(context)
        {

        }
    }
}
