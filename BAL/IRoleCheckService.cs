using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.BAL
{
    public interface IRoleCheckService
    {
        Task<IEnumerable<RoleCheck>> GetAllRoleCheckAsync();
        Task<RoleCheck> GetRoleCheckByIdAsync(int id);
        Task AddRoleCheckAsync(RoleCheck roleCheck);
        Task UpdateRoleCheckAsync(RoleCheck roleCheck);
        Task DeleteRoleCheckAsync(int id);
        Task<bool> IsAdminAsync(string userId);
    }
}
