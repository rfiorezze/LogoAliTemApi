using LogoAliTem.Domain;
using LogoAliTem.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LogoAliTem.Persistence
{
    public class CertidaoEstadiaRepository : ICertidaoEstadiaRepository
    {
        private readonly LogoAliTemContext _context;

        public CertidaoEstadiaRepository(LogoAliTemContext context)
        {
            _context = context;
        }

        public async Task<CertidaoEstadia> AdicionarAsync(CertidaoEstadia entity)
        {
            _context.CertidaoEstadia.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<CertidaoEstadia> ObterPorIdAsync(int id)
        {
            return await _context.CertidaoEstadia
                .Include(e => e.CalculoEstadia)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task<int> ContarAsync()
        {
            return await _context.CertidaoEstadia.CountAsync();
        }
    }
}