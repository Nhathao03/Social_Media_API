using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Social_Media.BAL;
using Social_Media.DAL;
using Social_Media.Models;
using Social_Media.Models.DTO;
using Social_Media.Models.DTO.AccountUser;
using Social_Media.Models.Email;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ForgotPasswordRequest = Social_Media.Models.Email.ForgotPasswordRequest;
using ResetPasswordRequest = Social_Media.Models.Email.ResetPasswordRequest;

namespace Social_Media.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleCheckService _roleCheckService;
        private readonly IConfiguration _config;
        private static Dictionary<string, (string Otp, DateTime Expiry)> _otpStore = new();
        private readonly EmailService _emailService;

        public AuthController(IUserService userService, 
            IRoleCheckService roleCheckService,
            IConfiguration configuration,
            EmailService emailService)
        {
            _userService = userService;
            _roleCheckService = roleCheckService;
            _config = configuration;
            _emailService = emailService;
        }

        // Register account
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAccount([FromBody] RegisterDTO model)
        {
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                return BadRequest("Invalid input data.");

            var existingUsers = await _userService.GetAllUsersAsync();
            bool isEmailExists = existingUsers.Any(user => user.Email == model.Email);
            bool isPhoneNumberExists = existingUsers.Any(user => user.PhoneNumber == model.PhoneNumber);

            if (isEmailExists || isPhoneNumberExists)
            {
                return BadRequest("Email or phone number already exists.");
            }

            // Hash password before save to db
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
            model.Password = hashedPassword;
            await _userService.RegisterAccountAsync(model);

            return Ok(new { message = "User registered successfully!" });
        }

        // Login
        [HttpPost("login")]
        public async Task<IActionResult> LoginAccount([FromBody] LoginDTO model)
        {
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                return BadRequest("Invalid input data.");

            var user = (await _userService.GetAllUsersAsync())
                        .FirstOrDefault(u => u.Email == model.Email);

            if (user == null)
                return Unauthorized("Email not found.");

            // Equals password
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);
            if (!isPasswordValid)
            {
                return Unauthorized("Incorrect password.");
            }

            // Generate JWT token
            var token = GenerateJwtToken(user.Id);

            // Redirect URL based on user role
            string redirectURL = "/home";
            if (await _roleCheckService.IsAdminAsync(user.Id))
            {
                redirectURL = "/admin";
            }

            return Ok(new
            {
                token,
                redirectURL,
            });
        }

        //Generate JWT Token
        private string GenerateJwtToken(string userID)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));

            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, userID),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            string role = "User";
            if (_roleCheckService.IsAdminAsync(userID).Result)
            {
                role = "Admin";
            }

            claims.Add(new Claim("role", role));

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["Expires"])),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //Refresh JWT Token
        [HttpPost("refreshToken")]
        public IActionResult RefreshToken([FromBody] string token)
        {
            if (string.IsNullOrEmpty(token))
                return BadRequest("Token is required.");

            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(token))
                return BadRequest("Invalid token format.");

            var jwtToken = handler.ReadJwtToken(token);
            var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User not authenticated.");

            var newToken = GenerateJwtToken(userId);
            return Ok(new { token = newToken });
        }

        //Decode JWT Token
        [HttpPost("decode")]
        public IActionResult Decode([FromBody] string jwtToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(jwtToken))
                    return BadRequest("Token is required.");

                var handler = new JwtSecurityTokenHandler();

                if (!handler.CanReadToken(jwtToken))
                    return BadRequest("Invalid JWT format.");

                var token = handler.ReadJwtToken(jwtToken);

                // Transform claims dictionary
                var payload = token.Payload
                    .ToDictionary(kvp =>
                        kvp.Key == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name" ? "userID" : kvp.Key,
                        kvp => kvp.Value
                    );

                var result = new
                {
                    Header = token.Header,
                    Payload = payload
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
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

        //Logout user
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok(new { message = "Logout successful" });
        }

        //Find user by string data (email or phone number)
        [HttpGet("findUser/{stringData}")]
        public async Task<IActionResult> FindUser(string stringData)
        {
            var user = await _userService.FindUserAsync(stringData);
            if (user == null) return NotFound();
            return Ok(user);
        }

        //Update personal information user
        [HttpPut("UpdatePersonalInformation")]
        public async Task<IActionResult> UpdatePersonalInformation([FromBody] PersonalInformationDTO personalInformationDTO)
        {
            if (personalInformationDTO == null) return BadRequest();
            await _userService.UpdatePersonalInformation(personalInformationDTO);
            return Ok("Success update user.");
        }

        //Change user password
        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            if (changePasswordDTO == null) return BadRequest();
            if(changePasswordDTO.newPass != changePasswordDTO.verifyPass)
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
            if(manageContactDTO == null ) return BadRequest();
            await _userService.ManageContact(manageContactDTO);
            return Ok("Update manage contact success");
        }

        //Upload background user
        [HttpPut("UpdateBackgroundUser")]
        public async Task<IActionResult> UpdateBackgroundUser([FromBody] BackgroundDTO backgroundDTO)
        {
            if (backgroundDTO == null) return BadRequest();
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
            if (!await _userService.CheckexistEmail(request.Email))
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
    }
}
