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
    public class UserLoginService : IUserLoginService
    {
        private readonly UserLoginRepository _userLoginRepository;

        public UserLoginService(UserLoginRepository repository)
        {
            _userLoginRepository = repository;
        }

        public async Task<IEnumerable<UserLogins>> GetAllUserLoginsAsync()
        {
            return await _userLoginRepository.GetAllUserLogin();
        }

        public async Task<UserLogins> GetUserLoginByIdAsync(int id)
        {
            return await _userLoginRepository.GetUserLoginById(id);
        }

        public async Task AddUserLoginsAsync(UserLogins userLogins)
        {
            await _userLoginRepository.AddUserLogin(userLogins);
        }
        public async Task UpdateUserLoginAsync(UserLogins userLogins)
        {
            await _userLoginRepository.UpdateUserLogin(userLogins);
        }

        public async Task DeleteUserLoginAsync(int id)
        {
            await _userLoginRepository.DeleteUserLogin(id);
        }
    }
}
