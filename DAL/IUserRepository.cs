using Social_Media.Models;
using Social_Media.Models.DTO.AccountUser;

namespace Social_Media.DAL
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUser();
        Task<User> GetUserById(string id);
        Task UpdateUser(User user);
        Task DeleteUser(string id);
        Task<User> RegisterAccount(User user);
        Task<bool> IsEmailExistsAsync(string email);
        Task<bool> IsPhoneExistsAsync(string phoneNumber);
        Task<User> GetUserByEmail(string email);
        Task<bool> CheckPasswordSignInAsync(LoginDTO model, string password);
    }
}
