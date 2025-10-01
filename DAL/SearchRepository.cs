using Social_Media.Models;
using Microsoft.EntityFrameworkCore;

namespace Social_Media.DAL
{
    public class SearchRepository : ISearchRepository
    {
        private readonly AppDbContext _context;
        public SearchRepository(AppDbContext context)
        {
            _context = context;
        }

        //Find user by username || phonenumber || email
        public async Task<IEnumerable<User>> FindUser(string stringData, string CurrentUserIdSearch)
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
    }
}
