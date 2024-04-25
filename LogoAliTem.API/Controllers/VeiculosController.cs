using AutoMapper;
using LogoAliTem.API.Extensions;
using LogoAliTem.Application.Interfaces;
using LogoAliTem.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogoAliTem.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class VeiculosController : ControllerBase
{
    private readonly IVeiculoService _veiculoService;
    private readonly IMapper _mapper;

    public VeiculosController(IVeiculoService veiculoService, IMapper mapper)
    {
        _veiculoService = veiculoService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var veiculos = await _veiculoService.GetAllVeiculosAsync();

            if (veiculos == null) return NoContent();

            return Ok(_mapper.Map<List<VeiculoDto>>(veiculos));
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
            var Veiculo = await _veiculoService.GetVeiculoByIdAsync(id);

            if (Veiculo == null) return NoContent();

            return Ok(_mapper.Map<VeiculoDto>(Veiculo));
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar buscar Veiculo. Erro: {ex.Message}");
        }
    }

    [HttpGet("placa/{placa}")]
    public async Task<IActionResult> GetByPlaca(string placa)
    {
        try
        {
            var veiculos = await _veiculoService.GetVeiculoByPlacaAsync(placa);

            if (veiculos == null) return NoContent();

            return Ok(_mapper.Map<List<VeiculoDto>>(veiculos));
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar buscar Veiculo por placa. Erro: {ex.Message}");
        }
    }

    [HttpGet("motorista/{motoristaId}")]
    public async Task<IActionResult> GetVeiculosByMotorista(int motoristaId)
    {
        try
        {
            var veiculos = await _veiculoService.GetVeiculosByMotoristaIdAsync(motoristaId);

            if (veiculos == null) return NoContent();

            return Ok(_mapper.Map<VeiculoDto[]>(veiculos));
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar buscar Veiculos por motorista. Erro: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(VeiculoDto request)
    {
        try
        {
            var userId = User.GetUserId();
            var Veiculo = await _veiculoService.AddVeiculo(request, userId);

            if (Veiculo == null) return BadRequest("Erro ao tentar adicionar um Veiculo");

            return Ok(_mapper.Map<VeiculoDto>(Veiculo));
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar um Veiculo. Erro: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, VeiculoDto request)
    {
        try
        {
            var userId = User.GetUserId();
            var Veiculo = await _veiculoService.UpdateVeiculo(id, request, userId);

            if (Veiculo == null) return BadRequest("Erro ao tentar atualizar um Veiculo");

            return Ok(Veiculo);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, new {message = $"Erro ao tentar atualziar um Veiculo. Erro: {ex.Message}" });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var Veiculo = await _veiculoService.GetVeiculoByIdAsync(id);
            if (Veiculo == null) return NoContent();

            return await _veiculoService.DeleteVeiculo(id)
            ? Ok(new { message = "Deletado" })
            : throw new Exception("Ocorreu um problema não específico ao tentar deletar um Veiculo.");
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar um Veiculo. Erro: {ex.Message}");
        }
    }
}
