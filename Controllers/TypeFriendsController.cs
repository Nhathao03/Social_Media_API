using Microsoft.AspNetCore.Mvc;
using Social_Media.BAL;
using Social_Media.Models;
using Social_Media.Models.DTO;

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

        [HttpPost("addTypeFriend")]
        public async Task<IActionResult> addTypeFriend([FromBody] TypeFriendsDTO typeFriendsDTO)
        {
            if (typeFriendsDTO == null) return BadRequest();
            await _typeFriendsService.AddTypeFriendsAsync(typeFriendsDTO);
            return Ok("add success !");
        }
         
    }
}
