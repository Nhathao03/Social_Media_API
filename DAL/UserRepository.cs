using Microsoft.EntityFrameworkCore;
using Social_Media.Models;
using Social_Media.Models.DTO;
using Social_Media.Models.DTO.AccountUser;

namespace Social_Media.DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            return await _context.users.ToListAsync();
        }

        public async Task<User> GetUserById(string id)
        {
            return await _context.users.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.users.FirstOrDefaultAsync(p => p.Email == email);
        }
        public async Task UpdateUser(User user)
        {
            _context.users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(string id)
        {
            var user = await _context.users.FindAsync(id);
            if (user != null)
            {
                _context.users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        //Register account
        public async Task RegisterAccount(User user)
        {
            _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        //Find user by username || phonenumber || email
        public async Task<List<User>> FindUser(string stringData)
        {
            return await _context.users
                .Include(u => u.follower)
                .Include(u => u.following)
                .Where(u =>
                    u.Email == stringData ||
                    u.PhoneNumber.Contains(stringData) ||
                    u.Fullname.Contains(stringData))
                .Select(u => new User
                {
                    Id = u.Id,
                    Fullname = u.Fullname,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Avatar = u.Avatar,
                    follower = u.follower,
                    following = u.following
                })
                .ToListAsync();
        }

        // Check exist email
        public async Task<bool> CheckexistEmail(string email)
        {
            var existEmail =  await _context.users.FirstOrDefaultAsync(u => u.Email == email);
            if (existEmail != null)
            {
                return true; // Email already exists
            }
            return false; // Email does not exist
        }

    }
}
