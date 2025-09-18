using ProjetAtrst.Interfaces.Repositories;

namespace ProjetAtrst.Repositories
{
    public class ProjectTaskRepository : GenericRepository<ProjectTask>, IProjectTaskRepository
    {
        public ProjectTaskRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<ProjectTask>> GetByProjectIdAsync(int projectId) =>
                await _context.ProjectTasks
               .Where(t => t.ProjectId == projectId)
               .ToListAsync();

    }
}
