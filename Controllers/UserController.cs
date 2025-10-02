using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Core.Services;
using SocialMedia.Core.Entities.DTO;
using SocialMedia.Core.Entities.DTO.AccountUser;
using SocialMedia.Core.Entities.Email;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ForgotPasswordRequest = SocialMedia.Core.Entities.Email.ForgotPasswordRequest;
using ResetPasswordRequest = SocialMedia.Core.Entities.Email.ResetPasswordRequest;
namespace Social_Media.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleCheckService _roleCheckService;
        private readonly IConfiguration _config;
        private static Dictionary<string, (string Otp, DateTime Expiry)> _otpStore = new();
        private readonly EmailService _emailService;

        public UserController(IUserService userService, 
            IRoleCheckService roleCheckService,
            IConfiguration configuration,
            EmailService emailService)
        {
            _userService = userService;
            _roleCheckService = roleCheckService;
            _config = configuration;
            _emailService = emailService;
        }
        
        //Get all users
        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            var getAllUser = await _userService.GetAllUsersAsync();
            return Ok(getAllUser);
        }

        //Get user by ID
        [HttpGet("GetUserById/{userID}")]
        public async Task<IActionResult> GetUserById(string userID)
        {
            var getUser = await _userService.GetUserByIdAsync(userID);
            return Ok(getUser);
        }

        //Update personal information user
        [HttpPut("UpdatePersonalInformation")]
        public async Task<IActionResult> UpdatePersonalInformation([FromBody] PersonalInformationDTO personalInformationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _userService.UpdatePersonalInformation(personalInformationDTO);
            return Ok("Success update user.");
        }

        //Change user password
        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (changePasswordDTO.newPass != changePasswordDTO.verifyPass)
            {
                return BadRequest("Verify Password incorrect");
            }
            else
            {
                await _userService.ChangePassword(changePasswordDTO);
                return Ok("Success update password user");
            }
        }

        //Change manage contact user
        [HttpPut("ManageContact")]
        public async Task<IActionResult> ManageContact([FromBody] ManageContactDTO manageContactDTO)
         {
            // check valid model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _userService.ManageContact(manageContactDTO);
            return Ok("Update manage contact success");
        }

        //Upload background user
        [HttpPut("UpdateBackgroundUser")]
        public async Task<IActionResult> UpdateBackgroundUser([FromBody] BackgroundDTO backgroundDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _userService.UploadBackgroundUser(backgroundDTO);
            return Ok("Update background user success");
        }

        //Get current logged-in user information
        [HttpGet("GetCurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User not authenticated.");

            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
                return NotFound("User not found.");

            return Ok(user);
        }

        // Forgot Passsword
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            // Check if email is not exist in database
            if (!await _userService.IsEmailExistsAsync(request.Email))
            {
                return BadRequest("Email not exist");
            }

            // Create OTP 6 numbers
            var otp = new Random().Next(100000, 999999).ToString();

            // Save OTP 5 minutes
            _otpStore[request.Email] = (otp, DateTime.UtcNow.AddMinutes(5));

            // Send OTP to Email
            await _emailService.SendOtpAsync(request.Email, otp);

            return Ok("OTP was send to email");
        }

        // Reset password
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            // If email don't exist in OTP store, it means forgot password request not sent
            if (!_otpStore.ContainsKey(request.Email))
                return BadRequest("Chưa gửi yêu cầu quên mật khẩu");

            var (otp, expiry) = _otpStore[request.Email];

            // Check if OTP is valid and not expired
            if (otp != request.Otp || DateTime.UtcNow > expiry)
                return BadRequest("OTP is not valid or not expired");

            if (request.NewPassword != request.ConfirmPassword)
                return BadRequest("New password and confirm new password do not match");

            var user = await _userService.GetUserByEmailAsync(request.Email);
            if (user == null) return BadRequest("User not exist");

            // Hash new password by BCrypt
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            await _userService.UpdateUserAsync(user);

            // Delete OTP after successful reset
            _otpStore.Remove(request.Email);

            return Ok("Reset password successful");
        }

        //Delete user by ID (Admin only)
        [HttpDelete("DeleteUserById/{userID}")]
        public async Task<IActionResult> DeleteUserById(string userID)
        {
            // check null or empty
            if (string.IsNullOrEmpty(userID))
            {
                return BadRequest("UserID is required.");
            }
            await _userService.DeleteUserAsync(userID);
            return NoContent();
        }
    }
}
