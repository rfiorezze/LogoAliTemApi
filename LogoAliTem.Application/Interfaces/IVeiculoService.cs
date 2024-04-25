using LogoAliTem.Domain;
using System.Threading.Tasks;

namespace LogoAliTem.Application.Interfaces;

public interface IVeiculoService
{
    Task<VeiculoDto> AddVeiculo(VeiculoDto requestDto, int userId);
    Task<VeiculoDto> UpdateVeiculo(int VeiculoId, VeiculoDto requestDto, int userId);
    Task<bool> DeleteVeiculo(int VeiculoId);
    Task<VeiculoDto[]> GetAllVeiculosAsync();
    Task<VeiculoDto[]> GetVeiculoByPlacaAsync(string placa);
    Task<VeiculoDto> GetVeiculoByIdAsync(int VeiculoId);
    Task<VeiculoDto[]> GetVeiculosByMotoristaIdAsync(int motoristaId);
}