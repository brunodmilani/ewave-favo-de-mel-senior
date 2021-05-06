using AutoMapper;
using FavoDeMel.Application.DTO;
using FavorDeMel.Domain.Entities;

namespace FavoDeMel.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ComandaDTO, Comanda>()
                .ConstructUsing(m => new Comanda(m.Id, m.Nome, m.DatahoraAbertura));

            CreateMap<ComandaCreateDTO, Comanda>()
                .ConstructUsing(m => new Comanda(m.Id, m.Nome, m.DatahoraAbertura));

            CreateMap<PedidoDTO, Pedido>()
                .ConstructUsing(m => new Pedido(m.Id, m.ComandaId, m.Descricao));

            CreateMap<PedidoCreateDTO, Pedido>()
                .ConstructUsing(m => new Pedido(m.Id, m.ComandaId, m.Descricao));
        }
    }
}
