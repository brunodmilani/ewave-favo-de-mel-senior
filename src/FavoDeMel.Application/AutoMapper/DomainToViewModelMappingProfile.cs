using AutoMapper;
using FavoDeMel.Application.DTO;
using FavorDeMel.Domain.Entities;

namespace FavoDeMel.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Comanda, ComandaDTO>();
            CreateMap<Pedido, PedidoDTO>();
        }
    }
}
