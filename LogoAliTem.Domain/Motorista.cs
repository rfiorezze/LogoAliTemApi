using LogoAliTem.Domain.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogoAliTem.Domain;

[Table("Motoristas")]
public class Motorista
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Nome { get; set; }

    [Required]
    [MaxLength(11)]
    public string Cpf { get; set; }

    [Required]
    [MaxLength(1)]
    public string Sexo { get; set; }
    public DateTime DataNascimento { get; set; }
    public string NumeroCNH { get; set; }

    [MaxLength(1)]
    public string CategoriaCNH { get; set; }
    public DateTime? DataVencimentoCNH { get; set; }
    public string Email { get; set; }
    public string TelefoneFixo { get; set; }
    public string Celular { get; set; }
    public string Cep { get; set; }
    public string Logradouro { get; set; }
    public string Numero { get; set; }
    public string Complemento { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public int UserId { get; set; }
    public User User {get; set; }
}