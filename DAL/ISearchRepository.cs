using Social_Media.Models;

namespace Social_Media.DAL
{
    public interface ISearchRepository
    {
        Task<IEnumerable<User>> FindUser(string stringData, string CurrentUserIdSearch);
    }
}
