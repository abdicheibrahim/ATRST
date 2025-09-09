using ProjetAtrst.ViewModels.Partner;
using ProjetAtrst.ViewModels.Researcher;

namespace ProjetAtrst.Interfaces.Services
{
    public interface IResearcherService
    {
        Task<EditResearcherProfileViewModel?> GetEditProfileResearcherViewModelAsync(string userId);
        Task EditProfileResearcherViewModelAsync(string userId, EditResearcherProfileViewModel model);
        Task<(List<ResearcherViewModel> Researchers, int TotalCount)> GetAvailableResearchersForInvitationAsync(int projectId, int page, int pageSize);
        Task<ResearcherDetailsViewModel?> GetResearcherDetailsAsync(string PartnerId);

    }
}