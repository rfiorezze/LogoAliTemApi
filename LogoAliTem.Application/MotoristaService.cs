using AutoMapper;
using EntityFramework.Exceptions.Common;
using LogoAliTem.Application.Dtos;
using LogoAliTem.Application.Interfaces;
using LogoAliTem.Domain;
using LogoAliTem.Domain.Enum;
using LogoAliTem.Persistence.Interfaces;
using System;
using System.Threading.Tasks;

namespace LogoAliTem.Application;
public class MotoristaService : IMotoristaService
{
    private readonly IBaseRepository _baseRepository;
    private readonly IMotoristaRepository _motoristaRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public MotoristaService(IBaseRepository baseRepository, IMotoristaRepository motoristaRepository,
                            IMapper mapper, IUserRepository userRepository)
    {
        _baseRepository = baseRepository;
        _motoristaRepository = motoristaRepository;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    // Adicionar motorista
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

    // Atualizar motorista
    public async Task<MotoristaDto> UpdateMotorista(int motoristaId, MotoristaDto requestDto, int userId)
    {
        try
        {
            var model = _mapper.Map<Motorista>(requestDto);
            var motorista = await _motoristaRepository.GetMotoristaByIdAsync(motoristaId);
            if (motorista == null) return null;

            model.Id = motorista.Id;
            model.UserId = userId;
            model.DataAlteracao = DateTime.UtcNow;

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

    // Deletar motorista
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

    // Listar todos os motoristas
    public async Task<MotoristaDto[]> GetAllMotoristasAsync(int userId)
    {
        try
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            Motorista[] motoristas;

            if (user.Funcao.Equals(Funcao.Administrador))
                motoristas = await _motoristaRepository.GetAllMotoristasAsync();
            else
                motoristas = await _motoristaRepository.GetAllMotoristasByUserId(userId);

            if (motoristas == null) return null;

            return _mapper.Map<MotoristaDto[]>(motoristas);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    // Buscar motorista por CPF
    public async Task<MotoristaDto> GetMotoristaByCpfAsync(string cpf, int userId)
    {
        try
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) throw new Exception("Usuário não encontrado.");

            var motorista = await _motoristaRepository.GetMotoristaByCpfAsync(cpf);

            // Verifica se o usuário tem permissão para acessar o motorista
            if (motorista == null || (!user.Funcao.Equals(Funcao.Administrador) && motorista.UserId != userId))
                return null;

            return _mapper.Map<MotoristaDto>(motorista);
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao tentar buscar motorista por CPF. Erro: {ex.Message}");
        }
    }

    // Buscar motoristas por nome
    public async Task<MotoristaDto[]> GetAllMotoristasByNomeAsync(string nome, int userId)
    {
        try
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) throw new Exception("Usuário não encontrado.");

            Motorista[] motoristas;

            if (user.Funcao.Equals(Funcao.Administrador))
            {
                motoristas = await _motoristaRepository.GetAllMotoristasByNomeAsync(nome);
            }
            else
            {
                motoristas = await _motoristaRepository.GetAllMotoristasByNomeAndUserIdAsync(nome, userId);
            }

            if (motoristas == null) return null;

            return _mapper.Map<MotoristaDto[]>(motoristas);
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao tentar buscar motoristas por nome. Erro: {ex.Message}");
        }
    }

    // Buscar motoristas por estado e cidade
    public async Task<MotoristaDto[]> GetAllMotoristasByEstadoCidadeAsync(string estado, string cidade, int userId)
    {
        try
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) throw new Exception("Usuário não encontrado.");

            Motorista[] motoristas;

            if (user.Funcao.Equals(Funcao.Administrador))
            {
                motoristas = await _motoristaRepository.GetAllMotoristasByEstadoCidadeAsync(estado, cidade);
            }
            else
            {
                motoristas = await _motoristaRepository.GetAllMotoristasByEstadoCidadeAndUserIdAsync(estado, cidade, userId);
            }

            if (motoristas == null) return null;

            return _mapper.Map<MotoristaDto[]>(motoristas);
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao tentar buscar motoristas por estado e cidade. Erro: {ex.Message}");
        }
    }

    // Buscar motorista por ID
    public async Task<MotoristaDto> GetMotoristaByIdAsync(int motoristaId, int userId)
    {
        try
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) throw new Exception("Usuário não encontrado.");

            var motorista = await _motoristaRepository.GetMotoristaByIdAsync(motoristaId);

            // Verifica se o usuário tem permissão para acessar o motorista
            if (motorista == null || (!user.Funcao.Equals(Funcao.Administrador) && motorista.UserId != userId))
                return null;

            return _mapper.Map<MotoristaDto>(motorista);
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao tentar buscar motorista por ID. Erro: {ex.Message}");
        }
    }
}
