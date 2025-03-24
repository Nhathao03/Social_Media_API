using Social_Media.DAL;
using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.BAL
{
    public class AddressService : IAddressService
    {
        private readonly AddressRepository _repository;

        public AddressService(AddressRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Address>> GetAllAddressAsync()
        {
            return await _repository.GetAllAddress();
        }

        public async Task<Address> GetAddressByIdAsync(int id)
        {
            return await _repository.GetAddressById(id);
        }

        public async Task UpdateAddressAsync(Address Address)
        {
            await _repository.UpdateAddress(Address);
        }

        public async Task DeleteAddressAsync(int id)
        {
            await _repository.DeleteAddress(id);
        }
        public async Task AddAddressAsync(AddressDTO addressDTO)
        {
            var address = new Address
            {
                name = addressDTO.name,
                slug = addressDTO.slug,
                type = addressDTO.type,
                name_with_type = addressDTO.name_with_type,
            };
            await _repository.AddNewAddress(address);
        }
    }
}
