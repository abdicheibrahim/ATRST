using ProjetAtrst.ViewModels;

namespace ProjetAtrst.Interfaces.Services
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetDashboardDataAsync(string userId);
    }
}
