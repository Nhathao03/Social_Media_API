using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Services;
using SocialMedia.Infrastructure.Repositories;

namespace Social_Media.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        //Search user by string data (email or phone number)
        [HttpGet("SearchUser/{stringData}/{CurrentUserIdSearch}")]
        public async Task<IActionResult> SearchUser(string stringData, string CurrentUserIdSearch)
        {
            var user = await _searchService.FindUserAsync(stringData, CurrentUserIdSearch);
            if (user == null) return NotFound();
            return Ok(user);
        }
    }
}
