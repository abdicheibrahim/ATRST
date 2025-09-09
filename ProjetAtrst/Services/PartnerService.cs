using ProjetAtrst.Interfaces;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.ViewModels.Partner;
using ProjetAtrst.ViewModels.Researcher;
namespace ProjetAtrst.Services
{
    public class PartnerService: IPartnerService
    {
        private readonly IUnitOfWork _unitOfWork;
         public PartnerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task EditProfilePartnerAsync(string userId, EditPartnerProfileViewModel model)
        {
            var partner = await _unitOfWork.Partners.GetByIdAsync(userId);
            if (partner == null)
                return;

            partner.Baccalaureat = model.Baccalaureat;
            partner.Diploma = model.Diploma;
            partner.Profession = model.Profession;
            partner.Speciality = model.Speciality;
            partner.Establishment = model.Establishment;
            partner.PartnerResearchPrograms = model.PartnerResearchPrograms;
            partner.PartnerSocioEconomicWorks = model.PartnerSocioEconomicWorks;

            _unitOfWork.Partners.Update(partner);
            await _unitOfWork.SaveAsync();


        }
        public async Task<EditPartnerProfileViewModel?> GetEditProfilePartnerAsync(string userId)
        {
            var Partner = await _unitOfWork.Partners.GetByIdAsync(userId);
            if (Partner == null)
                return null;

            return new EditPartnerProfileViewModel
            {
                Baccalaureat = Partner.Baccalaureat,
                Diploma = Partner.Diploma,
                Profession = Partner.Profession,
                Speciality = Partner.Speciality,
                Establishment = Partner.Establishment,
                PartnerResearchPrograms = Partner.PartnerResearchPrograms,
                PartnerSocioEconomicWorks = Partner.PartnerSocioEconomicWorks

            };
        }

        public Task<PartnerDetailsViewModel?> GetPartnerDetailsAsync(string PartnerId)
        {
            return _unitOfWork.Partners.GetPartnerDetailsAsync(PartnerId);
        }
    }
}
