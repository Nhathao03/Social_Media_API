using Social_Media.DAL;
using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.BAL
{
    public class RoleService : IRoleService
    {
        private readonly RoleRepository _roleRepository;

        public RoleService(RoleRepository repository)
        {
            _roleRepository = repository;
        }

        public async Task<IEnumerable<Role>> GetAllRoleAsync()
        {
            return await _roleRepository.GetAllRole();
        }

        public async Task<Role> GetRoleByIdAsync(int id)
        {
            return await _roleRepository.GetRoleById(id);
        }

        public async Task AddRoleAsync(Role role)
        {
            await _roleRepository.AddRole(role);
        }

        public async Task UpdateRoleAsync(Role role)
        {
            await _roleRepository.UpdateRole(role);
        }

        public async Task DeleteRoleAsync(int id)
        {
            await _roleRepository.DeleteRole(id);
        } 
    }
}
