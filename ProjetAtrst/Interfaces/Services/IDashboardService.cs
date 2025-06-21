using ProjetAtrst.ViewModels.Dashboard;

namespace ProjetAtrst.Interfaces.Services
{
    public interface IDashboardService
    {
        Task<List<MyLeaderProjectsViewModel>> GetMyLeaderProjectsAsync(string researcherId);
        Task<List<MyJoinedProjectsViewModel>> GetMyJoinedProjectsAsync(string researcherId);
    }
}
