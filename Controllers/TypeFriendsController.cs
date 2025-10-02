using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Services;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Entities.DTO;

namespace Social_Media.Controllers
{
    [Route("api/typefriends")]
    [ApiController]
    public class TypeFriendsController : ControllerBase
    {
        private readonly ITypeFriendsService _typeFriendsService;

        public TypeFriendsController(ITypeFriendsService typeFriendsService)
        {
            _typeFriendsService = typeFriendsService;
        }

        // Add new type friend
        [HttpPost("addTypeFriend")]
        public async Task<IActionResult> addTypeFriend([FromBody] TypeFriendsDTO typeFriendsDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _typeFriendsService.AddTypeFriendsAsync(typeFriendsDTO);
            return Ok("add success !");
        }

        // Get all type friends
        [HttpGet("getAllTypeFriends")]
        public async Task<IActionResult> getAllTypeFriends()
        {
            var typeFriends = await _typeFriendsService.GetAllTypeFriendsAsync();
            if (typeFriends == null) return NotFound();
            return Ok(typeFriends);
        }

        // Get type friend by ID
        [HttpGet("getTypeFriendsById/{id}")]
        public async Task<IActionResult> getTypeFriendsById(int id)
        {
            var typeFriend = await _typeFriendsService.GetTypeFriendsByIdAsync(id);
            if (typeFriend == null) return NotFound("Type friend not found");
            return Ok(typeFriend);
        }

        // Delete type friend by ID
        [HttpDelete("deleteTypeFriend/{id}")]
        public async Task<IActionResult> deleteTypeFriend(int id)
        {
            var typeFriend = await _typeFriendsService.GetTypeFriendsByIdAsync(id);
            if (typeFriend == null) return NotFound("Type friend not found");
            await _typeFriendsService.DeleteTypeFriendsAsync(id);
            return Ok("Delete type friend success");
        }
         
    }
}
