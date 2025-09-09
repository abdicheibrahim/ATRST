using ProjetAtrst.ViewModels.Partner;
using ProjetAtrst.ViewModels.Researcher;

namespace ProjetAtrst.Interfaces.Services
{
    public interface IPartnerService
    {

        Task<EditPartnerProfileViewModel?> GetEditProfilePartnerAsync(string userId);

        Task EditProfilePartnerAsync(string userId, EditPartnerProfileViewModel model);
        Task<PartnerDetailsViewModel?> GetPartnerDetailsAsync(string PartnerId);

    }
}
