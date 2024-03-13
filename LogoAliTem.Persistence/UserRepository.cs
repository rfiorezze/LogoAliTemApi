using LogoAliTem.Domain.Identity;
using LogoAliTem.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogoAliTem.Persistence
{
    public class UserRepository: BaseRepository, IUserRepository
    {
        private readonly LogoAliTemContext _context;

        public UserRepository(LogoAliTemContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(user=> user.UserName.Equals(username.ToLower()));
        }

    }
}