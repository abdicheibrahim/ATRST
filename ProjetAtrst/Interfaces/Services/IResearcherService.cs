using Microsoft.AspNetCore.Identity;
using ProjetAtrst.ViewModels.Account;
using ProjetAtrst.ViewModels.Researcher;

namespace ProjetAtrst.Interfaces.Services
{
    public interface IResearcherService
    {
        Task<IdentityResult> RegisterNewResearcherAsync(RegisterViewModel model);
        //new
        Task<EditResearcherProfileViewModel?> GetEditProfileResearcherViewModelAsync(string userId);

        Task EditProfileResearcherViewModelAsync(string userId, EditResearcherProfileViewModel model);
        Task<bool> IsProfileCompleteAsync(string userId);

        Task<List<ResearcherViewModel>> GetAvailableResearchersForInvitationAsync(int projectId);

    }
}