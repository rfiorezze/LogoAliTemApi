using LogoAliTem.Domain;
using LogoAliTem.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LogoAliTem.Persistence;
public class MotoristaRespository : IMotoristaRepository
{
    public readonly LogoAliTemContext _context;

    public MotoristaRespository(LogoAliTemContext context)
    {
        _context = context;
    }
    public async Task<Motorista[]> GetAllMotoristasAsync()
    {
        IQueryable<Motorista> query = _context.Motoristas;

        query = query.AsNoTracking().OrderBy(m => m.Id);

        return await query.ToArrayAsync();
    }

    public async Task<Motorista[]> GetAllMotoristasByNomeAsync(string nome)
    {
        IQueryable<Motorista> query = _context.Motoristas;

        query = query
            .AsNoTracking()
            .OrderBy(m => m.Id)
            .Where(m => m.Nome.ToLower().Contains(nome.ToLower()));

        return await query.ToArrayAsync();
    }

    public async Task<Motorista> GetMotoristaByCpfAsync(string cpf)
    {
        IQueryable<Motorista> query = _context.Motoristas;

        query = query
            .AsNoTracking()
            .OrderBy(m => m.Id)
            .Where(m => m.Cpf == cpf);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<Motorista> GetMotoristaByIdAsync(int motoristaId)
    {
        IQueryable<Motorista> query = _context.Motoristas;

        query = query
            .AsNoTracking()
            .OrderBy(m => m.Id)
            .Where(m => m.Id == motoristaId);

        return await query.FirstOrDefaultAsync();
    }
}
