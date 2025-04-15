using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Social_Media.BAL;
using Social_Media.Models;
using Social_Media.Models.DTO;
using Social_Media.Models.DTO.AccountUser;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Social_Media.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleCheckService _roleCheckService;
        private readonly IConfiguration _config;
        public AuthController(IUserService userService, IRoleCheckService roleCheckService, IConfiguration configuration)
        {
            _userService = userService;
            _roleCheckService = roleCheckService;
            _config = configuration;
        }

        //Register acccount
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAccount([FromBody] RegisterDTO model)
        {
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                return BadRequest("Invalid input data.");

            var existingUsers = await _userService.GetAllUsersAsync();
            bool isEmailExists = existingUsers.Any(user => user.Email == model.Email);
            bool isPhoneNumberExists = existingUsers.Any(user => user.PhoneNumber == model.PhoneNumber);

            if (!isEmailExists & !isPhoneNumberExists)
            {
                await _userService.RegisterAccountAsync(model);
            }
            else
            {
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
            if (user == null)
                return Unauthorized("Email not found.");

            if (user.Password != model.Password)
            {
                return Unauthorized("Incorrect password.");
            } else
            {
                var token = GenerateJwtToken(user.Id);

                // Check user role
                string redirectURL = "/home";
                if (await _roleCheckService.IsAdminAsync(user.Id))
                {
                    redirectURL = "/admin";
                }

                return Ok(new
                {
                    token,RedirectURL = redirectURL
                });
            }  
        }

        //Generate token
        private string GenerateJwtToken(string userID)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));

            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, userID),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["Expires"])),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            var getAllUser = await _userService.GetAllUsersAsync();
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

        [HttpPut("UpdatePersonalInformation")]
        public async Task<IActionResult> UpdatePersonalInformation([FromBody] PersonalInformationDTO personalInformationDTO)
        {
            if (personalInformationDTO == null) return BadRequest();
            await _userService.UpdatePersonalInformation(personalInformationDTO);
            return Ok("Success update user.");
        }

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            if (changePasswordDTO == null) return BadRequest();
            if(changePasswordDTO.newPassword != changePasswordDTO.verifyPaddword)
            {
                return BadRequest("Verify Password incorrect");
            }
            else
            {
                await _userService.ChangePassword(changePasswordDTO);
                return Ok("Success update password user");
            }
        }
        [HttpPut("ManageContact")]
        public async Task<IActionResult> ManageContact([FromBody] ManageContactDTO manageContactDTO)
        {
            if(manageContactDTO == null ) return BadRequest();
            await _userService.ManageContact(manageContactDTO);
            return Ok("Update manage contact success");
        }
    }
}
