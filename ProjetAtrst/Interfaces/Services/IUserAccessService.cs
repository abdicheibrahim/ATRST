using ProjetAtrst.Models;
using ProjetAtrst.ViewModels.UserAccess;
using System.Threading.Tasks;

namespace ProjetAtrst.Interfaces.Services
{
    public interface IUserAccessService
    {
        Task<UserAccessStatusViewModel> GetAccessStatusAsync(string userId);
        string? GetUserId();
       

    }
}
