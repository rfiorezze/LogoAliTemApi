using System.Threading.Tasks;
using LogoAliTem.Domain;

namespace LogoAliTem.Persistence.Interfaces;
public interface IMotoristaRepository
{
    Task<Motorista[]> GetAllMotoristasAsync();
    Task<Motorista[]> GetAllMotoristasByNomeAsync(string nome);
    Task<Motorista[]> GetAllMotoristasByEstadoCidadeAsync(string estado, string cidade);
    Task<Motorista> GetMotoristaByIdAsync(int motoristaId);
    Task<Motorista> GetMotoristaByCpfAsync(string cpf);
}