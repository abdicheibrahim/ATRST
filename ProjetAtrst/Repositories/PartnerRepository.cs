using ProjetAtrst.DTO;
using ProjetAtrst.Interfaces.Repositories;
using ProjetAtrst.ViewModels.Account;
using ProjetAtrst.ViewModels.Partner;

namespace ProjetAtrst.Repositories
{
    public class PartnerRepository : GenericRepository<Partner>, IPartnerRepository
    {
        public PartnerRepository(ApplicationDbContext context) : base(context) { }

       
        public async Task<PartnerDetailsViewModel?> GetPartnerDetailsAsync(string PartnerId)
        {
            return await _context.Partners
                .Where(p => p.Id == PartnerId)
                .Select(p => new PartnerDetailsViewModel
                {
                   
                    FullName = p.User.FullName,
                    FullNameAr = p.User.FirstNameAr + " " + p.User.LastNameAr,
                    Gender = p.User.Gender,
                    Birthday = p.User.Birthday,
                    Mobile = p.User.Mobile,
                    Baccalaureat = p.Baccalaureat,
                    Diploma = p.Diploma,
                    Profession = p.Profession,
                    Speciality = p.Speciality,
                    Establishment = p.Establishment,
                    PartnerResearchPrograms = p.PartnerResearchPrograms,
                    PartnerSocioEconomicWorks = p.PartnerSocioEconomicWorks
                })
                .FirstOrDefaultAsync();
        }
    
    }
}
