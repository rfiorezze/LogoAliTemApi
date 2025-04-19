using LogoAliTem.Domain;
using LogoAliTem.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LogoAliTem.Persistence
{
    public class CalculoReboqueRepository : BaseRepository, ICalculoReboqueRepository
    {
        private readonly LogoAliTemContext _context;

        public CalculoReboqueRepository(LogoAliTemContext context) : base(context)
        {
            _context = context;
        }

        public async Task RegistrarCalculoAsync(CalculoReboque calculo)
        {
            await _context.CalculoReboque.AddAsync(calculo);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetTotalCalculosAsync()
        {
            return await _context.CalculoReboque.CountAsync();
        }
    }
}
