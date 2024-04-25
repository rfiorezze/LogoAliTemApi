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
    public int QtdEixos { get; set; }
    public int MotoristaId { get; set; }
    public int UserId { get; set; }
}
