using Social_Media.Models;
using Social_Media.Models.DTO;
using Social_Media.Models.DTO.AccountUser;

namespace Social_Media.DAL
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUser();
        Task<User> GetUserById(string id);
        Task UpdateUser(User user);
        Task DeleteUser(string id);
        Task RegisterAccount(User user);
        Task<List<User>> FindUser(string stringData);
        Task<bool> CheckexistEmail(string email);
        Task<User> GetUserByEmail(string email);
    }
}
