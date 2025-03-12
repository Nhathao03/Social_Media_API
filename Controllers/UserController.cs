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
        private readonly IRoleCheckService _roleCheckService;
        private readonly IRoleService _roleService;
        public UserController(IUserService userService, IRoleCheckService roleCheckService, IRoleService roleService)
        {
            _userService = userService;
            _roleCheckService = roleCheckService;
            _roleService = roleService;
        }

        //Register acccount
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

        //Check login account
        [HttpPost("login")]
        public async Task<IActionResult> LoginAccount([FromBody] LoginDTO model)
        {
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                return BadRequest("Invalid input data.");
            var user = (await _userService.GetAllUsersAsync())
                        .FirstOrDefault(u => u.Email == model.Email);
            if (user == null )
                return Unauthorized("Email not found.");

            if (user.Password != model.Password) 
                return Unauthorized("Incorrect password.");
            // Check user role
            string redirectURL = "/home"; 
            if (await _roleCheckService.IsAdminAsync(user.Id)) // IsAdminAsync checks if the user is an admin
            {
                redirectURL = "/admin";
            }

            return Ok(new
            {
                Message = "Login successful.",
                Token = user.Id,
                RedirectURL = redirectURL
            });
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

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok(new { message = "Logout successful" });
        }

        [HttpGet("findUser/{stringData}")]
        public async Task<IActionResult> FindUser(string stringData)
        {
            var user = await _userService.FindUserAsync(stringData);
            if (user == null) return NotFound();
            return Ok(user);
        }
    }
}
