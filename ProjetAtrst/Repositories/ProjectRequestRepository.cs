using Microsoft.CodeAnalysis;
using ProjetAtrst.Interfaces.Repositories;
using ProjetAtrst.Models;
using ProjetAtrst.ViewModels.ProjectRequests;

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
                .Include(r => r.Receiver)
                .Where(r => r.ReceiverId == userId)
                .ToListAsync();
        }
        public async Task<IEnumerable<ProjectRequest>> GetBySenderIdAsync(string senderId)
        {
            return await _context.ProjectRequests
                .Include(r => r.Receiver)
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
                .Where(r => r.ProjectId == projectId && r.Type == RequestType.Join)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }  
        
        public async Task<List<ProjectRequest>> GetInvitationRequestsByProjectIdAsync(int projectId)
        {
            return await _context.ProjectRequests
                .Include(r => r.Receiver)
                .Where(r => r.ProjectId == projectId && r.Type == RequestType.Invitation)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

       
        public async Task<List<ProjectRequest>> GetJoinRequestsBySenderAsync(string researcherId)
        {
            return await _context.ProjectRequests
                .Where(r => r.SenderId == researcherId
                            && r.Project.ProjectMemberships
                                .Any(pm => pm.UserId == r.ReceiverId && pm.Role == Role.Leader)
                            && r.Type == RequestType.Join) // أو النوع الذي تستعمله للانضمام
                .Include(r => r.Receiver)
                    .Include(r => r.Project)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<ProjectRequest>> GetInvitationsForUserAsync(string userId)
        {
            return await _context.ProjectRequests
                .Include(r => r.Sender) // القائد الذي أرسل الدعوة
                .Include(r => r.Project)
                .Where(r => r.Type == RequestType.Invitation && r.ReceiverId == userId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }
        //------------------------------------------------
        //public async Task<ProjectRequest> GetByIdWithRelationsAsync(int Id)
        //{
        //    return await _context.ProjectRequests
        //    .Include(r => r.Sender)
        //    .Include(r => r.Receiver)
        //    .Include(r => r.Project)
        //    .FirstOrDefaultAsync(r => r.Id == Id);

        //}
        public async Task<ProjectRequestDetailsViewModel?> GetByIdWithRelationsAsync(int id)
        {
            return await _context.ProjectRequests
                .Where(r => r.Id == id)
                .Select(r => new ProjectRequestDetailsViewModel
                {
                    ProjectTitle = r.Project.Title,
                    SenderName = r.Sender.FullName,
                    ReceiverName = r.Receiver.FullName,
                    Message = r.Message,
                    RelatedWorks = r.RelatedWorks,
                    Type = r.Type,
                    Status = r.Status,
                    CreatedAt = r.CreatedAt
                })
                .FirstOrDefaultAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
