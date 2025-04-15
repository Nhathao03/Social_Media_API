using Social_Media.Models;
using Social_Media.Models.DTO;
using Social_Media.Models.DTO.AccountUser;

namespace Social_Media.BAL
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(string id);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(string id);
        Task RegisterAccountAsync (RegisterDTO registerDTO);
        Task<List<User>> FindUserAsync(string stringData);
        Task UpdatePersonalInformation (PersonalInformationDTO personalInformationDTO);
        Task ChangePassword(ChangePasswordDTO changePasswordDTO);
        Task ManageContact(ManageContactDTO manageContactDTO);
    }
}
