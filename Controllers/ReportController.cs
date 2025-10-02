using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Services;
using SocialMedia.Core.Entities.DTO;

namespace Social_Media.Controllers
{
    [Route("api/report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }
        //Create a new report
        [HttpPost("CreateReport")]
        public async Task<IActionResult> CreateReport([FromBody] ReportDTO model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (string.IsNullOrEmpty(model.UserId) || string.IsNullOrEmpty(model.Reason))
            {
                return BadRequest("UserId, content, and reportType are required.");
            }

            await _reportService.CreateReport(model);
            return Ok("Report created successfully.");
        }

        //Get all reports
        [HttpGet("GetAllReports")]
        public async Task<IActionResult> GetAllReports()
        {
            var reports = await _reportService.GetAllReports();
            if (reports == null) return NotFound();
            return Ok(reports);
        }

        //Delete a report by ID
        [HttpDelete("DeleteReport/{reportId}")]
        public async Task<IActionResult> DeleteReport(int reportId)
        {
            await _reportService.DeleteReport(reportId);
            return Ok("Report deleted successfully.");
        }
    }
}
