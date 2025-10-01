using Microsoft.EntityFrameworkCore;
using Social_Media.Models;

namespace Social_Media.DAL
{
    public class UserLoginRepository : IUserLoginRepository
    {
        private readonly AppDbContext _context;

        public UserLoginRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserLogins>> GetAllUserLogin()
        {
            return await _context.usersLogins.ToListAsync();
        }

        public async Task<UserLogins> GetUserLoginById(int id)
        {
            return await _context.usersLogins.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateUserLogin(UserLogins userLogins)
        {
            _context.usersLogins.Update(userLogins);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserLogin(int id)
        {
            var UserLogin = await _context.usersLogins.FindAsync(id);
            if (UserLogin != null)
            {
                _context.usersLogins.Remove(UserLogin);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddUserLogin(UserLogins userLogins)
        {
            _context.AddAsync(userLogins);
            await _context.SaveChangesAsync();
        }

    }
}
