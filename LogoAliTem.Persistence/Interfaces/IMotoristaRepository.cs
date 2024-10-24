using System.Threading.Tasks;
using LogoAliTem.Domain;

namespace LogoAliTem.Persistence.Interfaces;
public interface IMotoristaRepository
{
    Task<Motorista[]> GetAllMotoristasAsync();
    Task<Motorista[]> GetAllMotoristasByUserId(int userId);
    Task<Motorista[]> GetAllMotoristasByNomeAsync(string nome);
    Task<Motorista[]> GetAllMotoristasByNomeAndUserIdAsync(string nome, int userId);
    Task<Motorista[]> GetAllMotoristasByEstadoCidadeAsync(string estado, string cidade);
    Task<Motorista[]> GetAllMotoristasByEstadoCidadeAndUserIdAsync(string estado, string cidade, int userId);
    Task<Motorista> GetMotoristaByIdAsync(int motoristaId);
    Task<Motorista> GetMotoristaByCpfAsync(string cpf);
    Task<Motorista> GetMotoristaByCpfAndUserIdAsync(string cpf, int userId);
}
