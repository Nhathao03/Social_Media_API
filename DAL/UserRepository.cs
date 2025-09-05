using Microsoft.EntityFrameworkCore;
using Social_Media.Models;
using Social_Media.Models.DTO;
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
        public async Task RegisterAccount(User user)
        {
            _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        //Find user by username || phonenumber || email
        public async Task<List<User>> FindUser(string stringData, string CurrentUserIdSearch)
        {
            if (string.IsNullOrWhiteSpace(stringData))
                return new List<User>();

            IQueryable<User> query = _context.users
                .Include(u => u.follower)
                .Include(u => u.following);

            if (stringData.Contains("@"))
            {
                query = query.Where(u => u.Email.ToLower() == stringData.ToLower());
            }
            else if (stringData.All(char.IsDigit))
            {
                var normalizedPhone = new string(stringData.Where(char.IsDigit).ToArray());
                query = query.Where(u => u.PhoneNumber == normalizedPhone);
            }
            else
            {
                var allMatches = await query
                    .Select(u => new
                    {
                        u.Id,
                        u.Fullname,
                        u.NormalizeUsername,
                        u.Email,
                        u.PhoneNumber,
                        u.Avatar,
                        u.follower,
                        u.following
                    })
                    .ToListAsync();

                var result = allMatches
                    .Where(u => u.NormalizeUsername.Contains(stringData) && u.Id != CurrentUserIdSearch)
                    .OrderByDescending(u => u.follower.Count + u.following.Count)
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
                    .ToList();

                return result;
            }

            var dbResult = await query
                .OrderByDescending(u => u.follower.Count + u.following.Count)
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

            return dbResult;
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

    }
}
