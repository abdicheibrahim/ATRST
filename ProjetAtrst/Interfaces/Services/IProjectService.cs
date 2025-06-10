using ProjetAtrst.Models;
using ProjetAtrst.ViewModels;

namespace ProjetAtrst.Interfaces.Services
{
    public interface IProjectService
    {
        Task<bool> CreateProjectAsync(string userId, CreateProjectViewModel model);
        Task<IEnumerable<Project>> GetOpenProjectsForJoiningAsync(string userId);
        Task<ProjectLeader> GetLeaderByResearcherIdAsync(string userId);
    }
}
