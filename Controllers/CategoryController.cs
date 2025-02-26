using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Social_Media.BAL;
using Social_Media.Models;

namespace Social_Media.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IPostCategoryService _category;

        public CategoryController( IPostCategoryService category)
        {
            _category = category;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var postcategory = await _category.GetAllPostCategory();
            return Ok(postcategory);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var postcategory = await _category.GetPostCategoryById(id);
            if (postcategory == null) return NotFound();
            return Ok(postcategory);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostCategory postcategory)
        {
            if (postcategory == null) return BadRequest();
            await _category.AddPostCategory(postcategory);
            return CreatedAtAction(nameof(Get), new { id = postcategory.ID }, postcategory);
        }
    }
}
