using ProjetAtrst.ViewModels.ProjectTask;
namespace ProjetAtrst.Interfaces.Services
{
    public interface IProjectTaskService
    {
        Task<IEnumerable<ProjectTask>> GetTasksByProjectIdAsync(int projectId);
        Task<ProjectTask?> GetTaskByIdAsync(int Id);
        Task<bool> CreateAsync(ProjectTaskViewModel taskViewModel);
        Task<bool> UpdateAsync(int taskId, ProjectTaskViewModel taskViewModel);
        Task<bool> DeleteAsync(int taskId);
    }
}
