using ProjetAtrst.ViewModels.ProjectTask;
namespace ProjetAtrst.Interfaces.Services
{
    public interface IProjectTaskService
    {
        //Task<IEnumerable<ProjectTaskViewModel>> GetTasksByProjectIdAsync(int projectId);
        //Task<ProjectTaskViewModel?> GetTaskByIdAsync(int taskId);
        //Task<bool> CreateAsync(ProjectTaskViewModel taskViewModel);
        //Task<bool> UpdateAsync(ProjectTaskViewModel taskViewModel);
        //Task<bool> DeleteAsync(int taskId);
        Task<IEnumerable<ProjectTask>> GetTasksByProjectIdAsync(int projectId);
        Task<ProjectTask?> GetTaskByIdAsync(int Id);
        Task<bool> CreateAsync(ProjectTaskViewModel taskViewModel);
        Task<bool> UpdateAsync(int taskId, ProjectTaskViewModel taskViewModel);
        Task<bool> DeleteAsync(int taskId);
    }
}
