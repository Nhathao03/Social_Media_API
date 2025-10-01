using Social_Media.DAL;
using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.BAL
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _repository;

        public AddressService(IAddressRepository repository)
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

        public async Task UpdateAddressAsync(AddressDTO modelDTO)
        {
            var existingAddress = await _repository.GetAddressById(modelDTO.Id);
            if (existingAddress == null)
            {
                throw new KeyNotFoundException($"Address with Id {modelDTO.Id} not exits.");
            }
            existingAddress.name = modelDTO.name;
            existingAddress.slug = modelDTO.slug;
            existingAddress.type = modelDTO.type;
            existingAddress.name_with_type = modelDTO.name_with_type;

            await _repository.UpdateAddress(existingAddress);
        }


        public async Task DeleteAddressAsync(int id)
        {
            await _repository.DeleteAddress(id);
        }
        public async Task<int> AddAddressAsync(AddressDTO addressDTO)
        {
            if(addressDTO == null)
                throw new ArgumentNullException(nameof(addressDTO), "Address data is required.");
            if(string.IsNullOrWhiteSpace(addressDTO.name))
                throw new ArgumentException("Address name cannot be empty.", nameof(addressDTO.name));

            var address = new Address
            {
                slug = addressDTO.slug,
                name = addressDTO.name,
                type = addressDTO.type,
                name_with_type = addressDTO.name_with_type
            };

            var newId = await _repository.AddNewAddress(address);
            return newId;
        }
    }
}
