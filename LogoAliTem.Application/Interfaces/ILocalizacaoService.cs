using System.Threading.Tasks;

namespace LogoAliTem.Application.Interfaces
{
    public interface ILocalizacaoService
    {
        /// <summary>
        /// Obtém o endereço formatado a partir das coordenadas (latitude e longitude).
        /// </summary>
        Task<string> ObterEnderecoPorCoordenadasAsync(double latitude, double longitude);

        /// <summary>
        /// Calcula a distância (em km) entre dois endereços.
        /// </summary>
        Task<double> CalcularDistanciaKm(string origem, string destino);

        /// <summary>
        /// Obtém sugestões de endereços conforme o usuário digita.
        /// </summary>
        Task<string[]> ObterSugestoesEnderecosAsync(string input);
    }
}
