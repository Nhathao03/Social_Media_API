using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Services;

namespace Social_Media.Controllers
{
    [Route("api/UploadFile")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        //Upload file post to wwwroot
        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile (IFormFile file)
        {
            if(file != null && file.Length > 0)
            {
                var savePath = Path.Combine("wwwroot/posts/image", file.FileName);
                using (var fileStream = new FileStream(savePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                string filePath = "posts/image/" + file.FileName;
                return Ok(filePath);
            }
            return BadRequest("Image is empty");
        }
        
        //Upload file comment to wwwroot
        [HttpPost("UploadFileComment")]
        public async Task<IActionResult> UploadFileComment (IFormFile file)
        {
            if(file != null && file.Length > 0)
            {
                var savePath = Path.Combine("wwwroot/comments/images", file.FileName);
                using (var fileStream = new FileStream(savePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                string filePath = "comments/images/" + file.FileName;
                return Ok(filePath);
            }
            return BadRequest("Image is empty");
        }
        
        //Upload avatar user to wwwroot
        [HttpPost("UploadAvatarUser")]
        public async Task<IActionResult> UploadAvatarUser(IFormFile file)
        {
            if(file != null && file.Length > 0)
            {
                var savePath = Path.Combine("wwwroot/user/avatar", file.FileName);
                using (var fileStream = new FileStream(savePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                string filePath = "user/avatar/" + file.FileName;
                return Ok(filePath);
            }
            return BadRequest("Image is empty");
        }

        //Upload background user to wwwroot
        [HttpPost("UploadBackgroundUser")]
        public async Task<IActionResult> UploadBackgroundUser(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var savePath = Path.Combine("wwwroot/user/background", file.FileName);
                using (var fileStream = new FileStream(savePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                string filePath = "user/background/" + file.FileName;
                return Ok(filePath);
            }
            return BadRequest("Image is empty");
        }
    }
}
