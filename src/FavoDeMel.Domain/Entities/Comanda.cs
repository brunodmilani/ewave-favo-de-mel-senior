using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FavorDeMel.Domain.Entities
{
    public class Comanda
    {
        public Comanda(long id, string nome, DateTime datahoraAbertura)
        {
            Id = id;
            Nome = nome;
            DatahoraAbertura = datahoraAbertura;
        }

        [Key]
        public long Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Nome { get; set; }
        [Required]
        public DateTime DatahoraAbertura { get; set; }
        public DateTime? DatahoraFechamento { get; set; }
        public virtual IList<Pedido> Pedidos { get; set; }
    }
}
