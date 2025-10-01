using Microsoft.EntityFrameworkCore;
using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.DAL
{
    public class TypeFriendsRepository : ITypeFriendsRepository
    {
        private readonly AppDbContext _context;

        public TypeFriendsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Type_Friends>> GetAllTypeFriends()
        {
            return await _context.type_friends.ToListAsync();
        }

        public async Task<Type_Friends> GetTypeFriendsById(int id)
        {
            return await _context.type_friends.FirstOrDefaultAsync(p => p.ID == id);
        }

        public async Task UpdateTypeFriends(Type_Friends TypeFriends)
        {
            _context.type_friends.Update(TypeFriends);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTypeFriends(int id)
        {
            var TypeFriends = await _context.type_friends.FindAsync(id);
            if (TypeFriends != null)
            {
                _context.type_friends.Remove(TypeFriends);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddTypeFriends (Type_Friends model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
        }

    }
}
