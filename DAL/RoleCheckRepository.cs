using Microsoft.EntityFrameworkCore;
using Social_Media.Models;

namespace Social_Media.DAL
{
    public class RoleCheckRepository : IRoleCheckRepository
    {
        private readonly AppDbContext _context;

        public RoleCheckRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoleCheck>> GetAllRoleCheck()
        {
            return await _context.rolesCheck.ToListAsync();
        }

        public async Task<RoleCheck> GetRoleCheckById(int id)
        {
            return await _context.rolesCheck.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddRoleCheck(RoleCheck roleCheck)
        {
            _context.rolesCheck.AddAsync(roleCheck);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRoleCheck(RoleCheck roleCheck)
        {
            _context.rolesCheck.Update(roleCheck);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoleCheck(int id)
        {
            var roleCheck = await _context.rolesCheck.FindAsync(id);
            if (roleCheck != null)
            {
                _context.rolesCheck.Remove(roleCheck);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsAdminAsync(string userId)
        {
            var adminRole = await _context.roles
                .FirstOrDefaultAsync(r => r.Name.Equals("Admin"));

            if (adminRole == null) return false; // No "Admin" role found

            // Check if user has any role assigned
            var userRoleCheck = await _context.rolesCheck
                .FirstOrDefaultAsync(rc => rc.UserID == userId);

            if (userRoleCheck == null || string.IsNullOrEmpty(userRoleCheck.RoleID))
                return false; // User has no role assigned, or role is NULL

            return userRoleCheck.RoleID == adminRole.Id.ToString();
        }

    }
}
