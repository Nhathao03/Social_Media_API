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

        //Render userID
        private string GenerateUserID()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 20)
              .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }

        //Register account
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
                gender = model.gender,
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

        //Update personal information
        public async Task UpdatePersonalInformation (PersonalInformationDTO personalInformationDTO)
        {
            if (personalInformationDTO == null) return;
            var user = await _context.users.FindAsync(personalInformationDTO.userID);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {personalInformationDTO.userID} not found.");
            }
            user.Fullname = personalInformationDTO.fullname;
            user.Birth = personalInformationDTO.Birth;
            user.Avatar = personalInformationDTO.avatar;
            
        }
    }
}
