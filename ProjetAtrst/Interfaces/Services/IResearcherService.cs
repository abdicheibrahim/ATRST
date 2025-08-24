using Microsoft.AspNetCore.Identity;
using ProjetAtrst.ViewModels.Account;
using ProjetAtrst.ViewModels.Researcher;

namespace ProjetAtrst.Interfaces.Services
{
    public interface IResearcherService
    {
       
        //new
        Task<EditResearcherProfileViewModel?> GetEditProfileResearcherViewModelAsync(string userId);

        Task EditProfileResearcherViewModelAsync(string userId, EditResearcherProfileViewModel model);
       

        Task<(List<ResearcherViewModel> Researchers, int TotalCount)> GetAvailableResearchersForInvitationAsync(int projectId, int page, int pageSize);


    }
}