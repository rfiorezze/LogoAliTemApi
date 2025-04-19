using LogoAliTem.Domain;
using System.Threading.Tasks;

namespace LogoAliTem.Persistence.Interfaces
{
    public interface ICertidaoEstadiaRepository
    {
        Task<CertidaoEstadia> AdicionarAsync(CertidaoEstadia entity);
        Task<CertidaoEstadia> ObterPorIdAsync(int id);
        Task<int> ContarAsync();
    }
}