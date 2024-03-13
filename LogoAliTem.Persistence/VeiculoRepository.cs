using LogoAliTem.Domain;
using LogoAliTem.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LogoAliTem.Persistence;

public class VeiculoRepository : IVeiculoRepository
{
    public readonly LogoAliTemContext _context;

    public VeiculoRepository(LogoAliTemContext context)
    {
        _context = context;
    }

    public async Task<Veiculo[]> GetAllVeiculosAsync()
    {
        IQueryable<Veiculo> query = _context.Veiculos
            .Include(v => v.Motorista);

        query = query.OrderBy(v => v.Id);

        return await query.ToArrayAsync();
    }

    public async Task<Veiculo[]> GetAllVeiculosByPlacaAsync(string placa)
    {
        IQueryable<Veiculo> query = _context.Veiculos
            .Include(v => v.Motorista);

        query = query.OrderBy(v => v.Id).Where(v => v.Placa.ToLower().Contains(placa.ToLower()));

        return await query.ToArrayAsync();
    }

    public async Task<Veiculo> GetVeiculoByIdAsync(int veiculoId)
    {
        IQueryable<Veiculo> query = _context.Veiculos;

        query = query
            .OrderBy(m => m.Id)
            .Where(m => m.Id == veiculoId);

        return await query.FirstOrDefaultAsync();
    }
}