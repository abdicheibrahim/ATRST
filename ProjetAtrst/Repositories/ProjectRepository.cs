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
        
        //Verified
        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        // delete
        //public async Task<(List<Project> Projects, int TotalCount)> GetAvailableProjectsForJoinAsync(string researcherId, int pageNumber, int pageSize)
        //{
        //    var joinedProjectIds = await _context.ProjectMemberships
        //        .Where(pm => pm.ResearcherId == researcherId)
        //        .Select(pm => pm.ProjectId)
        //        .ToListAsync();

        //    var requestedProjectIds = await _context.ProjectRequests
        //        .Where(j => j.SenderId == researcherId)
        //        .Select(j => j.ProjectId)
        //        .ToListAsync();

        //    var query = _context.Projects
        //        .Where(p =>
        //            p.ProjectStatus == ProjectStatus.Open &&
        //            p.IsAcceptingJoinRequests &&
        //            !joinedProjectIds.Contains(p.Id) &&
        //            !requestedProjectIds.Contains(p.Id)
        //        )
        //        .Include(p => p.ProjectMemberships)
        //            .ThenInclude(pm => pm.Researcher)
        //                .ThenInclude(r => r.User);

        //    var totalCount = await query.CountAsync();

        //    var projects = await query
        //        .Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToListAsync();

        //    return (projects, totalCount);
        //}

        //

        public async Task<(string ProjectTitle, string LeaderFullName)> GetProjectInfoAsync(int projectId)
        {
             var result = await _context.ProjectMemberships
            .Include(pm => pm.Project)
            .Include(pm => pm.Researcher)
            .ThenInclude(r => r.User)
            .Where(pm => pm.ProjectId == projectId && pm.Role== Role.Leader)
            .Select(pm => new
            {
                ProjectTitle = pm.Project.Title,
                LeaderFullName = pm.Researcher.User.FullName 
            })
            .FirstOrDefaultAsync();


            if (result == null)
                return (null, null);

            return (result.ProjectTitle, result.LeaderFullName);
        }


        // new

        // أساس الاستعلام (بدون Paging) – نستخدم NOT EXISTS لتفادي تحميل قوائم IDs
        private IQueryable<Project> BaseAvailableQuery(string researcherId)
        {
            return _context.Projects
                .Where(p =>
                    p.ProjectStatus == ProjectStatus.Open &&
                    p.IsAcceptingJoinRequests &&
                    !_context.ProjectMemberships.Any(pm => pm.ResearcherId == researcherId && pm.ProjectId == p.Id) &&
                    !_context.ProjectRequests.Any(r => r.SenderId == researcherId && r.ProjectId == p.Id)
                );
        }

        // إسقاط (Projection) إلى DTO مع استخراج الـ Leader عبر الـ Memberships
        public IQueryable<AvailableProjectDto> GetAvailableProjectsQuery(string researcherId)
        {
            return BaseAvailableQuery(researcherId)
                .Select(p => new AvailableProjectDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    //Description = p.Description,
                    CreationDate = p.CreationDate,
                    ImageUrl = p.LogoPath,

                    LeaderId = p.ProjectMemberships
                        .Where(pm => pm.Role == Role.Leader)
                        .Select(pm => pm.ResearcherId)
                        .FirstOrDefault(),

                    LeaderFullName = p.ProjectMemberships
                        .Where(pm => pm.Role == Role.Leader)
                        .Select(pm => pm.Researcher.User.FullName)
                        .FirstOrDefault()
                })
                .AsNoTracking();
        }

        public async Task<(List<AvailableProjectDto> Projects, int TotalCount)>
            GetAvailableProjectsForJoinAsync(string researcherId, int pageNumber, int pageSize)
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
                        .Select(pm => pm.ResearcherId)
                        .FirstOrDefault(),
                    LeaderFullName = p.ProjectMemberships
                        .Where(pm => pm.Role == Role.Leader)
                        .Select(pm => pm.Researcher.User.FullName)
                        .FirstOrDefault()
                })
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (page, totalCount);
        }

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
                        .Select(pm => pm.ResearcherId)
                        .FirstOrDefault(),
                    LeaderFullName = p.ProjectMemberships
                        .Where(pm => pm.Role == Role.Leader)
                        .Select(pm => pm.Researcher.User.FullName)
                        .FirstOrDefault()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}
