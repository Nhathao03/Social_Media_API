using Social_Media.Models;
using Social_Media.Models.DTO;
using Social_Media.Models.DTO.RoleCheck;

namespace Social_Media.BAL
{
    public interface IRoleCheckService
    {
        Task<IEnumerable<RoleCheck>> GetAllRoleCheckAsync();
        Task<RoleCheck> GetRoleCheckByIdAsync(int id);
        Task AddRoleCheckAsync(RoleCheckDTO roleCheck);
        Task UpdateRoleCheckAsync(RoleCheckDTO roleCheck);
        Task DeleteRoleCheckAsync(int id);
        Task DeleteRoleCheckByUserIdAsync(string userId);
        Task<bool> IsAdminAsync(string userId);    }
}
