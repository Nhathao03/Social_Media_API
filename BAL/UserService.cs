using Social_Media.DAL;
using Social_Media.Models;
using Social_Media.Models.DTO;
using Social_Media.Models.DTO.AccountUser;

namespace Social_Media.BAL
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository repository)
        {
            _userRepository = repository;
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
        public async Task RegisterAccountAsync(RegisterDTO registerDTO)
        {
            await _userRepository.RegisterAccount(registerDTO);
        }

        public async Task<List<User>> FindUserAsync(string stringData)
        {
            return await _userRepository.FindUser(stringData);
        }

        public async Task UpdatePersonalInformation (PersonalInformationDTO personalInformationDTO)
        {
            await _userRepository.UpdatePersonalInformation(personalInformationDTO);
        }
    }
}
