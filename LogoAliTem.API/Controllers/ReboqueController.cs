using LogoAliTem.Application.Dtos;
using LogoAliTem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LogoAliTem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReboqueController : ControllerBase
    {
        private readonly IReboqueService _reboqueService;

        public ReboqueController(IReboqueService reboqueService)
        {
            _reboqueService = reboqueService;
        }

        /// <summary>
        /// Calcula o valor estimado do reboque com base na distância entre retirada e destino.
        /// </summary>
        [HttpPost("calcular")]
        public async Task<IActionResult> CalcularValor([FromBody] ReboqueCalculoDto request)
        {
            try
            {
                if (request == null || string.IsNullOrEmpty(request.LocalRetirada) || string.IsNullOrEmpty(request.LocalDestino))
                    return BadRequest("Os endereços de retirada e destino são obrigatórios.");

                var valor = await _reboqueService.CalcularValorAsync(request);
                if (valor == null)
                    return BadRequest("Não foi possível calcular o valor do reboque.");

                return Ok(new { valor });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao calcular valor do reboque. Erro: {ex.Message}");
            }
        }

        /// <summary>
        /// Solicita um reboque com base nos dados informados.
        /// </summary>
        [HttpPost("contratar")]
        public async Task<IActionResult> ContratarReboque([FromBody] ReboqueSolicitacaoDto request)
        {
            try
            {
                if (request == null || string.IsNullOrEmpty(request.LocalRetirada) || string.IsNullOrEmpty(request.LocalDestino))
                    return BadRequest("Os dados da solicitação são obrigatórios.");

                var sucesso = await _reboqueService.ContratarReboqueAsync(request);
                if (!sucesso)
                    return BadRequest("Erro ao solicitar o reboque.");

                return Ok(new { mensagem = "Reboque solicitado com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao contratar reboque. Erro: {ex.Message}");
            }
        }
    }
}
