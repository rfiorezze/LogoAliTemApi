using System.Globalization;
using Flurl.Http;
using Polly;
using Polly.Retry;
using System.Threading.Tasks;
using LogoAliTem.Application.Interfaces;
using System.Linq;
using System;
using Microsoft.Extensions.Configuration;

public class LocalizacaoService : ILocalizacaoService
{
    private readonly string _googleApiKey;
    private readonly AsyncRetryPolicy _retryPolicy;

    public LocalizacaoService(IConfiguration config)
    {
        _googleApiKey = config["GoogleMapsApiKey"];
        _retryPolicy = Policy
            .Handle<FlurlHttpException>()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))); // Retry exponencial
    }

    public async Task<string> ObterEnderecoPorCoordenadasAsync(double latitude, double longitude)
    {
        var url = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={latitude.ToString(CultureInfo.InvariantCulture)},{longitude.ToString(CultureInfo.InvariantCulture)}&key={_googleApiKey}";

        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var response = await url.GetJsonAsync<GoogleGeocodeResponse>();

            if (response.Status != "OK" || response.Results == null || response.Results.Count == 0)
                throw new Exception("Endereço não encontrado.");

            // 🚀 1. Pegamos o endereço formatado completo
            var enderecoFormatado = response.Results.FirstOrDefault()?.FormattedAddress;
            if (!string.IsNullOrEmpty(enderecoFormatado))
                return enderecoFormatado;

            // 🚀 2. Se o `formatted_address` não existir, vamos construir manualmente
            var endereco = ConstruirEndereco(response.Results.FirstOrDefault());
            return endereco;
        });
    }
    /// <summary>
    /// Calcula a distância (em km) entre dois endereços.
    /// </summary>
    public async Task<double> CalcularDistanciaKm(string origem, string destino)
    {
        var url = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={Uri.EscapeDataString(origem)}&destinations={Uri.EscapeDataString(destino)}&key={_googleApiKey}";

        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var response = await url.GetJsonAsync<GoogleDistanceResponse>();

            if (response.Status != "OK" || response.Rows == null || response.Rows.Count == 0 ||
                response.Rows[0].Elements == null || response.Rows[0].Elements.Count == 0)
            {
                throw new Exception("Erro ao calcular distância.");
            }

            return response.Rows[0].Elements[0].Distance.Value / 1000.0; // metros para km
        });
    }

    /// <summary>
    /// Obtém sugestões de endereços conforme o usuário digita.
    /// </summary>
    public async Task<string[]> ObterSugestoesEnderecosAsync(string input)
    {
        var url = $"https://maps.googleapis.com/maps/api/place/autocomplete/json?input={Uri.EscapeDataString(input)}&key={_googleApiKey}&components=country:BR";

        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var response = await url.GetJsonAsync<GoogleAutocompleteResponse>();

            if (response.Status != "OK" || response.Predictions == null || response.Predictions.Count == 0)
                throw new Exception("Nenhuma sugestão encontrada.");

            return response.Predictions.Select(p => p.Description).ToArray();
        });
    }

    private string ConstruirEndereco(GoogleGeocodeResult result)
    {
        if (result == null || result.AddressComponents == null) return "Endereço não encontrado.";

        var numero = result.AddressComponents.FirstOrDefault(ac => ac.Types.Contains("street_number"))?.LongName;
        var rua = result.AddressComponents.FirstOrDefault(ac => ac.Types.Contains("route"))?.LongName;
        var bairro = result.AddressComponents.FirstOrDefault(ac => ac.Types.Contains("sublocality"))?.LongName;
        var cidade = result.AddressComponents.FirstOrDefault(ac => ac.Types.Contains("administrative_area_level_2"))?.LongName;
        var estado = result.AddressComponents.FirstOrDefault(ac => ac.Types.Contains("administrative_area_level_1"))?.ShortName;
        var pais = result.AddressComponents.FirstOrDefault(ac => ac.Types.Contains("country"))?.LongName;
        var cep = result.AddressComponents.FirstOrDefault(ac => ac.Types.Contains("postal_code"))?.LongName;

        // 🔹 Construção dinâmica do endereço
        var enderecoCompleto = $"{(rua != null ? rua + ", " : "")}{(numero != null ? numero + " - " : "")}{(bairro != null ? bairro + ", " : "")}{(cidade != null ? cidade + " - " : "")}{(estado != null ? estado + ", " : "")}{(cep != null ? cep + ", " : "")}{pais}";

        return enderecoCompleto.Trim(' ', ',');
    }
}
