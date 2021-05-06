using FavoDeMel.Application.DTO;
using FavoDeMel.Infrastructure.Data.Repositories;
using FavorDeMel.Domain.Entities;
using FavorDeMel.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace FavoDeMel.Test
{
    public class ComandaTest
    {
        private readonly ComandaRepository comandaRepository;
        private readonly FavoDeMelDbContext context;
        public static DbContextOptions<FavoDeMelDbContext> dbContextOptions { get; }
        public static string connectionString = "Data Source=localhost;initial catalog=FavoDeMel;persist security info=True;user id=sa;password=/password;multipleactiveresultsets=True;";

        static ComandaTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<FavoDeMelDbContext>()
                .UseSqlServer(connectionString)
                .Options;
        }

        public ComandaTest()
        {
            context = new FavoDeMelDbContext(dbContextOptions);
            comandaRepository = new ComandaRepository(context);
        }

        [Fact]
        public void TestCreate()
        {
            Comanda comanda = new Comanda(0, "MESA 1", DateTime.Now);
            comandaRepository.Add(comanda);
            Assert.NotEqual(0, comanda.Id);

            comandaRepository.Remove(comanda.Id);
        }

        [Fact]
        public void TestUpdate()
        {
            Comanda comanda = new Comanda(0, "MESA 1", DateTime.Now);
            comandaRepository.Add(comanda);
            var nome = comanda.Nome;
            Assert.NotEqual(0, comanda.Id);

            comanda.Nome = "MESA 1 (ALTERAÇÃO)";
            comandaRepository.Update(comanda);
            Assert.NotEqual(nome, comanda.Nome);

            comandaRepository.Remove(comanda.Id);
        }

        [Fact]
        public void TestDelete()
        {
            Comanda comanda = new Comanda(0, "MESA 1", DateTime.Now);
            comandaRepository.Add(comanda);
            Assert.NotEqual(0, comanda.Id);

            comandaRepository.Remove(comanda.Id);
            var consulta = comandaRepository.GetById(comanda.Id);
            Assert.Null(consulta);
        }
    }
}
