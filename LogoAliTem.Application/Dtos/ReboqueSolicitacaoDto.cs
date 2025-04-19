namespace LogoAliTem.Application.Dtos;

public class ReboqueSolicitacaoDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string TipoVeiculo { get; set; }
    public string LocalRetirada { get; set; }
    public string LocalDestino { get; set; }
    public double ValorEstimado { get; set; }
    public string Placa { get; set; }
}