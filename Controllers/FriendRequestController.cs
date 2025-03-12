using Microsoft.AspNetCore.Mvc;
using Social_Media.BAL;
using Social_Media.Helpers;
using Social_Media.Models.DTO;
using static Social_Media.Helpers.Constants;
namespace Social_Media.Controllers
{
    [Route("api/friendsRequest")]
    public class FriendRequestController : ControllerBase
    {
        private readonly IFriendRequestService _friendRequestService;

        public FriendRequestController(IFriendRequestService friendRequestService)
        {
            _friendRequestService = friendRequestService;
        }

        [HttpPost("addFriendRequest")]
        public async Task<IActionResult> addFriendRequest ([FromBody] FriendRequestDTO friendRequestDTO)
        {
            if (friendRequestDTO == null) return BadRequest();
            await _friendRequestService.AddFriendRequestAsync(friendRequestDTO);
            return Ok("Successed !");
        }

        [HttpGet("getAllFriendsRequest")]
        public async Task<IActionResult> getAllFriendsRequest()
        {
            var result = await _friendRequestService.GetAllFriendRequestAsync();
            if(result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("getFriendsRequestByID/{id}")]
        public async Task<IActionResult> getFriendsRequestByID(int id)
        {
            var result = await _friendRequestService.GetFriendRequestByIdAsync(id);
            if(result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("deleteFriendRequestByID/{id}")]
        public async Task<IActionResult> deleteFriendRequest(int id)
        {
            await _friendRequestService.DeleteFriendRequestAsync(id);
            return NoContent();
        }
    }

}
