using AutoMapper;
using AutoMapper.QueryableExtensions;
using FavoDeMel.Application.DTO;
using FavoDeMel.Application.Interfaces;
using FavoDeMel.Domain.Interfaces;
using FavorDeMel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FavoDeMel.Application.Services
{
    public class ComandaAppService : IDisposable, IComandaAppService
    {
        private readonly IComandaRepository _comandaRepository;
        private readonly IMapper _mapper;

        public ComandaAppService(IMapper mapper, IComandaRepository comandaRepository)
        {
            _mapper = mapper;
            _comandaRepository = comandaRepository;
        }

        public IEnumerable<ComandaDTO> GetAll()
        {
            return _comandaRepository.GetAll().ProjectTo<ComandaDTO>(_mapper.ConfigurationProvider);
        }

        public object GetById(long id)
        {
            return _mapper.Map<ComandaDTO>(_comandaRepository.GetById(id));
        }

        public void Add(ComandaCreateDTO comandaDTO)
        {
            var comanda = _mapper.Map<ComandaCreateDTO, Comanda>(comandaDTO);
            _comandaRepository.Add(comanda);
        }

        public void Update(ComandaDTO comandaDTO)
        {
            var  comanda = _mapper.Map<ComandaDTO, Comanda>(comandaDTO);
            _comandaRepository.Update(comanda);
        }

        public object Finalizar(long id)
        {
            var comanda = _comandaRepository.GetById(id);
            comanda.DatahoraFechamento = DateTime.Now;
            _comandaRepository.Update(comanda);
            return _mapper.Map<ComandaDTO>(comanda);
        }

        public void Remove(long id)
        {
            _comandaRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
