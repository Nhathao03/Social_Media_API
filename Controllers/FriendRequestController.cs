﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("getFriendRequestByReceiverID/{id}")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<IActionResult> getFriendRequestsBySenderID(string id)
        {
            if(id == null) return BadRequest();
            var result = await _friendRequestService.GetFriendRequestByReceiverID(id);
            if(result == null) return NotFound();
            return Ok(result);
        }

        [HttpPut("confirmRequest/{id}")]
        public async Task<IActionResult> ConfirmRequest(int id)
        {
            if(id == null || id == 0) return BadRequest();
            await _friendRequestService.ConfirmRequest(id);
            return Ok("Accected friend success !");
        }

        [HttpDelete("rejectFriendRequest/{id}")]
        public async Task<IActionResult> rejectFriendRequest(int id)
        {
            if (id == null || id == 0) return BadRequest();
            await _friendRequestService.DeleteFriendRequestAsync(id);
            return Ok("Reject friend request success !");
        }

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
