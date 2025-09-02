using ProjetAtrst.ViewModels.Associate;
using ProjetAtrst.ViewModels.Partner;

namespace ProjetAtrst.Interfaces.Services
{
    public interface IAssociateService
    {
        Task<EditAssociateProfileViewModel?> GetEditProfileAssociateAsync(string userId);

        Task EditProfileAssociateAsync(string userId, EditAssociateProfileViewModel model);
    }
}
