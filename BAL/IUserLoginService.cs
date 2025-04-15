using Social_Media.Models;
using Social_Media.Models.DTO;
namespace Social_Media.BAL
{
    public interface IUserLoginService
    {
        Task<IEnumerable<UserLogins>> GetAllUserLoginsAsync();
        Task<UserLogins> GetUserLoginByIdAsync(int id);
        Task UpdateUserLoginAsync(UserLogins userLogins);
        Task DeleteUserLoginAsync(int id);
        Task AddUserLoginsAsync (UserLogins userLogins);
    }
}
