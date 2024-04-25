using System.Threading.Tasks;
using LogoAliTem.Domain;

namespace LogoAliTem.Persistence.Interfaces
{
    public interface IVeiculoRepository
    {
        Task<Veiculo[]> GetAllVeiculosAsync();
        Task<Veiculo[]> GetVeiculoByPlacaAsync(string placa);
        Task<Veiculo> GetVeiculoByIdAsync(int veiculoId);
        Task<Veiculo[]> GetVeiculosByMotoristaIdAsync(int motoristaId);
    }
}