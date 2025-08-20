using ProjetAtrst.ViewModels.Account;

namespace ProjetAtrst.Interfaces.Services
{
    public interface IUserService
    {
        Task<bool> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
        Task CompleteProfileAsync(string userId, CompleteProfileViewModel model);
        Task<CompleteProfileViewModel?> GetCompleteProfileViewModelAsync(string userId);

        Task<EditProfileViewModel?> GetEditProfileViewModelAsync(string userId);
        Task EditProfileAsync(string userId, EditProfileViewModel model);
    }

}
