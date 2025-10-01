using Social_Media.Models;

namespace Social_Media.BAL
{
    public interface ISearchService
    {
        Task<IEnumerable<User>> FindUserAsync(string stringData, string CurrentUserIdSearch);
    }
}
