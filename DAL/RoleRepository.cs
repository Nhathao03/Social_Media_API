using Microsoft.EntityFrameworkCore;
using Social_Media.Models;

namespace Social_Media.DAL
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAllRole()
        {
            return await _context.role.ToListAsync();
        }

        public async Task<Role> GetRoleById(string id)
        {
            return await _context.role.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddRole(Role role)
        {
            _context.role.AddAsync(role);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRole(Role role)
        {
            _context.role.Update(role);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRole(int id)
        {
            var role = await _context.role.FindAsync(id);
            if (role != null)
            {
                _context.role.Remove(role);
                await _context.SaveChangesAsync();
            }
        }

        // Get role Id by name
        public async Task<string> GetRoleIdUser()
        {
            var user = await _context.role.FirstOrDefaultAsync(u => u.Name == "User");
            if (user == null) return null;

            return user.Id.ToString();
        }
    }
}
