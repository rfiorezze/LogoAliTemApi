using AutoMapper;
using EntityFramework.Exceptions.Common;
using LogoAliTem.Application.Dtos;
using LogoAliTem.Application.Interfaces;
using LogoAliTem.Domain;
using LogoAliTem.Persistence.Interfaces;
using System;
using System.Threading.Tasks;

namespace LogoAliTem.Application;

public class MotoristaService : IMotoristaService
{
    private readonly IBaseRepository _baseRepository;
    private readonly IMotoristaRepository _motoristaRepository;
    private readonly IMapper _mapper;
    public MotoristaService(IBaseRepository baseRepository, IMotoristaRepository motoristaRepository, IMapper mapper)
    {
        _baseRepository = baseRepository;
        _motoristaRepository = motoristaRepository;
        _mapper = mapper;
    }

    public async Task<MotoristaDto> AddMotorista(MotoristaDto requestDto, int userId)
    {
        try
        {
            var model = _mapper.Map<Motorista>(requestDto);
            model.UserId = userId;
            _baseRepository.Add<Motorista>(model);

            if (await _baseRepository.SaveChangesAsync())
            {
                var retornoModel = await _motoristaRepository.GetMotoristaByIdAsync(model.Id);
                return _mapper.Map<MotoristaDto>(retornoModel);
            }
            return null;
        }
        catch (UniqueConstraintException)
        {
            throw new Exception("Não foi possível salvar este motorista, pois outro motorista já utiliza esse número de celular.");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<MotoristaDto> UpdateMotorista(int motoristaId, MotoristaDto requestDto, int userId)
    {
        try
        {
            var model = _mapper.Map<Motorista>(requestDto);
            var motorista = await _motoristaRepository.GetMotoristaByIdAsync(motoristaId);
            if (motorista == null) return null;

            model.Id = motorista.Id;
            model.UserId = userId;

            _baseRepository.Update(model);
            if (await _baseRepository.SaveChangesAsync())
            {
                var retornoModel = await _motoristaRepository.GetMotoristaByIdAsync(model.Id);
                return _mapper.Map<MotoristaDto>(retornoModel);
            }
            return null;
        }
        catch (UniqueConstraintException)
        {
            throw new Exception("Não foi possível salvar este motorista, pois outro motorista já utiliza esse número de celular");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> DeleteMotorista(int motoristaId)
    {
        try
        {
            var motorista = await _motoristaRepository.GetMotoristaByIdAsync(motoristaId);
            if (motorista == null) throw new Exception("Motorista param delete não encontrado.");

            _baseRepository.Delete<Motorista>(motorista);
            return await _baseRepository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<MotoristaDto[]> GetAllMotoristasAsync()
    {
        try
        {
            var motoristas = await _motoristaRepository.GetAllMotoristasAsync();
            if (motoristas == null) return null;

            return _mapper.Map<MotoristaDto[]>(motoristas);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<MotoristaDto[]> GetAllMotoristasByNomeAsync(string nome)
    {
        try
        {
            var motoristas = await _motoristaRepository.GetAllMotoristasByNomeAsync(nome);
            if (motoristas == null) return null;

            return _mapper.Map<MotoristaDto[]>(motoristas);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<MotoristaDto> GetMotoristaByIdAsync(int motoristaId)
    {
        try
        {
            var motorista = await _motoristaRepository.GetMotoristaByIdAsync(motoristaId);
            if (motorista == null) return null;

            return _mapper.Map<MotoristaDto>(motorista);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}