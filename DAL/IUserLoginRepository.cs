using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.DAL
{
    public interface IUserLoginRepository
    {
        Task<IEnumerable<UserLogins>> GetAllUserLogin();
        Task<UserLogins> GetUserLoginById(int id);
        Task UpdateUserLogin(UserLogins userLogins);
        Task DeleteUserLogin(int id);
        Task AddUserLogin(UserLogins userLogins);
    }
}
