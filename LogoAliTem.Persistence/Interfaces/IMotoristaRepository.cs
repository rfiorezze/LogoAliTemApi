using System.Threading.Tasks;
using LogoAliTem.Domain;

namespace LogoAliTem.Persistence.Interfaces;
public interface IMotoristaRepository
{
    Task<Motorista[]> GetAllMotoristasAsync();
    Task<Motorista[]> GetAllMotoristasByNomeAsync(string nome);
    Task<Motorista> GetMotoristaByIdAsync(int motoristaId);
}