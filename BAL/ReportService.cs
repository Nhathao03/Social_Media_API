using Social_Media.DAL;
using Social_Media.Helpers;
using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.BAL
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task CreateReport(ReportDTO model)
        {
            var report = new Report
            {
                Id = model.Id,
                UserId = model.UserId,
                Reason = model.Reason,
                PostId = model.PostId,
                CreatedAt = DateTime.UtcNow,
                Status = (int)Constants.ReportStatusEnum.Pending,
            };
            await _reportRepository.CreateReport(report);
        }

        public async Task<IEnumerable<Report>> GetAllReports()
        {
            return await _reportRepository.GetAllReports();
        }
        public async Task DeleteReport(int reportId)
        {
            await _reportRepository.DeleteReport(reportId);
        }
        public async Task<Report> GetReportById(int reportId)
        {
            return await _reportRepository.GetReportById(reportId);
        }
        public async Task MarkReportAsReviewed(int reportId)
        {
            await _reportRepository.MarkReportAsReviewed(reportId);
        }

    }
}
