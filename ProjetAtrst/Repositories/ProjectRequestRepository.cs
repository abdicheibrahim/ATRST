using ProjetAtrst.Interfaces.Repositories;
using ProjetAtrst.Models;

namespace ProjetAtrst.Repositories
{
    public class ProjectRequestRepository : IProjectRequestRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(ProjectRequest request)
        {
            await _context.ProjectRequests.AddAsync(request);
        }

        public async Task<ProjectRequest> GetByIdAsync(int id)
        {
            return await _context.ProjectRequests
                .Include(r => r.Project)
                .Include(r => r.Sender)
                .Include(r => r.Receiver)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<ProjectRequest>> GetByReceiverIdAsync(string userId)
        {
            return await _context.ProjectRequests
                .Include(r => r.Project)
                .Include(r => r.Sender)
                    .ThenInclude(s => s.User) // ضروري لتحميل FullName
                .Include(r => r.Receiver)
                .Where(r => r.ReceiverId == userId)
                .ToListAsync();
        }
        public async Task<IEnumerable<ProjectRequest>> GetBySenderIdAsync(string senderId)
        {
            return await _context.ProjectRequests
                .Include(r => r.Receiver)
                    .ThenInclude(r => r.User)
                .Include(r => r.Project)
                .Where(r => r.SenderId == senderId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }


        public async Task<IEnumerable<ProjectRequest>> GetByProjectIdAsync(int projectId)
        {
            return await _context.ProjectRequests
                .Include(r => r.Sender)
                .Include(r => r.Receiver)
                .Where(r => r.ProjectId == projectId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProjectRequest>> GetByUserAndProjectAsync(string userId, int? projectId = null)
        {
            var query = _context.ProjectRequests
                .Include(r => r.Project)
                .Include(r => r.Receiver)
                .Where(r => r.SenderId == userId);

            if (projectId.HasValue && projectId.Value > 0)
                query = query.Where(r => r.ProjectId == projectId);

            return await query.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
