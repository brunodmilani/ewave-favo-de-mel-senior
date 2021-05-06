using FavorDeMel.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FavoDeMel.Application.DTO
{
    public enum Status
    {
        EmAndamento = 1,
        Finalizado = 2,
        Cancelado = 3
    }

    public class PedidoDTO
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public long ComandaId { get; set; }
        [Required]
        [StringLength(255)]
        public string Descricao { get; set; }
        public int Status { get; set; }
        public virtual Comanda Comanda { get; set; }
    }

    public class PedidoCreateDTO
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public long ComandaId { get; set; }
        [Required]
        [StringLength(255)]
        public string Descricao { get; set; }
        public virtual Comanda Comanda { get; set; }
    }
}
