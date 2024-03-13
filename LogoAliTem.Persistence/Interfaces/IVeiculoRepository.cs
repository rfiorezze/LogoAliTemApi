using System.Threading.Tasks;
using LogoAliTem.Domain;

namespace LogoAliTem.Persistence.Interfaces
{
    public interface IVeiculoRepository
    {
        Task<Veiculo[]> GetAllVeiculosAsync();
        Task<Veiculo[]> GetAllVeiculosByPlacaAsync(string placa);
        Task<Veiculo> GetVeiculoByIdAsync(int veiculoId);
    }
}