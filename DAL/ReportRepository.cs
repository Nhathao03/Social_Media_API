using Microsoft.EntityFrameworkCore;
using Social_Media.Helpers;
using Social_Media.Models;
using Social_Media.Models.DTO;
using System.Reflection.Metadata;

namespace Social_Media.DAL
{
    public class ReportRepository :IReportRepository
    {
        private readonly AppDbContext _context;
        public ReportRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateReport(Report model)
        {
            await _context.reports.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Report>> GetAllReports()
        {
            return await _context.reports
                .Include(r => r.User)
                .Include(r => r.Post)
                .ToListAsync(); 
        }

        public async Task<Report> GetReportById(int reportId)
        {
            return await _context.reports
                .Include(r => r.User)  
                .Include(r => r.Post)  
                .FirstOrDefaultAsync(r => r.Id == reportId);
        }
        
        // Get report by user id
        public async Task<IEnumerable<Report>> GetReportsByUserId(string userId)
        {
            return await _context.reports
                .Where(r => r.UserId == userId)
                .Include(r => r.User)
                .Include(r => r.Post)
                .ToListAsync();
        }

        public async Task MarkReportAsReviewed(int reportId)
        {
            var report = await _context.reports.FindAsync(reportId);
            if (report != null)
            {
                report.Status = (int)Constants.ReportStatusEnum.Resolved;
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteReport(int reportId)
        {
            var report = await _context.reports.FindAsync(reportId);
            if (report != null)
            {
                _context.reports.Remove(report);
                await _context.SaveChangesAsync();
            }
        }
    }
}
