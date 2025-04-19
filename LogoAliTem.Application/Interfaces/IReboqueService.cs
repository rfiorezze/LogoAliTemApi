using LogoAliTem.Application.Dtos;
using System.Threading.Tasks;

namespace LogoAliTem.Application.Interfaces
{
    /// <summary>
    /// Serviço responsável pelas operações relacionadas a reboques.
    /// </summary>
    public interface IReboqueService
    {
        /// <summary>
        /// Calcula o valor estimado do reboque com base na distância entre os endereços informados.
        /// A distância total inclui: escritório → retirada → destino → escritório.
        /// </summary>
        /// <param name="request">Dados para cálculo do reboque</param>
        /// <returns>Valor estimado ou null em caso de erro</returns>
        Task<double?> CalcularValorAsync(ReboqueCalculoDto request);

        /// <summary>
        /// Registra a solicitação de reboque no banco de dados e envia notificação por e-mail.
        /// </summary>
        /// <param name="request">Dados da solicitação de reboque</param>
        /// <returns>True se salvo com sucesso, False em caso de erro</returns>
        Task<bool> ContratarReboqueAsync(ReboqueSolicitacaoDto request);

        /// <summary>
        /// Retorna os indicadores gerais de reboque, incluindo total de cálculos, contratações e taxa de conversão.
        /// </summary>
        /// <returns>DTO com os dados dos indicadores</returns>
        Task<IndicadoresReboqueDto> ObterIndicadoresAsync();
    }
}
