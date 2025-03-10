using Social_Media.DAL;
using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.BAL
{
    public class RoleCheckService : IRoleCheckService
    {
        private readonly RoleCheckRepository _roleCheckRepository;

        public RoleCheckService(RoleCheckRepository repository)
        {
            _roleCheckRepository = repository;
        }

        public async Task<IEnumerable<RoleCheck>> GetAllRoleCheckAsync()
        {
            return await _roleCheckRepository.GetAllRoleCheck();
        }

        public async Task<RoleCheck> GetRoleCheckByIdAsync(int id)
        {
            return await _roleCheckRepository.GetRoleCheckById(id);
        }

        public async Task AddRoleCheckAsync(RoleCheck roleCheck)
        {
            await _roleCheckRepository.AddRoleCheck(roleCheck);
        }

        public async Task UpdateRoleCheckAsync(RoleCheck roleCheck)
        {
            await _roleCheckRepository.UpdateRoleCheck(roleCheck);
        }

        public async Task DeleteRoleCheckAsync(int id)
        {
            await _roleCheckRepository.DeleteRoleCheck(id);
        }

        public async Task<bool> IsAdminAsync(string userId)
        {
            return await _roleCheckRepository.IsAdminAsync(userId);
        }
    }
}
