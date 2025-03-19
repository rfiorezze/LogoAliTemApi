using LogoAliTem.Application.Dtos;
using System.Threading.Tasks;

public interface IReboqueService
{
    /// <summary>
    /// Calcula o valor do reboque baseado na distância entre os endereços fornecidos.
    /// Inclui a distância do escritório até o local de retirada e a volta do destino ao escritório.
    /// </summary>
    /// <param name="request">DTO contendo os endereços de retirada e destino</param>
    /// <returns>O valor estimado do reboque ou null em caso de erro</returns>
    Task<double?> CalcularValorAsync(ReboqueCalculoDto request);

    /// <summary>
    /// Registra a solicitação de reboque no banco de dados.
    /// </summary>
    /// <param name="request">DTO contendo os detalhes da solicitação</param>
    /// <returns>True se a solicitação foi salva com sucesso, False em caso de falha</returns>
    Task<bool> ContratarReboqueAsync(ReboqueSolicitacaoDto request);
}