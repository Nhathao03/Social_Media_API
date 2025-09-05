using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Social_Media.BAL;
using Social_Media.Models;
using Social_Media.Models.DTO;
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
        private static Dictionary<string, (string Otp, DateTime Expiry)> _otpStore = new();

        public AuthController(IUserService userService,
            IRoleCheckService roleCheckService,
            IConfiguration configuration)
        {
            _userService = userService;
            _roleCheckService = roleCheckService;
            _config = configuration;
        }

        // Register account
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAccount([FromBody] RegisterDTO model)
        {
            // check valid model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                return BadRequest("Invalid input data.");


            if(await _userService.IsEmailExistsAsync(model.Email))
                return BadRequest("Email already in use.");
            if (!string.IsNullOrEmpty(model.PhoneNumber) && await _userService.IsPhoneExistsAsync(model.PhoneNumber))
                return BadRequest("Phone number already in use.");

            // Hash password before save to db
            model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            await _userService.RegisterAccountAsync(model);

            return Ok(new { message = "User registered successfully!" });
        }

        // Login
        [HttpPost("login")]
        public async Task<IActionResult> LoginAccount([FromBody] LoginDTO model)
        {
            // check valid model
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                return BadRequest("Invalid input data.");

            // Get user by email
            var user = await _userService.GetUserByEmailAsync(model.Email);
            if (user == null)
                return Unauthorized("Email not found.");

            // Equals password
            if (!BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                return Unauthorized("Incorrect password.");

            // Get user role
            bool isAdmin = await _roleCheckService.IsAdminAsync(user.Id);
            string role = isAdmin ? "Admin" : "User";

            // Generate JWT token
            var token = GenerateJwtToken(user.Id, role);

            // Redirect URL based on user role
            string redirectURL = isAdmin ? "/admin" : "/home";

            return Ok(new
            {
                token,
                redirectURL,
            });
        }

        //Generate JWT Token
        private string GenerateJwtToken(string userID, string role)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userID),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, role)
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
            var role = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User not authenticated.");

            var newToken = GenerateJwtToken(userId, role);
            return Ok(new { token = newToken });
        }

        //Decode JWT Token
        [HttpPost("decode")]
        public IActionResult Decode([FromBody] string jwtToken)
        {

            if (string.IsNullOrWhiteSpace(jwtToken))
                return BadRequest("Token is required.");

            var handler = new JwtSecurityTokenHandler();

            if (!handler.CanReadToken(jwtToken))
                return BadRequest("Invalid JWT format.");
            try
            {
                var token = handler.ReadJwtToken(jwtToken);

                // Map claim key to easy readable keys
                var claimKeyMap = new Dictionary<string, string>
                {
                    {"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "userID" },
                    {"http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "role" },
                };
                // Transform claims dictionary
                var payload = token.Payload
                    .ToDictionary(
                        kvp => claimKeyMap.ContainsKey(kvp.Key) ? claimKeyMap[kvp.Key] : kvp.Key,
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

        //Logout user
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok(new { message = "Logout successful" });
        }

    }
}
