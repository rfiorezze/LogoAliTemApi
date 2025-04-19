using AutoMapper;
using EntityFramework.Exceptions.Common;
using LogoAliTem.Application.Interfaces;
using LogoAliTem.Domain;
using LogoAliTem.Persistence.Interfaces;
using System;
using System.Threading.Tasks;

namespace LogoAliTem.Application;

public class VeiculoService : IVeiculoService
{
    private readonly IBaseRepository _baseRepository;
    private readonly IVeiculoRepository _VeiculoRepository;
    private readonly IMapper _mapper;
    public VeiculoService(IBaseRepository baseRepository, IVeiculoRepository VeiculoRepository, IMapper mapper)
    {
        _baseRepository = baseRepository;
        _VeiculoRepository = VeiculoRepository;
        _mapper = mapper;
    }

    public async Task<VeiculoDto> AddVeiculo(VeiculoDto requestDto, int userId)
    {
        try
        {
            var model = _mapper.Map<Veiculo>(requestDto);
            model.UserId = userId;
            _baseRepository.Add<Veiculo>(model);

            if (await _baseRepository.SaveChangesAsync())
            {
                var retornoModel = await _VeiculoRepository.GetVeiculoByIdAsync(model.Id);
                return _mapper.Map<VeiculoDto>(retornoModel);
            }
            return null;
        }
        catch (UniqueConstraintException)
        {
            throw new Exception("Não foi possível salvar este Veiculo, pois outro Veiculo já utiliza esse número de celular.");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<VeiculoDto> UpdateVeiculo(int veiculoId, VeiculoDto requestDto, int userId)
    {
        try
        {
            var model = _mapper.Map<Veiculo>(requestDto);
            var veiculo = await _VeiculoRepository.GetVeiculoByIdAsync(veiculoId);
            if (veiculo == null) return null;

            model.Id = veiculo.Id;
            model.UserId = userId;
            model.DataAlteracao = DateTime.UtcNow;

            _baseRepository.Update(model);
            if (await _baseRepository.SaveChangesAsync())
            {
                var retornoModel = await _VeiculoRepository.GetVeiculoByIdAsync(model.Id);
                return _mapper.Map<VeiculoDto>(retornoModel);
            }
            return null;
        }
        catch (UniqueConstraintException)
        {
            throw new Exception("Não foi possível salvar este Veiculo, pois outro Veiculo já utiliza esse número de celular");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> DeleteVeiculo(int veiculoId)
    {
        try
        {
            var veiculo = await _VeiculoRepository.GetVeiculoByIdAsync(veiculoId);
            if (veiculo == null) throw new Exception("Veiculo param delete não encontrado.");

            _baseRepository.Delete<Veiculo>(veiculo);
            return await _baseRepository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<VeiculoDto[]> GetAllVeiculosAsync()
    {
        try
        {
            var veiculos = await _VeiculoRepository.GetAllVeiculosAsync();
            if (veiculos == null) return null;

            return _mapper.Map<VeiculoDto[]>(veiculos);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<VeiculoDto[]> GetVeiculoByPlacaAsync(string placa)
    {
        try
        {
            var veiculo = await _VeiculoRepository.GetVeiculoByPlacaAsync(placa);
            if (veiculo == null) return null;

            return _mapper.Map<VeiculoDto[]>(veiculo);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<VeiculoDto> GetVeiculoByIdAsync(int veiculoId)
    {
        try
        {
            var veiculo = await _VeiculoRepository.GetVeiculoByIdAsync(veiculoId);
            if (veiculo == null) return null;

            return _mapper.Map<VeiculoDto>(veiculo);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<VeiculoDto[]> GetVeiculosByMotoristaIdAsync(int motoristaId)
    {
        try
        {
            var veiculos = await _VeiculoRepository.GetVeiculosByMotoristaIdAsync(motoristaId);
            if (veiculos == null) return null;

            return _mapper.Map<VeiculoDto[]>(veiculos);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<int> ObterQuantidadeVeiculosAsync()
    {
        try
        {
            return await _VeiculoRepository.ContarVeiculosAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao obter a quantidade de veículos: " + ex.Message);
        }
    }
}