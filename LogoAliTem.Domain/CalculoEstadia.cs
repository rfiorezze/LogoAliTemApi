using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogoAliTem.Domain
{
    public class CalculoEstadia
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DataChegada { get; set; }

        [Required]
        public TimeSpan HoraChegada { get; set; }

        [Required]
        public DateTime DataSaida { get; set; }

        [Required]
        public TimeSpan HoraSaida { get; set; }

        [Required]
        public double CapacidadeCargaVeiculo { get; set; }

        [Required]
        public double ValorCalculado { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DataCalculo { get; set; } = DateTime.UtcNow;

        public virtual CertidaoEstadia CertidaoEstadia { get; set; }
    }
}
