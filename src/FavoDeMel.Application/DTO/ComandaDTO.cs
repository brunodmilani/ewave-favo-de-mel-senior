using FavorDeMel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FavoDeMel.Application.DTO
{
    public class ComandaDTO
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }
        public DateTime DatahoraAbertura { get; set; }
        public DateTime? DatahoraFechamento { get; set; }
        public virtual IList<Pedido> Pedidos { get; set; }
    }

    public class ComandaCreateDTO
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }
        public DateTime DatahoraAbertura {
            get
            {
                return DateTime.Now;
            }
        }
    }
}
