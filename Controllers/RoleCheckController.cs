using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Services;
using SocialMedia.Core.Entities.DTO.RoleCheck;

namespace Social_Media.Controllers
{
    [Route("api/roleCheck")]
    [ApiController]
    public class RoleCheckController : ControllerBase
    {
        private readonly IRoleCheckService _roleCheckService;

        public RoleCheckController(IRoleCheckService roleCheckService)
        {
            _roleCheckService = roleCheckService;
        }

        // Get all role checks
        [HttpGet("GetAllRoleCheck")]
        public async Task<IActionResult> GetAllRoleCheck()
        {
            var roleChecks = await _roleCheckService.GetAllRoleCheckAsync();
            return Ok(roleChecks);
        }

        // Get role check by ID
        [HttpGet("GetRoleCheckById/{id}")]
        public async Task<IActionResult> GetRoleCheckById(int id)
        {
            var roleCheck = await _roleCheckService.GetRoleCheckByIdAsync(id);
            if (roleCheck == null) return NotFound();
            return Ok(roleCheck);
        }

        // Add new role check
        [HttpPost("AddRoleCheck")]
        public async Task<IActionResult> AddRoleCheck([FromBody] RoleCheckDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _roleCheckService.AddRoleCheckAsync(model);
            return Ok("Role check added successfully.");
        }
        // Update role check
        [HttpPut("UpdateRoleCheck")]
        public async Task<IActionResult> UpdateRoleCheck([FromBody] RoleCheckDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _roleCheckService.UpdateRoleCheckAsync(model);
            return Ok("Role check updated successfully.");
        }

        // Delete role check by ID
        [HttpDelete("DeleteRoleCheck/{id}")]
        public async Task<IActionResult> DeleteRoleCheck(int id)
        {
            await _roleCheckService.DeleteRoleCheckAsync(id);
            return NoContent();
        }

        // Delete role check by User ID
        [HttpDelete("DeleteRoleCheckByUserId/{userId}")]
        public async Task<IActionResult> DeleteRoleCheckByUserId(string userId)
        {
            //check if userId is null or empty 
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("UserId is required.");
            }
            await _roleCheckService.DeleteRoleCheckByUserIdAsync(userId);
            return NoContent();
        }

        // Check if user is admin
        [HttpGet("IsAdmin/{userId}")]
        public async Task<IActionResult> IsAdmin(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("UserId is required.");
            }
            var isAdmin = await _roleCheckService.IsAdminAsync(userId);
            return Ok(isAdmin);
        }

    }
}
