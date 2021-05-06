using FavoDeMel.Application.DTO;
using System;
using System.Collections.Generic;

namespace FavoDeMel.Application.Interfaces
{
    public interface IComandaAppService : IDisposable
    {
        IEnumerable<ComandaDTO> GetAll();
        object GetById(long id);
        void Add(ComandaCreateDTO customerViewModel);
        void Update(ComandaDTO customerViewModel);
        object Finalizar(long id);
        void Remove(long id);
        new void Dispose();
    }
}
