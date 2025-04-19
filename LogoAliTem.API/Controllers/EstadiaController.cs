using LogoAliTem.Application.Dtos;
using LogoAliTem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LogoAliTem.API.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class EstadiaController : ControllerBase
    {
        private readonly IEstadiaService _estadiaService;

        public EstadiaController(IEstadiaService estadiaService)
        {
            _estadiaService = estadiaService;
        }

        /// <summary>
        /// Registra um novo cálculo de estadia.
        /// </summary>
        [HttpPost("calcular")]
        public async Task<IActionResult> CalcularEstadia([FromBody] CalculoEstadiaDto request)
        {
            try
            {
                if (request == null)
                    return BadRequest("Dados do cálculo são obrigatórios.");

                var resultado = await _estadiaService.RegistrarCalculoAsync(request.ToEntity());
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao registrar cálculo de estadia. Erro: {ex.Message}");
            }
        }

        /// <summary>
        /// Registra uma nova certidão de estadia vinculada a um cálculo existente.
        /// </summary>
        [HttpPost("certidao")]
        [RequestSizeLimit(10_000_000)] // aumenta o limite para 10MB
        public async Task<IActionResult> GerarCertidao(
            [FromForm] CertidaoEstadiaDto request,
            [FromForm] IFormFile arquivo)
        {
            try
            {
                if (request == null || request.CalculoEstadiaId <= 0)
                    return BadRequest("Erro ao gerar certidão! Por favor, refaça o calculo da estadia!");

                byte[] pdfBytes = null;
                string nomeArquivo = null;

                if (arquivo != null && arquivo.Length > 0)
                {
                    using var memoryStream = new MemoryStream();
                    await arquivo.CopyToAsync(memoryStream);
                    pdfBytes = memoryStream.ToArray();
                    nomeArquivo = arquivo.FileName;
                }

                var entidade = request.ToEntity(pdfBytes);

                var resultado = await _estadiaService.RegistrarCertidaoAsync(entidade);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao registrar certidão de estadia. Erro: {ex.Message}");
            }
        }

        [HttpGet("indicadores")]
        public async Task<IActionResult> ObterIndicadores()
        {
            try
            {
                var (totalCalculos, totalCertidoes, taxaConversao) = await _estadiaService.ObterIndicadoresAsync();

                return Ok(new
                {
                    totalCalculos,
                    totalCertidoes,
                    taxaConversao = taxaConversao.ToString("F2") // duas casas decimais, ex: "58.33"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter indicadores. Erro: {ex.Message}");
            }
        }
    }
}
