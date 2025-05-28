using ProjetAtrst.ViewModel.Researcher;

namespace ProjetAtrst.Services.Interfaces
{
    public interface IResearcherService
    {
        Task<ResearcherProfileViewModel> GetProfileAsync(string userId);
        Task UpdateProfileAsync(string userId, ResearcherProfileViewModel model);
    }
}
