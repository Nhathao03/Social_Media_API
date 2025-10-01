using Social_Media.DAL;
using Social_Media.Models;
using Social_Media.Models.DTO;
using Social_Media.Models.DTO.Role;

namespace Social_Media.BAL
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository repository)
        {
            _roleRepository = repository;
        }

        public async Task<IEnumerable<Role>> GetAllRoleAsync()
        {
            return await _roleRepository.GetAllRole();
        }

        public async Task<Role> GetRoleByIdAsync(string id)
        {
            return await _roleRepository.GetRoleById(id);
        }

        public async Task AddRoleAsync(string RoleName)
        {
            var modelData = new Role
            {
                // call to function generate roleId to create new roleId
                Id = GenerateRoleId(),
                Name = RoleName
            };
            await _roleRepository.AddRole(modelData);
        }

        // Generate roleId 
        private string GenerateRoleId()
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 8)
                             .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task UpdateRoleAsync(RoleDTO modelDTO)
        {
            var existingRole = await _roleRepository.GetRoleById(modelDTO.Id);
            if(existingRole == null)
            {
                throw new KeyNotFoundException($"Role with Id {modelDTO.Id} not exits.");
            }

            existingRole.Name = modelDTO.Name;

            await _roleRepository.UpdateRole(existingRole);
        }

        public async Task DeleteRoleAsync(int id)
        {
            await _roleRepository.DeleteRole(id);
        }
        public async Task<string> GetRoleIdUserAsync()
        {
            return await _roleRepository.GetRoleIdUser();
        }
    }
}
