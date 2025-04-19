using LogoAliTem.Domain;
using System.Threading.Tasks;

namespace LogoAliTem.Persistence.Interfaces
{
    public interface ICalculoReboqueRepository
    {
        Task RegistrarCalculoAsync(CalculoReboque calculo);
        Task<int> GetTotalCalculosAsync();
    }
}
