using Social_Media.Models;
using Social_Media.Models.DTO;
using Social_Media.Models.DTO.Role;

namespace Social_Media.BAL
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAllRoleAsync();
        Task<Role> GetRoleByIdAsync(string id);
        Task AddRoleAsync(string RoleName);
        Task UpdateRoleAsync(RoleDTO modelDTO);
        Task DeleteRoleAsync(int id);
        Task<string> GetRoleIdUserAsync();

    }
}
