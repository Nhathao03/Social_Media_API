using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Social_Media.BAL;
using Social_Media.Helpers;
using Social_Media.Models;
using Social_Media.Models.DTO.AccountUser;
using Social_Media.Models.DTO.RoleCheck;
using Swashbuckle.AspNetCore.Annotations;
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
        [SwaggerOperation(Summary = "Register a new user", Description = "Creates a new user account using email, password.")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterAccount([FromBody] RegisterDTO? model)
        {
            // check valid model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model == null)
                return ApiResponseHelper.BadRequest("Model can not null.");

            var result = await _userService.RegisterAccountAsync(model);
            if (!result.Success)
                return ApiResponseHelper.BadRequest(string.Join("", "", result.Errors));
            return ApiResponseHelper.Created(result.Errors, "Register account successfully.");
        }

        // Login
        [HttpPost("login")]
        [SwaggerOperation(Summary = "User login", Description = "Logs in user with email and password, returns JWT tokens and refresh token.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> LoginAccount([FromBody] LoginDTO? model)
        {
            // check valid model
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (model == null)
                return ApiResponseHelper.BadRequest("Model can not null.");

            var result = await _userService.LoginAsync(model);

            if(!result.Success)
                return ApiResponseHelper.Unauthorized(string.Join("", "", result.Errors));

            var jwtSettings = _config.GetSection("Jwt");
            Response.Cookies.Append("token", result.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["Expires"]))
            }); ;

            return ApiResponseHelper.Success(new { result.RefreshToken, result.AccessToken }, "Login successfully." );
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
