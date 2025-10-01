using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.BAL
{
    public interface IReportService
    {
        Task CreateReport(ReportDTO model);
        Task<IEnumerable<Report>> GetAllReports();
        Task DeleteReport(int reportId);
        Task<Report> GetReportById(int reportId);
        Task MarkReportAsReviewed(int reportId);
    }
}
