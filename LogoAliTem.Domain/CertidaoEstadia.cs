using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogoAliTem.Domain
{
    public class CertidaoEstadia
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("CalculoEstadia")]
        public int CalculoEstadiaId { get; set; }

        public virtual CalculoEstadia CalculoEstadia { get; set; }

        [Required, StringLength(50)]
        public string Placa { get; set; }

        [StringLength(50)]
        public string RNTRC { get; set; }

        [Required, StringLength(150)]
        public string NomeMotorista { get; set; }

        [Required, StringLength(20)]
        public string CpfCnpjMotorista { get; set; }

        [Required, StringLength(100)]
        public string EmailMotorista { get; set; }

        [Required, StringLength(20)]
        public string TelefoneMotorista { get; set; }

        [StringLength(10)]
        public string CepMotorista { get; set; }

        [StringLength(100)]
        public string LogradouroMotorista { get; set; }

        [StringLength(20)]
        public string NumeroMotorista { get; set; }

        [StringLength(100)]
        public string ComplementoMotorista { get; set; }

        [StringLength(100)]
        public string BairroMotorista { get; set; }

        [StringLength(100)]
        public string CidadeMotorista { get; set; }

        [StringLength(2)]
        public string EstadoMotorista { get; set; }

        [StringLength(20)]
        public string CpfCnpjLocalCarga { get; set; }

        [Required, StringLength(150)]
        public string NomeLocalCarga { get; set; }

        [StringLength(100)]
        public string EmailLocalCarga { get; set; }

        [StringLength(20)]
        public string TelefoneLocalCarga { get; set; }

        [Required, StringLength(10)]
        public string CepLocalCarga { get; set; }

        [Required, StringLength(100)]
        public string LogradouroLocalCarga { get; set; }

        [Required, StringLength(20)]
        public string NumeroLocalCarga { get; set; }

        [StringLength(100)]
        public string ComplementoLocalCarga { get; set; }

        [Required, StringLength(100)]
        public string BairroLocalCarga { get; set; }

        [Required, StringLength(100)]
        public string CidadeLocalCarga { get; set; }

        [Required, StringLength(2)]
        public string EstadoLocalCarga { get; set; }

        [StringLength(20)]
        public string CpfCnpjLocalDescarga { get; set; }

        [Required, StringLength(150)]
        public string NomeLocalDescarga { get; set; }

        [StringLength(100)]
        public string EmailLocalDescarga { get; set; }

        [StringLength(20)]
        public string TelefoneLocalDescarga { get; set; }

        [Required, StringLength(10)]
        public string CepLocalDescarga { get; set; }

        [Required, StringLength(100)]
        public string LogradouroLocalDescarga { get; set; }

        [Required, StringLength(20)]
        public string NumeroLocalDescarga { get; set; }

        [StringLength(100)]
        public string ComplementoLocalDescarga { get; set; }

        [Required, StringLength(100)]
        public string BairroLocalDescarga { get; set; }

        [Required, StringLength(100)]
        public string CidadeLocalDescarga { get; set; }

        [Required, StringLength(2)]
        public string EstadoLocalDescarga { get; set; }

        [StringLength(50)]
        public string CteCiotContratante { get; set; }

        [StringLength(20)]
        public string CpfCnpjContratante { get; set; }

        [StringLength(150)]
        public string NomeContratante { get; set; }

        [StringLength(100)]
        public string EmailContratante { get; set; }

        [StringLength(20)]
        public string TelefoneContratante { get; set; }

        [StringLength(10)]
        public string CepContratante { get; set; }

        [StringLength(100)]
        public string LogradouroContratante { get; set; }

        [StringLength(20)]
        public string NumeroContratante { get; set; }

        [StringLength(100)]
        public string ComplementoContratante { get; set; }

        [StringLength(100)]
        public string BairroContratante { get; set; }

        [StringLength(100)]
        public string CidadeContratante { get; set; }

        [StringLength(2)]
        public string EstadoContratante { get; set; }

        [Required]
        public byte[] Arquivo { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    }
}
