using ProjetAtrst.ViewModels.Researcher;

namespace ProjetAtrst.Interfaces.Services
{
    public interface IUserService
    {
        Task CompleteUserProfileAsync(string userId, CompleteProfileViewModel model);
        Task<CompleteProfileViewModel?> GetCompleteProfileViewModelAsync(string userId);

    }

}
