using ProjetAtrst.Interfaces.Repositories;

namespace ProjetAtrst.Repositories
{
    public class ProjectTaskAssignmentRepository : IProjectTaskAssignmentRepository
    {
        private readonly ApplicationDbContext _db;
        public ProjectTaskAssignmentRepository(ApplicationDbContext db) => _db = db;

        public async Task AddAsync(ProjectTaskAssignment entity) =>
            await _db.ProjectTaskAssignments.AddAsync(entity);

        public async Task<ProjectTaskAssignment> GetByIdAsync(int id) =>
            await _db.ProjectTaskAssignments
                     .Include(a => a.AssignedUser)
                     .Include(a => a.Task)
                     .FirstOrDefaultAsync(a => a.ProjectTaskAssignmentId == id);

        public async Task<IEnumerable<ProjectTaskAssignment>> GetByTaskIdAsync(int taskId) =>
            await _db.ProjectTaskAssignments
                     .Include(a => a.AssignedUser)
                     .Where(a => a.TaskId == taskId)
                     .ToListAsync();

        public void Remove(ProjectTaskAssignment entity) => _db.ProjectTaskAssignments.Remove(entity);
        public async Task<IEnumerable<ProjectTask>> GetTasksByUserIdAsync(string userId)
        {
            return await _db.ProjectTaskAssignments
                .Include(a => a.Task)
                    .ThenInclude(t => t.Project)
                .Where(a => a.AssignedUserId == userId)
                .Select(a => a.Task)
                .ToListAsync();
        }

    }


}
