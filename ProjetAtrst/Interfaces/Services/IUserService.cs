using Microsoft.AspNetCore.Identity;
using ProjetAtrst.ViewModels.Account;

namespace ProjetAtrst.Interfaces.Services
{
    public interface IUserService
    {
        Task<bool> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
        Task<IdentityResult> RegisterNewAccountAsync(RegisterViewModel model);
        Task CompleteProfileAsync(string userId, CompleteProfileViewModel model);
        Task<CompleteProfileViewModel?> GetCompleteProfileViewModelAsync(string userId);

        Task<EditProfileViewModel?> GetEditProfileViewModelAsync(string userId);
        Task EditProfileAsync(string userId, EditProfileViewModel model);
        Task<bool> IsProfileCompleteAsync(string userId);
        Task<RoleType> GetRoleAsync(string userId);
        //....
       Task<List<ApplicationUser>> GetAllAvailableUsersAsync(int projectId);
        Task<List<string>> GetInvitedOrMembersIdsAsync(int projectId);
        Task<List<ApplicationUser>> GetAvailableUsersAsync(List<string> excludedIds, int start, int pageSize);
        Task<int> GetAvailableUsersCountAsync(List<string> excludedIds);
        Task<List<ApplicationUser>> GetAvailableUsersAsync(List<string> excludedIds, int start, int pageSize, string searchValue = null, string sortColumn = null, string sortDirection = null);
        Task<int> GetAvailableUsersCountAsync(List<string> excludedIds, string searchValue = null);
       
    }

}
