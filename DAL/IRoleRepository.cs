using Social_Media.Models;

namespace Social_Media.DAL
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllRole();
        Task<Role> GetRoleById(string id);
        Task AddRole(Role role);
        Task UpdateRole(Role role);
        Task DeleteRole(int id);
        Task<string> GetRoleIdUser();
    }
}
