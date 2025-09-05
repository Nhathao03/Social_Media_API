using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Social_Media.DAL;
using Social_Media.Models;
using Social_Media.Models.DTO;
using Social_Media.Models.DTO.AccountUser;
using System.Text;

namespace Social_Media.BAL
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;
        private readonly RoleCheckRepository _roleCheckRepository;

        public UserService(UserRepository repository,
            RoleCheckRepository roleCheckRepository)
        {
            _userRepository = repository;
            _roleCheckRepository = roleCheckRepository;
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
        }

        //Generate userID
        private string GenerateUserID()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 20)
              .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }

        //Register account
        public async Task RegisterAccountAsync(RegisterDTO registerDTO)
        {
            var user = new User
            {
                Id = GenerateUserID(),
                Email = registerDTO.Email,
                Password = registerDTO.Password,
                PhoneNumber = registerDTO.PhoneNumber,
                Fullname = registerDTO.FullName,
                Birth = registerDTO.Birth,
                gender = null,
                Avatar = "user/avatar/avatar_user.png",
                BackgroundProfile = "user/background/1.jpg",
                NormalizeEmail = NormalizeString(registerDTO.Email),
                NormalizeUsername = NormalizeString(registerDTO.FullName),
            };
            await _userRepository.RegisterAccount(user);
            var roleUser = new RoleCheck
            {
                UserID = user.Id,
                RoleID = "1",
            };
            await _roleCheckRepository.AddRoleCheck(roleUser);
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

        //Find user by username || phonenumber || email
        public async Task<List<User>> FindUserAsync(string stringData, string CurrentUserIdSearch)
        {
            string normalizedData = NormalizeString(stringData);
            return await _userRepository.FindUser(normalizedData, CurrentUserIdSearch);
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
    }
}
