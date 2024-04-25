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
        IQueryable<Veiculo> query = _context.Veiculos;

        query = query.AsNoTracking().OrderBy(m => m.Id);

        return await query.ToArrayAsync();
    }

    public async Task<Veiculo[]> GetVeiculoByPlacaAsync(string placa)
    {
        IQueryable<Veiculo> query = _context.Veiculos;

        query = query
            .OrderBy(m => m.Id)
            .Where(m => m.Placa == placa)
            .AsNoTracking();

        return await query.ToArrayAsync();
    }

    public async Task<Veiculo> GetVeiculoByIdAsync(int veiculoId)
    {
        IQueryable<Veiculo> query = _context.Veiculos;

        query = query
            .OrderBy(m => m.Id)
            .Where(m => m.Id == veiculoId)
            .AsNoTracking();

        return await query.FirstOrDefaultAsync();
    }

    public async Task<Veiculo[]> GetVeiculosByMotoristaIdAsync(int motoristaId)
    {
        IQueryable<Veiculo> query = _context.Veiculos;

        query = query
            .OrderBy(m => m.Id)
            .Where(m => m.MotoristaId == motoristaId)
            .AsNoTracking();

        return await query.ToArrayAsync();
    }
}