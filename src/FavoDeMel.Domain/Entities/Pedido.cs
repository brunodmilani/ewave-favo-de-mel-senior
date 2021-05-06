using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FavorDeMel.Domain.Entities
{
    public class Pedido
    {
        public Pedido(long id, long comandaId, string descricao)
        {
            Id = id;
            ComandaId = comandaId;
            Descricao = descricao;
            Status = 1; //Em Andamento
        }

        [Key]
        public long Id { get; set; }
        [Required]
        public long ComandaId { get; set; }
        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Descricao { get; set; }
        [Required]
        public int Status { get; set; }
        public virtual Comanda Comanda { get; set; }
    }
}
