using Microsoft.EntityFrameworkCore;
using Social_Media.Models;
using Social_Media.Models.DTO.AccountUser;
using System.Text;

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
        public async Task<User> RegisterAccount(User user)
        {
            _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }        

        // Check exist email
        public async Task<bool> IsEmailExistsAsync(string email)
        {
            var existEmail =  await _context.users.FirstOrDefaultAsync(u => u.Email == email);
            if (existEmail != null)
            {
                return true; // Email already exists
            }
            return false; // Email does not exist
        }
        // Check exist phone number
        public async Task<bool> IsPhoneExistsAsync(string phoneNumber)
        {
            var existPhone = await _context.users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
            if (existPhone != null)
            {
                return true; // Phone number already exists
            }
            return false; // Phone number does not exist
        }

        // Check password sign in
        public async Task<bool> CheckPasswordSignInAsync(LoginDTO model, string password)
        {
            var user = await _context.users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return true; // Password is correct
            }
            return false; // Password is incorrect
        }

    }
}
