using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogoAliTem.Domain
{
    public class CalculoReboque
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string TipoVeiculo { get; set; }

        [Required]
        [StringLength(255)]
        public string LocalRetirada { get; set; }

        [Required]
        [StringLength(255)]
        public string LocalDestino { get; set; }

        [Required]
        public double DistanciaTotal { get; set; }

        [Required]
        public double ValorCalculado { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DataCalculo { get; set; } = DateTime.UtcNow;
    }
}
