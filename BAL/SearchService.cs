using Social_Media.DAL;
using Social_Media.Models;
using System.Text;

namespace Social_Media.BAL
{
    public class SearchService :ISearchService
    {
        private readonly ISearchRepository _searchRepository;
        public SearchService(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        // Normalize string by removing diacritics and converting to lowercase
        private static string NormalizeString(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;

            input = input.ToLowerInvariant();
            var normalized = input.Normalize(System.Text.NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (var c in normalized)
            {
                var unicodeCategory = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }
            return sb.ToString().Normalize(System.Text.NormalizationForm.FormC);
        }
        //Find user by username || phonenumber || email
        public async Task<IEnumerable<User>> FindUserAsync(string stringData, string CurrentUserIdSearch)
        {
            string normalizedData = NormalizeString(stringData);
            return await _searchRepository.FindUser(normalizedData, CurrentUserIdSearch);
        }
    }
}
