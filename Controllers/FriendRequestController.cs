using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Services;
using Social_Media.Helpers;
using SocialMedia.Core.Entities.DTO;
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

        //Send or cancel friend request
        [HttpPost("addFriendRequest")]
        public async Task<IActionResult> addFriendRequest ([FromBody] FriendRequestDTO friendRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _friendRequestService.GetAllFriendRequestAsync();
            bool existingFriend = result.Any(s => s.SenderID == friendRequestDTO.SenderID && s.ReceiverID == friendRequestDTO.ReceiverID);
            var listRequest = result.ToList();
            int idRequest = 0;
            if (existingFriend)
            {
                for(int i = 0; i < listRequest.Count; i++)
                {
                    if (listRequest[i].SenderID == friendRequestDTO.SenderID && listRequest[i].ReceiverID == friendRequestDTO.ReceiverID)
                    {
                        idRequest = listRequest[i].ID;
                    }
                }
                await _friendRequestService.DeleteFriendRequestAsync(idRequest);
            }
            else
            {
                await _friendRequestService.AddFriendRequestAsync(friendRequestDTO);
            }      
            return Ok("Successed !");
        }

        //Get all requests
        [HttpGet("getAllFriendsRequest")]
        public async Task<IActionResult> getAllFriendsRequest()
        {
            var result = await _friendRequestService.GetAllFriendRequestAsync();
            if(result == null) return NotFound();
            return Ok(result);
        }

        //Get request by ID
        [HttpGet("getFriendsRequestByID/{id}")]
        public async Task<IActionResult> getFriendsRequestByID(int id)
        {
            var result = await _friendRequestService.GetFriendRequestByIdAsync(id);
            if(result == null) return NotFound();
            return Ok(result);
        }

        //Delete request by ID
        [HttpDelete("deleteFriendRequestByID/{id}")]
        public async Task<IActionResult> deleteFriendRequest(int id)
        {
            await _friendRequestService.DeleteFriendRequestAsync(id);
            return NoContent();
        }

        //Get requests by receiver ID
        [HttpGet("getFriendRequestByReceiverID/{id}")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<IActionResult> getFriendRequestsBySenderID(string id)
        {
            if(id == null) return BadRequest();
            var result = await _friendRequestService.GetFriendRequestByReceiverID(id);
            if(result == null) return NotFound();
            return Ok(result);
        }

        //Confirm friend request by ID
        [HttpPut("confirmRequest/{id}")]
        public async Task<IActionResult> ConfirmRequest(int id)
        {
            if(id == null || id == 0) return BadRequest();
            await _friendRequestService.ConfirmRequest(id);
            return Ok("Accected friend success !");
        }

        //Reject friend request by ID
        [HttpDelete("rejectFriendRequest/{id}")]
        public async Task<IActionResult> rejectFriendRequest(int id)
        {
            if (id == null || id == 0) return BadRequest();
            await _friendRequestService.DeleteFriendRequestAsync(id);
            return Ok("Reject friend request success !");
        }

        //Get friend request by user ID
        [HttpGet("getFriendRequestByUserID/{id}")]
        public async Task<IActionResult> getFriendRequestByUserID(string id)
        {
            if (id == null) return BadRequest();
            var result = await _friendRequestService.GetFriendRequestByUserID(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
