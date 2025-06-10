using Microsoft.EntityFrameworkCore;
using ProjetAtrst.Interfaces.Repositories;
using ProjetAtrst.Models;

namespace ProjetAtrst.Repositories
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext context) : base(context) { }

        public async Task<int> CountLedByAsync(string userId)
        {
            return await _context.Projects.CountAsync(p => p.LeaderId == userId);
        }

        public async Task<int> CountMemberInAsync(string userId)
        {
            return await _context.ProjectMemberships
                .CountAsync(m => m.MemberId == userId);
        }

        public async Task<IEnumerable<Project>> GetLedProjectsAsync(string userId, int takeCount)
        {
            return await _context.Projects
                .Where(p => p.LeaderId == userId)
                .OrderByDescending(p => p.LastActivity) // أو حسب آخر تحديث إن وُجد
                .Take(takeCount)
                .ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetMemberProjectsAsync(string userId, int takeCount)
        {
            return await _context.ProjectMemberships
                .Where(m => m.MemberId == userId)
                .OrderByDescending(m => m.JoinedAt) // تأكد من وجود هذا العمود، أو استخدم أي تاريخ مناسب
                .Select(m => m.Project)
                .Take(takeCount)
                .ToListAsync();
        }
        public async Task<IEnumerable<Project>> GetOpenProjectsForJoiningAsync(string researcherId)
        {
            return await _context.Projects
                .Where(p => p.ProjectStatus == ProjectStatus.Open
                    && p.Leader.Id != researcherId
                    && !p.ProjectMemberships.Any(m => m.MemberId == researcherId))
                .Include(p => p.Leader)
                .ToListAsync();
        }

    }
}
