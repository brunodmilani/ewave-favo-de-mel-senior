using FavorDeMel.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FavorDeMel.Infrastructure.Data.Context
{
    public class FavoDeMelDbContext : IdentityDbContext
    {
        public FavoDeMelDbContext(DbContextOptions<FavoDeMelDbContext> options)
            : base(options)
        { }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Comanda> Comandas { get; set; }


        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Cozinha", NormalizedName = "Cozinheiro(a)" });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Garcom", NormalizedName = "Garçom" });
        }
        #endregion

    }
}