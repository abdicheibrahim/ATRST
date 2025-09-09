using ProjetAtrst.DTO;
using ProjetAtrst.ViewModels.Partner;

namespace ProjetAtrst.Interfaces.Repositories
{
    public interface IPartnerRepository : IGenericRepository<Partner>
    {
        Task<PartnerDetailsViewModel?> GetPartnerDetailsAsync(string PartnerId);
    }
}
