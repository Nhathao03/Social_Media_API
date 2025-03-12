﻿using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.DAL
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUser();
        Task<User> GetUserById(string id);
        Task UpdateUser(User user);
        Task DeleteUser(string id);
        Task RegisterAccount(RegisterDTO registerDTO);
        Task<List<User>> FindUser(string stringData);
    }
}
