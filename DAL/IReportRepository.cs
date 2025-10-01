using Social_Media.Models;
using Social_Media.Models.DTO;

namespace Social_Media.DAL
{
    public interface IReportRepository
    {
        Task CreateReport(Report model);
        Task<IEnumerable<Report>> GetAllReports();
        Task DeleteReport(int reportId);
        Task<Report> GetReportById(int reportId);
        Task MarkReportAsReviewed(int reportId);
        Task<IEnumerable<Report>> GetReportsByUserId(string userId);
    }
}
