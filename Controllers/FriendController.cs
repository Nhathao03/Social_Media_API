using Microsoft.AspNetCore.Mvc;
using Social_Media.BAL;
using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.Controllers
{
    [Route("api/friend")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private readonly IFriendsService _friendsService;
        public FriendController(IFriendsService friendsService)
        {
            _friendsService = friendsService;
        }


        [HttpPost("addFriend/{id}")]
        public async Task<IActionResult> addFriend(int id)
        {             
            await _friendsService.AddFriendsAsync(id);
            return Ok("Add friend success. ");
        }

        [HttpGet("getAllFriendByUserID/{userID}")]
        public async Task<IActionResult> getAllFriendsByUserID(string userID)
        {
            var response = await _friendsService.GetFriendsByUserIDAsync(userID);
            if (response == null) return NotFound();
            return Ok(response);
        }

        [HttpGet("getFriendRecentlyAdded")]
        public async Task<IActionResult> getFriendRecentlyAdded(string userID)
        {
            var response = await _friendsService.getFriendRecentlyAdded(userID); 
            if (response == null) return NotFound();
            return Ok(response);
        }
    }
}
