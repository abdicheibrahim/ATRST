using ProjetAtrst.ViewModels.Associate;
using ProjetAtrst.ViewModels.Partner;
using ProjetAtrst.ViewModels.Researcher;

namespace ProjetAtrst.Interfaces.Services
{
    public interface IAssociateService
    {
        Task<EditAssociateProfileViewModel?> GetEditProfileAssociateAsync(string userId);

        Task EditProfileAssociateAsync(string userId, EditAssociateProfileViewModel model);
        Task<AssociateDetailsViewModel?> GetAssociateDetailsAsync(string AssociateId);

    }
}
