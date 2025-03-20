using LogoAliTem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class LocalizacaoController : ControllerBase
{
    private readonly ILocalizacaoService _localizacaoService;

    public LocalizacaoController(ILocalizacaoService localizacaoService)
    {
        _localizacaoService = localizacaoService;
    }

    /// <summary>
    /// Obtém o endereço formatado a partir das coordenadas (latitude e longitude).
    /// </summary>
    [HttpGet("geolocalizacao")]
    [AllowAnonymous]
    public async Task<IActionResult> ObterEndereco([FromQuery] double latitude, [FromQuery] double longitude)
    {
        try
        {
            var endereco = await _localizacaoService.ObterEnderecoPorCoordenadasAsync(latitude, longitude);

            if (string.IsNullOrEmpty(endereco))
                return NotFound("Endereço não encontrado.");

            return Ok(new { endereco });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter endereço. Erro: {ex.Message}");
        }
    }

    /// <summary>
    /// Calcula a distância entre dois endereços.
    /// </summary>
    [HttpGet("distancia")]
    [AllowAnonymous]
    public async Task<IActionResult> CalcularDistancia([FromQuery] string origem, [FromQuery] string destino)
    {
        try
        {
            double distancia = await _localizacaoService.CalcularDistanciaKm(origem, destino);
            return Ok(new { distanciaKm = distancia });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao calcular distância. Erro: {ex.Message}");
        }
    }

    /// <summary>
    /// Obtém sugestões de endereços conforme o usuário digita.
    /// </summary>
    [HttpGet("sugestoes")]
    [AllowAnonymous]
    public async Task<IActionResult> ObterSugestoes([FromQuery] string input)
    {
        try
         {
            var sugestoes = await _localizacaoService.ObterSugestoesEnderecosAsync(input);

            if (sugestoes == null || sugestoes.Length == 0)
                return NoContent();

            return Ok(sugestoes);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter sugestões. Erro: {ex.Message}");
        }
    }
}
