using LogoAliTem.Domain;
using System.Threading.Tasks;

namespace LogoAliTem.Application.Interfaces
{
    public interface IEstadiaService
    {
        Task<CalculoEstadia> RegistrarCalculoAsync(CalculoEstadia entity);
        Task<CertidaoEstadia> RegistrarCertidaoAsync(CertidaoEstadia entity);
        Task<(int totalCalculos, int totalCertidoes, double taxaConversao)> ObterIndicadoresAsync();
    }
}
