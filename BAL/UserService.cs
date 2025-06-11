using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Social_Media.DAL;
using Social_Media.Models;
using Social_Media.Models.DTO;
using Social_Media.Models.DTO.AccountUser;

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

        public async Task UpdateUserAsync(User user)
        {
            await _userRepository.UpdateUser(user);
        }

        public async Task DeleteUserAsync(string id)
        {
            await _userRepository.DeleteUser(id);
        }

        //Render userID
        private string GenerateUserID()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 20)
              .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }

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
                Avatar = "user/avatar/avatar_user.png"
            };
            await _userRepository.RegisterAccount(user);
            var roleUser = new RoleCheck
            {
                UserID = user.Id,
                RoleID = "1",
            };
            await _roleCheckRepository.AddRoleCheck(roleUser);
        }

        public async Task<List<User>> FindUserAsync(string stringData)
        {
            return await _userRepository.FindUser(stringData);
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
            if (changePasswordDTO.currentPass != result.Password) return;
            result.Password = changePasswordDTO.newPass;
            await _userRepository.UpdateUser(result);
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
    }
}
