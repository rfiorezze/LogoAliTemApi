using LogoAliTem.Domain;
using LogoAliTem.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LogoAliTem.Persistence;
public class MotoristaRepository : IMotoristaRepository
{
    private readonly LogoAliTemContext _context;

    public MotoristaRepository(LogoAliTemContext context)
    {
        _context = context;
    }

    // Retorna todos os motoristas
    public async Task<Motorista[]> GetAllMotoristasAsync()
    {
        IQueryable<Motorista> query = _context.Motoristas;

        query = query.AsNoTracking().OrderBy(m => m.Id);

        return await query.ToArrayAsync();
    }

    // Retorna motoristas cadastrados por um usuário específico
    public async Task<Motorista[]> GetAllMotoristasByUserId(int userId)
    {
        IQueryable<Motorista> query = _context.Motoristas;

        query = query
            .AsNoTracking()
            .OrderBy(m => m.Id)
            .Where(m => m.UserId == userId);

        return await query.ToArrayAsync();
    }

    // Retorna motoristas por estado e cidade
    public async Task<Motorista[]> GetAllMotoristasByEstadoCidadeAsync(string estado, string cidade)
    {
        IQueryable<Motorista> query = _context.Motoristas;

        query = query
            .AsNoTracking()
            .OrderBy(m => m.Id)
            .Where(m => m.Estado.Equals(estado) && m.Cidade.Equals(cidade));

        return await query.ToArrayAsync();
    }

    // Retorna motoristas por estado e cidade para um usuário específico
    public async Task<Motorista[]> GetAllMotoristasByEstadoCidadeAndUserIdAsync(string estado, string cidade, int userId)
    {
        IQueryable<Motorista> query = _context.Motoristas;

        query = query
            .AsNoTracking()
            .OrderBy(m => m.Id)
            .Where(m => m.Estado.Equals(estado) && m.Cidade.Equals(cidade) && m.UserId == userId);

        return await query.ToArrayAsync();
    }

    // Retorna motoristas por nome
    public async Task<Motorista[]> GetAllMotoristasByNomeAsync(string nome)
    {
        IQueryable<Motorista> query = _context.Motoristas;

        query = query
            .AsNoTracking()
            .OrderBy(m => m.Id)
            .Where(m => m.Nome.ToLower().Contains(nome.ToLower()));

        return await query.ToArrayAsync();
    }

    // Retorna motoristas por nome e filtrado por usuário específico
    public async Task<Motorista[]> GetAllMotoristasByNomeAndUserIdAsync(string nome, int userId)
    {
        IQueryable<Motorista> query = _context.Motoristas;

        query = query
            .AsNoTracking()
            .OrderBy(m => m.Id)
            .Where(m => m.Nome.ToLower().Contains(nome.ToLower()) && m.UserId == userId);

        return await query.ToArrayAsync();
    }

    // Retorna motorista por CPF
    public async Task<Motorista> GetMotoristaByCpfAsync(string cpf)
    {
        IQueryable<Motorista> query = _context.Motoristas;

        query = query
            .AsNoTracking()
            .OrderBy(m => m.Id)
            .Where(m => m.Cpf == cpf);

        return await query.FirstOrDefaultAsync();
    }

    // Retorna motorista por CPF e filtrado por usuário específico
    public async Task<Motorista> GetMotoristaByCpfAndUserIdAsync(string cpf, int userId)
    {
        IQueryable<Motorista> query = _context.Motoristas;

        query = query
            .AsNoTracking()
            .OrderBy(m => m.Id)
            .Where(m => m.Cpf == cpf && m.UserId == userId);

        return await query.FirstOrDefaultAsync();
    }

    // Retorna motorista por ID
    public async Task<Motorista> GetMotoristaByIdAsync(int motoristaId)
    {
        IQueryable<Motorista> query = _context.Motoristas;

        query = query
            .AsNoTracking()
            .OrderBy(m => m.Id)
            .Where(m => m.Id == motoristaId);

        return await query.FirstOrDefaultAsync();
    }
    public async Task<int> ContarMotoristasAsync()
    {
        return await _context.Motoristas.CountAsync();
    }
}
