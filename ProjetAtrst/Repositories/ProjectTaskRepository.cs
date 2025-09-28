using ProjetAtrst.Interfaces.Repositories;
using ProjetAtrst.ViewModels.ProjectTask;

namespace ProjetAtrst.Repositories
{
    public class ProjectTaskRepository : GenericRepository<ProjectTask>, IProjectTaskRepository
    {
        public ProjectTaskRepository(ApplicationDbContext context) : base(context) { }

        public async Task<ProjectTask?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<IEnumerable<ProjectTask>> GetByProjectIdAsync(int projectId)
        {
            return await _context.ProjectTasks
                .Where(t => t.ProjectId == projectId)
                .ToListAsync();
        }
        //public async Task<IEnumerable<ProjectTaskViewModel>> GetTaskViewModelsByProjectIdAsync(int projectId)
        //{
        //    var tasks = await _context.ProjectTasks
        //        .Where(t => t.ProjectId == projectId)
        //        .Select(t => new ProjectTaskViewModel
        //        {
        //            TaskId = t.TaskId,
        //            TaskName = t.TaskName,
        //            Description = t.Description,
        //            Status = t.Status,
        //            Priority = t.Priority,
        //            Progress = t.Progress,
        //            StartDate = t.StartDate,
        //            EndDate = t.EndDate,
        //            ProjectId = t.ProjectId
        //        })
        //        .ToListAsync();

        //    return tasks;
        //}

        //public async Task<ProjectTaskViewModel?> GetTaskViewModelByIdAsync(int taskId)
        //{
        //    var task = await _context.ProjectTasks
        //        .Where(t => t.TaskId == taskId)
        //        .Select(t => new ProjectTaskViewModel
        //        {
        //            TaskId = t.TaskId,
        //            TaskName = t.TaskName,
        //            Description = t.Description,
        //            Status = t.Status,
        //            Priority = t.Priority,
        //            Progress = t.Progress,
        //            StartDate = t.StartDate,
        //            EndDate = t.EndDate,
        //            ProjectId = t.ProjectId
        //        })
        //        .FirstOrDefaultAsync();

        //    return task;
        //}
    }
}
