using Microsoft.CodeAnalysis;
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
        public async Task<List<ProjectRequest>> GetJoinRequestsByProjectIdAsync(int projectId)
        {
            return await _context.ProjectRequests
                .Include(r => r.Sender)
                .ThenInclude(s => s.User) // ضروري لتحميل FullName
                .Where(r => r.ProjectId == projectId && r.Type == RequestType.Join)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }  
        
        public async Task<List<ProjectRequest>> GetInvitationRequestsByProjectIdAsync(int projectId)
        {
            return await _context.ProjectRequests
                .Include(r => r.Receiver)
                .ThenInclude(s => s.User) // ضروري لتحميل FullName
                .Where(r => r.ProjectId == projectId && r.Type == RequestType.Invitation)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        //public async Task<List<ProjectRequest>> GetInvitationRequestsByResearcherIdAsync(string researcherId)
        //{
        //    return await _context.ProjectRequests
        //        .Include(r => r.Project)
        //            .ThenInclude(p => p.ProjectMemberships)
        //                .ThenInclude(pm => pm.Researcher)
        //                    .ThenInclude(r => r.User)
        //        .Where(r => r.ReceiverId == researcherId && r.Type == RequestType.Invitation)
        //        .OrderByDescending(r => r.CreatedAt)
        //        .ToListAsync();
        //}

        public async Task<List<ProjectRequest>> GetJoinRequestsBySenderAsync(string researcherId)
        {
            return await _context.ProjectRequests
                .Include(r => r.Project)
                .Where(r => r.SenderId == researcherId && r.Type == RequestType.Join)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<ProjectRequest>> GetInvitationsByLeaderAsync(string leaderId)
        {
            var projectIds = await _context.ProjectMemberships
                .Where(pm => pm.ResearcherId == leaderId && pm.Role == Role.Leader)
                .Select(pm => pm.ProjectId)
                .ToListAsync();

            return await _context.ProjectRequests
                .Include(r => r.Receiver).ThenInclude(u => u.User)
                .Include(r => r.Project)
                .Where(r => r.Type == RequestType.Invitation && projectIds.Contains(r.ProjectId))
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }
        public async Task<ProjectRequest> GetByIdWithRelationsAsync(int Id)
        {
            return await _context.ProjectRequests
            .Include(r => r.Sender).ThenInclude(u => u.User)
            .Include(r => r.Receiver).ThenInclude(u => u.User)
            .Include(r => r.Project)
            .FirstOrDefaultAsync(r => r.Id == Id);

        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
