using AutoMapper;
using AutoMapper.QueryableExtensions;
using FavoDeMel.Application.DTO;
using FavoDeMel.Application.Interfaces;
using FavoDeMel.Domain.Interfaces;
using FavorDeMel.Domain.Entities;
using System;
using System.Collections.Generic;

namespace FavoDeMel.Application.Services
{
    public class PedidoAppService : IDisposable, IPedidoAppService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapper _mapper;

        public PedidoAppService(IMapper mapper, IPedidoRepository pedidoRepository)
        {
            _mapper = mapper;
            _pedidoRepository = pedidoRepository;
        }

        public IEnumerable<PedidoDTO> GetAll()
        {
            return _pedidoRepository.GetAll().ProjectTo<PedidoDTO>(_mapper.ConfigurationProvider);
        }

        public IEnumerable<PedidoDTO> GetAllByComanda(long comandaId)
        {
            return _pedidoRepository.GetAllByComanda(comandaId).ProjectTo<PedidoDTO>(_mapper.ConfigurationProvider);
        }

        public object GetById(long id)
        {
            return _mapper.Map<PedidoDTO>(_pedidoRepository.GetById(id));
        }

        public void Add(PedidoCreateDTO pedidoDTO)
        {
            var pedido = _mapper.Map<PedidoCreateDTO, Pedido>(pedidoDTO);
            _pedidoRepository.Add(pedido);
        }

        public void Update(PedidoDTO pedidoDTO)
        {
            var pedido = _mapper.Map<PedidoDTO, Pedido>(pedidoDTO);
            _pedidoRepository.Update(pedido);
        }

        public void Remove(long id)
        {
            _pedidoRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
