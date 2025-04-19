using LogoAliTem.Domain;
using System.Threading.Tasks;

namespace LogoAliTem.Persistence.Interfaces
{
    public interface ICalculoEstadiaRepository
    {
        Task<CalculoEstadia> AdicionarAsync(CalculoEstadia entity);
        Task<CalculoEstadia> ObterPorIdAsync(int id);
        Task<int> ContarAsync();
    }
}