using LogoAliTem.Domain;
using System;
using System.Threading.Tasks;

namespace LogoAliTem.Persistence.Interfaces
{
    public interface IReboqueSolicitacaoRepository
    {
        Task<int> GetTotalSolicitacoesAsync();
        Task<int> GetTotalPorTipoVeiculoAsync(string tipoVeiculo);
        Task<int> GetTotalPorPeriodoAsync(DateTime inicio, DateTime fim);
    }
}
