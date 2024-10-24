using LogoAliTem.Application.Dtos;
using System.Threading.Tasks;

namespace LogoAliTem.Application.Interfaces;
public interface IMotoristaService
{
    Task<MotoristaDto> AddMotorista(MotoristaDto requestDto, int userId);
    Task<MotoristaDto> UpdateMotorista(int motoristaId, MotoristaDto requestDto, int userId);
    Task<bool> DeleteMotorista(int motoristaId);
    Task<MotoristaDto[]> GetAllMotoristasAsync(int userId);
    Task<MotoristaDto[]> GetAllMotoristasByNomeAsync(string nome, int userId);
    Task<MotoristaDto[]> GetAllMotoristasByEstadoCidadeAsync(string estado, string cidade, int userId);
    Task<MotoristaDto> GetMotoristaByCpfAsync(string cpf, int userId);
    Task<MotoristaDto> GetMotoristaByIdAsync(int motoristaId, int userId);
}
