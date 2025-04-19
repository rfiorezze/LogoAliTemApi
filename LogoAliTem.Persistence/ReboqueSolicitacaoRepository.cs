using LogoAliTem.Domain;
using LogoAliTem.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LogoAliTem.Persistence
{
    public class ReboqueSolicitacaoRepository : BaseRepository, IReboqueSolicitacaoRepository
    {
        private readonly LogoAliTemContext _context;

        public ReboqueSolicitacaoRepository(LogoAliTemContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> GetTotalSolicitacoesAsync()
        {
            return await _context.Reboques.CountAsync();
        }

        public async Task<int> GetTotalPorTipoVeiculoAsync(string tipoVeiculo)
        {
            return await _context.Reboques
                .CountAsync(r => r.TipoVeiculo.ToLower() == tipoVeiculo.ToLower());
        }

        public async Task<int> GetTotalPorPeriodoAsync(DateTime inicio, DateTime fim)
        {
            return await _context.Reboques
                .CountAsync(r => r.DataSolicitacao >= inicio && r.DataSolicitacao <= fim);
        }
    }
}
