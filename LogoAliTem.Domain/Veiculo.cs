namespace LogoAliTem.Domain;

public class Veiculo
{
    public int Id { get; set; }
    public string Placa { get; set; }
    public int Ano { get; set; }
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public string Categoria { get; set; }
    public string TipoCarroceria { get; set; }
    public int QtdEixos { get; set; }
    public int MotoristaId { get; set; }
    public Motorista Motorista { get; set; }
}
