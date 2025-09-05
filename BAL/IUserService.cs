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
        Task<List<User>> FindUserAsync(string stringData, string CurrentUserIdSearch);
        Task UpdatePersonalInformation (PersonalInformationDTO personalInformationDTO);
        Task ChangePassword(ChangePasswordDTO changePasswordDTO);
        Task ManageContact(ManageContactDTO manageContactDTO);
        Task UploadBackgroundUser(BackgroundDTO backgroundDTO);
        Task<bool> IsEmailExistsAsync(string email);
        Task<bool> IsPhoneExistsAsync(string phoneNumber);
        Task<User> GetUserByEmailAsync(string email);
    }
}
