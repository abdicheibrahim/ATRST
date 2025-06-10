using Microsoft.AspNetCore.Identity;
using ProjetAtrst.ViewModels.Identity;
using ProjetAtrst.ViewModels.Researcher;
using System.Security.Claims;

namespace ProjetAtrst.Interfaces.Services
{
    public interface IResearcherService
    {
        Task<IdentityResult> RegisterNewResearcherAsync(RegisterViewModel model);
        Task<bool> LoginAsync(LoginViewModel model);
        Task<CompleteProfileViewModel?> GetDashboardAsync(ClaimsPrincipal user);
        Task LogoutAsync();
    }
}