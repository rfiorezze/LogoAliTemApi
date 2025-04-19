using LogoAliTem.Domain;
using LogoAliTem.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LogoAliTem.Persistence
{
    public class CalculoEstadiaRepository : ICalculoEstadiaRepository
    {
        private readonly LogoAliTemContext _context;

        public CalculoEstadiaRepository(LogoAliTemContext context)
        {
            _context = context;
        }

        public async Task<CalculoEstadia> AdicionarAsync(CalculoEstadia entity)
        {
            _context.CalculoEstadia.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<CalculoEstadia> ObterPorIdAsync(int id)
        {
            return await _context.CalculoEstadia
                .Include(c => c.CertidaoEstadia)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<int> ContarAsync()
        {
            return await _context.CalculoEstadia.CountAsync();
        }
    }
}