using Social_Media.Models;

namespace Social_Media.DAL
{
    public interface IRoleCheckRepository
    {
        Task<IEnumerable<RoleCheck>> GetAllRoleCheck();
        Task<RoleCheck> GetRoleCheckById(int id);
        Task AddRoleCheck(RoleCheck roleCheck);
        Task UpdateRoleCheck(RoleCheck roleCheck);
        Task DeleteRoleCheck(int id);
        Task DeleteRoleCheckByUserId(string userId);
        Task<bool> IsAdminAsync(string userId);
    }
}
