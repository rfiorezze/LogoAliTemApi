using AutoMapper;
using LogoAliTem.API.Extensions;
using LogoAliTem.Application.Dtos;
using LogoAliTem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LogoAliTem.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class MotoristasController : ControllerBase
{
    private readonly IMotoristaService _motoristaService;

    public MotoristasController(IMotoristaService motoristaService)
    {
        _motoristaService = motoristaService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var motoristas = await _motoristaService.GetAllMotoristasAsync();

            if (motoristas == null) return NoContent();

            return Ok(motoristas);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar buscar motoristas. Erro: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var motorista = await _motoristaService.GetMotoristaByIdAsync(id);

            if (motorista == null) return NoContent();

            return Ok(motorista);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar buscar motorista. Erro: {ex.Message}");
        }
    }

    [HttpGet("nome/{nome}")]
    public async Task<IActionResult> GetByNome(string nome)
    {
        try
        {
            var motoristas = await _motoristaService.GetAllMotoristasByNomeAsync(nome);

            if (motoristas == null) return NoContent();

            return Ok(motoristas);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar buscar motoristas por Nome. Erro: {ex.Message}");
        }
    }

    [HttpGet("localidade/{estado}/{cidade}")]
    public async Task<IActionResult> GetByNome(string estado, string cidade)
    {
        try
        {
            var motoristas = await _motoristaService.GetAllMotoristasByEstadoCidadeAsync(estado, cidade);

            if (motoristas is null || !motoristas.Any()) return NoContent();

            return Ok(motoristas);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar buscar motoristas por estado e cidade. Erro: {ex.Message}");
        }
    }

    [HttpGet("cpf/{cpf}")]
    public async Task<IActionResult> GetByCpf(string cpf)
    {
        try
        {
            var motorista = await _motoristaService.GetMotoristaByCpfAsync(cpf);

            if (motorista == null) return NoContent();

            return Ok(motorista);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar buscar motoristas por Cpf. Erro: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(MotoristaDto request)
    {
        try
        {
            var userId = User.GetUserId();
            var motorista = await _motoristaService.AddMotorista(request, userId);

            if (motorista == null) return BadRequest("Erro ao tentar adicionar um motorista");

            return Ok(motorista);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar um motorista. Erro: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, MotoristaDto request)
    {
        try
        {
            var userId = User.GetUserId();
            var motorista = await _motoristaService.UpdateMotorista(id, request, userId);

            if (motorista == null) return BadRequest("Erro ao tentar atualizar um motorista");

            return Ok(motorista);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, new { message = $"Erro ao tentar atualziar um motorista. Erro: {ex.Message}" });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var motorista = await _motoristaService.GetMotoristaByIdAsync(id);
            if (motorista == null) return NoContent();

            return await _motoristaService.DeleteMotorista(id)
            ? Ok(new { message = "Deletado" })
            : throw new Exception("Ocorreu um problema não específico ao tentar deletar um motorista.");
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar um motorista. Erro: {ex.Message}");
        }
    }
}
