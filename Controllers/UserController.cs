using Microsoft.AspNetCore.Mvc;
using Social_Media.BAL;
using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAccount([FromBody] RegisterDTO model)
        {
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                return BadRequest("Invalid input data.");

            var existingUsers = await _userService.GetAllUsersAsync();
            bool isEmailExists = existingUsers.Any(user => user.Email == model.Email);

            if (!isEmailExists){
                await _userService.RegisterAccountAsync(model);
            }
            else{
                return BadRequest("Register account failed");
            }

            return Ok(new { message = "User registered successfully!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAccount([FromBody] LoginDTO model)
        {
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                return BadRequest("Invalid input data.");

            var user = (await _userService.GetAllUsersAsync())
                        .FirstOrDefault(u => u.Email == model.Email);

            if (user == null)
                return Unauthorized("Email not found.");

            if (user.Password != model.Password) 
                return Unauthorized("Incorrect password.");

            return Ok(new { Message = "Login successful.", token = user.Id });
        }

        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            var getAllUser =  await _userService.GetAllUsersAsync();
            return Ok(getAllUser);
        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(string userID)
        {
            var getUser = await _userService.GetUserByIdAsync(userID);
            return Ok(getUser);
        }
       
    }
}
