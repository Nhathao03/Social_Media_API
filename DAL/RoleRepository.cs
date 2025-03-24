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
            return await _context.roles.ToListAsync();
        }

        public async Task<Role> GetRoleById(int id)
        {
            return await _context.roles.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddRole(Role role)
        {
            _context.roles.AddAsync(role);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRole(Role role)
        {
            _context.roles.Update(role);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRole(int id)
        {
            var role = await _context.roles.FindAsync(id);
            if (role != null)
            {
                _context.roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }
    }
}
