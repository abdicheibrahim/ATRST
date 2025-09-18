using ProjetAtrst.ViewModels.ProjectTask;
namespace ProjetAtrst.Interfaces.Services
{
    public interface IProjectTaskService
    {
        Task<ProjectTask> CreateAsync(ProjectTaskViewModel Task);
    }
}
