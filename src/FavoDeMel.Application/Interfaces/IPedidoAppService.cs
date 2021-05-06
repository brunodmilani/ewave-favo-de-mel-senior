using FavoDeMel.Application.DTO;
using System;
using System.Collections.Generic;

namespace FavoDeMel.Application.Interfaces
{
    public interface IPedidoAppService : IDisposable
    {
        IEnumerable<PedidoDTO> GetAll();
        IEnumerable<PedidoDTO> GetAllByComanda(long comandaId);
        object GetById(long id);
        void Add(PedidoCreateDTO customerViewModel);
        void Update(PedidoDTO customerViewModel);
        void Remove(long id);
        new void Dispose();
    }
}
