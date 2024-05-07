using LogoAliTem.Application.Dtos;
using System.Threading.Tasks;

namespace LogoAliTem.Application.Interfaces;

public interface IMotoristaService
{
    Task<MotoristaDto> AddMotorista(MotoristaDto requestDto, int userId);
    Task<MotoristaDto> UpdateMotorista(int motoristaId, MotoristaDto requestDto, int userId);
    Task<bool> DeleteMotorista(int motoristaId);
    Task<MotoristaDto[]> GetAllMotoristasAsync();
    Task<MotoristaDto[]> GetAllMotoristasByNomeAsync(string nome);
    Task<MotoristaDto> GetMotoristaByCpfAsync(string cpf);
    Task<MotoristaDto> GetMotoristaByIdAsync(int motoristaId);
}