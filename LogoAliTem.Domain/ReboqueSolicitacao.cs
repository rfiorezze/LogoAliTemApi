using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogoAliTem.Domain
{
    public class ReboqueSolicitacao
    {
        [Key]
        public int Id { get; set; }

        public int? UserId { get; set; }

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
        public double ValorEstimado { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DataSolicitacao { get; set; } = DateTime.UtcNow;
    }
}
