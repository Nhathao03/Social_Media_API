using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Social_Media.DAL;
using Social_Media.Helpers;
using Social_Media.Models;
using Social_Media.Models.DTO.AccountUser;
using Social_Media.Models.DTO.RoleCheck;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace Social_Media.BAL
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleService _roleService;
        private readonly ILogger<UserService> _logger;
        private readonly IConfiguration _config;
        private readonly IRoleCheckService _roleCheckService;

        public UserService(IUserRepository repository,
            IRoleService roleService,
            ILogger<UserService> logger,
            IConfiguration config,
            IRoleCheckService roleCheckService)
        {
            _userRepository = repository;
            _roleService = roleService;
            _logger = logger;
            _config = config;
            _roleCheckService = roleCheckService;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUser();
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }
        public async Task UpdateUserAsync(User user)
        {
            await _userRepository.UpdateUser(user);
        }

        public async Task DeleteUserAsync(string id)
        {
            await _userRepository.DeleteUser(id);
            await _roleCheckService.DeleteRoleCheckByUserIdAsync(id);
        }

        //Generate userID
        private string GenerateUserID()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 20)
              .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }

        //Register account
        public async Task<AuthResultDTO> RegisterAccountAsync(RegisterDTO model)
        {
            _logger.LogInformation("Registering new user with email: {Email}", model.Email);

            var emailExists = await _userRepository.IsEmailExistsAsync(model.Email);
            if(emailExists)
            {
                _logger.LogWarning("Registeration failed: Email {Email} already in use.", model.Email);
                return new AuthResultDTO
                {
                    Success = false,
                    Errors = new[] { "Email already in use." }
                };
            }

            if (!string.IsNullOrEmpty(model.PhoneNumber) && await _userRepository.IsPhoneExistsAsync(model.PhoneNumber))
            {
                _logger.LogInformation("Registeration failed: Phone number {PhoneNumber} already in use.", model.PhoneNumber);
                return new AuthResultDTO
                {
                    Success = false,
                    Errors = new[] { "Phone number already in use." }
                };
            }

            var user = new User
            {
                Id = GenerateUserID(),
                Email = model.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                PhoneNumber = model.PhoneNumber,
                Fullname = model.FullName,
                Birth = model.Birth,
                gender = null,
                Avatar = "user/avatar/avatar_user.png",
                BackgroundProfile = "user/background/1.jpg",
                NormalizeEmail = NormalizeString(model.Email),
                NormalizeUsername = NormalizeString(model.FullName),
            };
            var result =  await _userRepository.RegisterAccount(user);

            if(result == null)
            {
                _logger.LogError("Registeration failed for email: {Email}", model.Email);
                return new AuthResultDTO
                {
                    Success = false,
                    Errors = new[] { "Error while registering." }
                };
            }

            //Get id role user
            var roleId = await _roleService.GetRoleIdUserAsync();
            var roleUser = new RoleCheckDTO
            {
                UserId = user.Id,
                RoleId = roleId,
            };
            await _roleCheckService.AddRoleCheckAsync(roleUser);

           return new AuthResultDTO
           {
               Success = true
           };
        }

        // Normalize string by removing diacritics and converting to lowercase
        private static string NormalizeString(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;

            input = input.ToLowerInvariant();
            var normalized = input.Normalize(System.Text.NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (var c in normalized)
            {
                var unicodeCategory = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }
            return sb.ToString().Normalize(System.Text.NormalizationForm.FormC);
        }

        //Update information user
        public async Task UpdatePersonalInformation (PersonalInformationDTO personalInformationDTO)
        {
            var user = await _userRepository.GetUserById(personalInformationDTO.userID);
            if (user == null) return;

            user.Fullname = personalInformationDTO.fullname ?? user.Fullname;
            user.Birth = personalInformationDTO.Birth ?? user.Birth;
            user.Avatar = personalInformationDTO.avatar ?? user.Avatar;
            user.gender = personalInformationDTO.gender ?? user.gender;
            user.addressID = personalInformationDTO.addressID ?? user.addressID;

            await _userRepository.UpdateUser(user);
        }

        //Change Password
        public async Task ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            var result = await _userRepository.GetUserById(changePasswordDTO.userID);
            if (result == null) return;
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(changePasswordDTO.currentPass, result.Password);
            if (isPasswordValid)
            {
                string newPass = BCrypt.Net.BCrypt.HashPassword(changePasswordDTO.newPass);
                result.Password = newPass;
                await _userRepository.UpdateUser(result);
            }   
        }

        public async Task ManageContact (ManageContactDTO manageContactDTO)
        {
            var result = await _userRepository.GetUserById(manageContactDTO.userID);
            if(result == null) return;
            result.PhoneNumber = manageContactDTO.phoneNumber;
            await _userRepository.UpdateUser(result);
        }
        
        public async Task UploadBackgroundUser(BackgroundDTO backgroundDTO)
        {
            var user = await _userRepository.GetUserById(backgroundDTO.userId);
            if (user == null ) return;
            if (backgroundDTO != null)
            {
                user.BackgroundProfile = backgroundDTO.backgroundImage;
                await _userRepository.UpdateUser(user);
            }
        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            return await _userRepository.IsEmailExistsAsync(email);
        }

        public async Task<bool> IsPhoneExistsAsync(string phoneNumber)
        {
            return await _userRepository.IsPhoneExistsAsync(phoneNumber);
        }

        public async Task<AuthResultDTO> LoginAsync(LoginDTO model)
        {
            _logger.LogInformation("Attempting login for email: {Email}", model.Email);
            var user = await _userRepository.GetUserByEmail(model.Email);
            if (user == null)
            {
                return new AuthResultDTO
                {
                    Success = false,
                    Errors = new[] { "Invalid credentials." }
                };
            }

            var result = await _userRepository.CheckPasswordSignInAsync(model, model.Password);
            if(!result)
            {
                _logger.LogWarning("Login failed for email: {Email} due to invalid password.", model.Email);
                return new AuthResultDTO
                {
                    Success = false,
                    Errors = new[] { "Invalid credentials." }
                };
            }

            bool isAdmin = await _roleCheckService.IsAdminAsync(user.Id);
            string role = isAdmin ? "Admin" : "User";

            // Generate JWT token
            var accessToken = await GenerateJwtToken(user.Id, role);
            //string refreshToken = await RefreshToken

            _logger.LogInformation("Login successful for email: {Email}", model.Email);
            return new AuthResultDTO
            {
                Success = true,
                AccessToken = accessToken,
                //RefreshToken = refreshToken
            };


        }

        //Generate JWT Token
        private async Task<string> GenerateJwtToken(string userID, string role)
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
            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            return accessToken;
        }
    }
}
