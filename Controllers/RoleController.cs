using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Services;
using SocialMedia.Core.Entities.DTO.Role;

namespace Social_Media.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        //Get all role
        [HttpGet("GetAllRole")]
        public async Task<IActionResult> GetAllRole()
        {
            var response = await _roleService.GetAllRoleAsync();
            return Ok(response);
        }
        //Get roleCheck by ID
        [HttpGet("GetRoleById/{id}")]
        public async Task<IActionResult> GetRoleById(string id)
        {
            var response = await _roleService.GetRoleByIdAsync(id);
            return Ok(response);
        }

        //Add role
        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole(string RoleName)
        {
            if(RoleName == null) return BadRequest("RoleName is required.");
            await _roleService.AddRoleAsync(RoleName);
            return Ok("Role added successfully.");
        }

        // Get role id of user
        [HttpGet("GetRoleIdOfUser")]
        public async Task<string> GetRoleIdOfUser()
        {
            var roleId = await _roleService.GetRoleIdUserAsync();;
            return roleId;
        }
    }
}
