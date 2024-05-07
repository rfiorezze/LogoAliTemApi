using System.ComponentModel.DataAnnotations;

namespace LogoAliTem.Domain;

public class VeiculoDto
{
    public int Id { get; set; }

    [StringLength(7, ErrorMessage = "� permitido apenas 7 caractere.")]
    [Required(ErrorMessage = "O campo {0} � obrigat�rio.")]
    public string Placa { get; set; }

    [Required(ErrorMessage = "O campo {0} � obrigat�rio.")]
    public int Ano { get; set; }

    [Required(ErrorMessage = "O campo {0} � obrigat�rio.")]
    public string Marca { get; set; }

    [Required(ErrorMessage = "O campo {0} � obrigat�rio.")]
    public string Modelo { get; set; }

    public string Categoria { get; set; }
    public string TipoCarroceria { get; set; }

    [StringLength(17, ErrorMessage = "� permitido apenas 17 caractere.")]
    [Required(ErrorMessage = "O campo {0} � obrigat�rio.")]
    public string Chassi { get; set; }

    [StringLength(11, ErrorMessage = "� permitido apenas 11 caracteres.")]
    [Required(ErrorMessage = "O campo {0} � obrigat�rio.")]
    public string Renavam { get; set; }

    public int QtdEixos { get; set; }
    public int MotoristaId { get; set; }
    public int UserId { get; set; }
}
