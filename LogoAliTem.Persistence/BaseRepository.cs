using LogoAliTem.Persistence.Interfaces;
using System.Threading.Tasks;

namespace LogoAliTem.Persistence;
public class BaseRepository : IBaseRepository
{
    public LogoAliTemContext _context { get; set; }
    public BaseRepository(LogoAliTemContext context)
    {
        _context = context;
    }
    public void Add<T>(T entity) where T : class
    {
        _context.AddAsync(entity);
    }

    public void Delete<T>(T entity) where T : class
    {
        _context.Remove(entity);
    }

    public void DeleteRange<T>(T[] entities) where T : class
    {
        _context.RemoveRange(entities);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync()) > 0;
    }

    public void Update<T>(T entity) where T : class
    {
        _context.Update(entity);
    }
}