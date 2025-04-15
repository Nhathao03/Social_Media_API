using Social_Media.Models;

namespace Social_Media.DAL
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllRole();
        Task<Role> GetRoleById(int id);
        Task AddRole(Role role);
        Task UpdateRole(Role role);
        Task DeleteRole(int id);
    }
}
