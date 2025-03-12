using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.BAL
{
    public interface IAddressService
    {
        Task<IEnumerable<Address>> GetAllAddressAsync();
        Task<Address> GetAddressByIdAsync(int id);
        Task UpdateAddressAsync(Address Address);
        Task DeleteAddressAsync(int id);
        Task AddAddressAsync(AddressDTO address);
    }
}
