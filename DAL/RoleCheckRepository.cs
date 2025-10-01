using Microsoft.EntityFrameworkCore;
using Social_Media.Models;

namespace Social_Media.DAL
{
    public class RoleCheckRepository : IRoleCheckRepository
    {
        private readonly AppDbContext _context;
        private readonly IRoleRepository _roleRepository;

        public RoleCheckRepository(AppDbContext context,
            IRoleRepository roleRepository)
        {
            _context = context;
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<RoleCheck>> GetAllRoleCheck()
        {
            return await _context.roleChecks.ToListAsync();
        }

        public async Task<RoleCheck> GetRoleCheckById(int id)
        {
            return await _context.roleChecks.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddRoleCheck(RoleCheck roleCheck)
        {
            _context.roleChecks.AddAsync(roleCheck);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRoleCheck(RoleCheck roleCheck)
        {
            _context.roleChecks.Update(roleCheck);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoleCheck(int id)
        {
            var roleCheck = await _context.roleChecks.FindAsync(id);
            if (roleCheck != null)
            {
                _context.roleChecks.Remove(roleCheck);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteRoleCheckByUserId(string userId)
        {
            var user = _context.roleChecks.FirstOrDefault(u => u.UserID == userId);
            if (user != null)
            {
                _context.roleChecks.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsAdminAsync(string userId)
        {
            var adminRole = await _context.role
                .FirstOrDefaultAsync(r => r.Name.Equals("Admin"));

            if (adminRole == null) return false; // No "Admin" role found

            // Check if user has any role assigned
            var userRoleCheck = await _context.roleChecks
                .FirstOrDefaultAsync(rc => rc.UserID == userId);

            if (userRoleCheck == null || string.IsNullOrEmpty(userRoleCheck.RoleID))
                return false; // User has no role assigned, or role is NULL

            return userRoleCheck.RoleID == adminRole.Id.ToString();
        }
    }
}
