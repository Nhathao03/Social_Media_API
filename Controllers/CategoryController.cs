using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Services;
using SocialMedia.Core.Entities.PostEntity;

namespace Social_Media.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IPostCategoryService _category;

        public CategoryController( IPostCategoryService category)
        {
            _category = category;
        }

        //Get all categories
        [HttpGet("getAllCategory")]
        public async Task<IActionResult> Get()
        {
            var postcategory = await _category.GetAllPostCategory();
            return Ok(postcategory);
        }

        //Get category by ID
        [HttpGet("getCategoryByID/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var postcategory = await _category.GetPostCategoryById(id);
            if (postcategory == null) return NotFound();
            return Ok(postcategory);
        }

        //Delete category by ID (Admin only)
        [Authorize(Roles = "Admin")]
        [HttpPost("addNewCategory")]
        public async Task<IActionResult> Post([FromBody] PostCategory postcategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _category.AddPostCategory(postcategory);
            return CreatedAtAction(nameof(Get), new { id = postcategory.ID }, postcategory);
        }

        //Update category by ID (Admin only)
        [Authorize(Roles = "Admin")]
        [HttpPut("updateCategory")]
        public async Task<IActionResult> Put([FromBody] PostCategory postcategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _category.UpdatePostCategory(postcategory);
            return Ok("Update category success");
        }
    }
}
