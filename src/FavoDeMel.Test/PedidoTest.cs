using FavoDeMel.Infrastructure.Data.Repositories;
using FavorDeMel.Domain.Entities;
using FavorDeMel.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace FavoDeMel.Test
{
    public class PedidoTest
    {
        private readonly PedidoRepository pedidoRepository;
        private readonly ComandaRepository comandaRepository;
        private readonly FavoDeMelDbContext context;
        public static DbContextOptions<FavoDeMelDbContext> dbContextOptions { get; }
        public static string connectionString = "Data Source=localhost;initial catalog=FavoDeMel;persist security info=True;user id=sa;password=/password;multipleactiveresultsets=True;";

        static PedidoTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<FavoDeMelDbContext>()
                .UseSqlServer(connectionString)
                .Options;
        }

        public PedidoTest()
        {
            context = new FavoDeMelDbContext(dbContextOptions);
            pedidoRepository = new PedidoRepository(context);
            comandaRepository = new ComandaRepository(context);
        }

        [Fact]
        public void TestCreate()
        {
            Comanda comanda = new Comanda(0, "MESA 1", DateTime.Now);
            comandaRepository.Add(comanda);
            Assert.NotEqual(0, comanda.Id);

            Pedido pedido = new Pedido(0, comanda.Id, "Pedido 1");
            pedidoRepository.Add(pedido);
            Assert.NotEqual(0, pedido.Id);

            pedidoRepository.Remove(pedido.Id);
        }

        [Fact]
        public void TestUpdate()
        {
            Comanda comanda = new Comanda(0, "MESA 1", DateTime.Now);
            comandaRepository.Add(comanda);
            Assert.NotEqual(0, comanda.Id);

            Pedido pedido = new Pedido(0, comanda.Id, "Pedido 1");
            pedidoRepository.Add(pedido);
            Assert.NotEqual(0, pedido.Id);
            var nome = pedido.Descricao;
            Assert.NotEqual(0, pedido.Id);

            pedido.Descricao = "Pedido 1 (ALTERAÇÃO)";
            pedidoRepository.Update(pedido);
            Assert.NotEqual(nome, pedido.Descricao);

            pedidoRepository.Remove(pedido.Id);
        }

        [Fact]
        public void TestDelete()
        {
            Comanda comanda = new Comanda(0, "MESA 1", DateTime.Now);
            comandaRepository.Add(comanda);
            Assert.NotEqual(0, comanda.Id);

            Pedido pedido = new Pedido(0, comanda.Id, "Pedido 1");
            pedidoRepository.Add(pedido);
            Assert.NotEqual(0, pedido.Id);

            pedidoRepository.Remove(pedido.Id);
            var consulta = pedidoRepository.GetById(pedido.Id);
            Assert.Null(consulta);
        }
    }
}
