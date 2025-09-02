using NuGet.Protocol.Plugins;
using ProjetAtrst.DTO;
using ProjetAtrst.Interfaces.Repositories;
using ProjetAtrst.Models;
using ProjetAtrst.ViewModels.Project;

namespace ProjetAtrst.Repositories
{
    public class ProjectRepository :GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext context) : base(context) { }
        
        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<(string ProjectTitle, string LeaderFullName)> GetProjectInfoAsync(int projectId)
        {
             var result = await _context.ProjectMemberships
            .Include(pm => pm.Project)
            .Include(pm => pm.User)
            .Where(pm => pm.ProjectId == projectId && pm.Role== Role.Leader)
            .Select(pm => new
            {
                ProjectTitle = pm.Project.Title,
                LeaderFullName = pm.User.FullName 
            })
            .FirstOrDefaultAsync();


            if (result == null)
                return (null, null);

            return (result.ProjectTitle, result.LeaderFullName);
        }
        //-- for get available projects for join--//
        private IQueryable<Project> BaseAvailableQuery(string researcherId)
        {
            return _context.Projects
                .Where(p =>
                    p.ProjectStatus == ProjectStatus.Open &&
                    p.IsAcceptingJoinRequests &&
                    !_context.ProjectMemberships.Any(pm => pm.UserId == researcherId && pm.ProjectId == p.Id) &&
                    !_context.ProjectRequests.Any(r => r.SenderId == researcherId && r.ProjectId == p.Id)
                );
        }
        public IQueryable<AvailableProjectDto> GetAvailableProjectsQuery(string researcherId)
        {
            return BaseAvailableQuery(researcherId)
                .Select(p => new AvailableProjectDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    CreationDate = p.CreationDate,
                    ImageUrl = p.LogoPath,

                    LeaderId = p.ProjectMemberships
                        .Where(pm => pm.Role == Role.Leader)
                        .Select(pm => pm.UserId)
                        .FirstOrDefault(),

                    LeaderFullName = p.ProjectMemberships
                        .Where(pm => pm.Role == Role.Leader)
                        .Select(pm => pm.User.FullName)
                        .FirstOrDefault()
                })
                .AsNoTracking();
        }
        public async Task<(List<AvailableProjectDto> Projects, int TotalCount)> GetAvailableProjectsForJoinAsync(string researcherId, int pageNumber, int pageSize)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            var baseQuery = BaseAvailableQuery(researcherId);

            var totalCount = await baseQuery.CountAsync();

            // ترتيب ثابت قبل Skip/Take (مثلاً الأحدث أولاً)
            var page = await baseQuery
                .OrderByDescending(p => p.CreationDate)
                .Select(p => new AvailableProjectDto
                {
                    Id = p.Id,
                    Title = p.Title,
                   // Description = p.Description,
                    CreationDate = p.CreationDate,
                    ImageUrl = p.LogoPath,
                    LeaderId = p.ProjectMemberships
                        .Where(pm => pm.Role == Role.Leader)
                        .Select(pm => pm.UserId)
                        .FirstOrDefault(),
                    LeaderFullName = p.ProjectMemberships
                        .Where(pm => pm.Role == Role.Leader)
                        .Select(pm => pm.User.FullName)
                        .FirstOrDefault()
                })
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (page, totalCount);
        }
        //---------------------//
        public async Task<ProjectDetailsDto?> GetProjectDetailsAsync(int projectId)
        {
            return await _context.Projects
                .Where(p => p.Id == projectId)
                .Select(p => new ProjectDetailsDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    //Description = p.Description,
                    CreationDate = p.CreationDate,
                    ImageUrl = p.LogoPath, // تأكد من اسم الخاصية عندك
                    LeaderId = p.ProjectMemberships
                        .Where(pm => pm.Role == Role.Leader)
                        .Select(pm => pm.UserId)
                        .FirstOrDefault(),
                    LeaderFullName = p.ProjectMemberships
                        .Where(pm => pm.Role == Role.Leader)
                        .Select(pm => pm.User.FullName)
                        .FirstOrDefault()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        //--- New methods for project management ---//
        public async Task<Project?> GetProjectForEditAsync(int projectId)
        {
            return await _context.Projects
                .Include(p => p.ProjectMemberships)
                .FirstOrDefaultAsync(p => p.Id == projectId);
        }
        public async Task<bool> UpdateProjectAsync(Project project)
        {
            try
            {
                _context.Projects.Update(project);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> IsUserProjectLeaderAsync(int projectId, string userId)
        {
            return await _context.ProjectMemberships
                .AnyAsync(pm => pm.ProjectId == projectId &&
                              pm.UserId == userId &&
                              pm.Role == Role.Leader);
        }
        public async Task<Project?> GetProjectWithMembersAsync(int projectId)
        {
            return await _context.Projects
                .Include(p => p.ProjectMemberships)
                .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(p => p.Id == projectId);
        }
        //-------------------//
    }
}
