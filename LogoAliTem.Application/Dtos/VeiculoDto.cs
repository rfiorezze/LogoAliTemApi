using System.ComponentModel.DataAnnotations;

namespace LogoAliTem.Domain;

public class VeiculoDto
{
    public int Id { get; set; }

    [StringLength(7, ErrorMessage = "É permitido apenas 7 caractere.")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public string Placa { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public int Ano { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public string Marca { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public string Modelo { get; set; }

    public string Categoria { get; set; }
    public string TipoCarroceria { get; set; }

    [StringLength(17, ErrorMessage = "É permitido apenas 17 caractere.")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public string Chassi { get; set; }

    [StringLength(11, ErrorMessage = "É permitido apenas 11 caracteres.")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public string Renavam { get; set; }

    public int QtdEixos { get; set; }
    public int MotoristaId { get; set; }
    public int UserId { get; set; }
}
