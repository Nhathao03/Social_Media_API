using Microsoft.EntityFrameworkCore;
using Social_Media.Models;
using Social_Media.Models.DTO;

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

        public async Task AddUser(User user)
        {
            _context.users.AddAsync(user);
            await _context.SaveChangesAsync();
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

        private string GenerateUserID()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 20)
              .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }

        public async Task RegisterAccount(RegisterDTO model)
        {
            var user = new User
            {
                Id = GenerateUserID(),
                Email = model.Email,
                Password = model.Password, 
                PhoneNumber = model.PhoneNumber,
                Fullname = model.FullName,
                Birth = model.Birth,
            };

            var roleUser = new RoleCheck
            {
                UserID = user.Id,
                RoleID = "1",
            };

            _context.AddAsync(user);
            _context.AddAsync(roleUser);
            await _context.SaveChangesAsync();
        }
    }
}
