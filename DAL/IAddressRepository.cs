using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.DAL
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> GetAllAddress();
        Task<Address> GetAddressById(int id);
        Task UpdateAddress(Address Address);
        Task DeleteAddress(int id);
        Task<int> AddNewAddress (Address address);
    }
}
