using LogoAliTem.Domain.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogoAliTem.Persistence.Interfaces;
public interface IUserRepository : IBaseRepository
{
    Task<IEnumerable<User>> GetUsersAsync();

    Task<User> GetUserByIdAsync(int id);

    Task<User> GetUserByEmailAsync(string email);
}
