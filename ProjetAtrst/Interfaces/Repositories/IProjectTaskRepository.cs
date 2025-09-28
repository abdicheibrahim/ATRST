using ProjetAtrst.ViewModels.ProjectTask;

namespace ProjetAtrst.Interfaces.Repositories
{
    public interface IProjectTaskRepository : IGenericRepository<ProjectTask>
    {
        Task<IEnumerable<ProjectTask>> GetByProjectIdAsync(int projectId);
        //Task<IEnumerable<ProjectTaskViewModel>> GetTaskViewModelsByProjectIdAsync(int projectId);
        //Task<ProjectTaskViewModel?> GetTaskViewModelByIdAsync(int taskId);
        Task<ProjectTask?> GetByIdAsync(int id);
    }
}

