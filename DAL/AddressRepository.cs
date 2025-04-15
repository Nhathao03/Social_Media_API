using Microsoft.EntityFrameworkCore;
using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.DAL
{
    public class AddressRepository :IAddressRepository
    {
        private readonly AppDbContext _context;

        public AddressRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Address>> GetAllAddress()
        {
            return await _context.addresses.ToListAsync();
        }

        public async Task<Address> GetAddressById(int id)
        {
            return await _context.addresses.FirstOrDefaultAsync(p => p.ID == id);
        }

        public async Task UpdateAddress(Address Address)
        {
            _context.addresses.Update(Address);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAddress(int id)
        {
            var Address = await _context.addresses.FindAsync(id);
            if (Address != null)
            {
                _context.addresses.Remove(Address);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddNewAddress(Address address)
        {
            _context.AddAsync(address);
            await _context.SaveChangesAsync();
        }
    }
}
