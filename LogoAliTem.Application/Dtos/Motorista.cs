using System.ComponentModel.DataAnnotations;

namespace LogoAliTem.Application.Dtos;

public class MotoristaDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public string Nome { get; set; }

    [StringLength(11, ErrorMessage = "É permitido apenas 11 caractere.")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public string Cpf { get; set; }

    [StringLength(1, ErrorMessage = "É permitido apenas 1 caractere.")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public string Sexo { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public string DataNascimento { get; set; }

    [StringLength(9, ErrorMessage = "É permitido apenas 9 caracteres.")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public string NumeroCNH { get; set; }

    [StringLength(1, ErrorMessage = "É permitido apenas 1 caractere.")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public string CategoriaCNH { get; set; }
    public string DataVencimentoCNH { get; set; }

    [Display(Name = "e-mail")]
    [EmailAddress(ErrorMessage = "É necessário ser um {0} válido.")]
    public string Email { get; set; }
    public string Celular { get; set; }
    public string Cep { get; set; }
    public string Logradouro { get; set; }
    public string Numero { get; set; }
    public string Complemento { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public int UserId { get; set; }
}
