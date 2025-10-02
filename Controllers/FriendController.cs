using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Services;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Entities.DTO;

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

        //Add a friend by ID
        [HttpPost("addFriend/{id}")]
        public async Task<IActionResult> addFriend(int id)
        {             
            await _friendsService.AddFriendsAsync(id);
            return Ok("Add friend success. ");
        }

        //Get all friends by user ID
        [HttpGet("getAllFriendByUserID/{userID}")]
        public async Task<IActionResult> getAllFriendsByUserID(string userID)
        {
            var response = await _friendsService.GetFriendsByUserIDAsync(userID);
            if (response == null) return NotFound();
            return Ok(response);
        }

        //Get friend recently added by ID
        [HttpGet("getFriendRecentlyAdded/{userId}")]
        public async Task<IActionResult> getFriendRecentlyAdded(string userID)
        {
            var response = await _friendsService.getFriendRecentlyAdded(userID); 
            if (response == null) return NotFound();
            return Ok(response);
        }

        //Get friend of each user by user ID
        [HttpGet("getFriendOfEachUser/{userId}")]
        public async Task<IActionResult> getFriendOfEachUser(string userId)
        {
            var response = await _friendsService.getFriendOfEachUser(userId);
            if (response == null) return NotFound();
            return Ok(response);
        }

        //Get friend base on home town by user ID
        [HttpGet("GetFriendBaseOnHomeTown/{userId}")]
        public async Task<IActionResult> GetFriendBaseOnHomeTown(string userId)
        {
            var response = await _friendsService.GetFriendBaseOnHomeTown(userId);
            if (response == null) return NotFound();
            return Ok(response);
        }
    }
}
