using ProjetAtrst.Interfaces.Repositories;
using ProjetAtrst.ViewModels.Associate;
using ProjetAtrst.ViewModels.Researcher;

namespace ProjetAtrst.Repositories
{
   
    public class AssociateRepository : GenericRepository<Associate>, IAssociateRepository
    {
        public AssociateRepository(ApplicationDbContext context) : base(context) { }
        public async Task<AssociateDetailsViewModel?> GetAssociateDetailsAsync(string ResearcherId)
        {
            return await _context.Associates
                .Where(p => p.Id == ResearcherId)
                .Select(p => new AssociateDetailsViewModel
                {

                    FullName = p.User.FullName,
                    FullNameAr = p.User.FirstNameAr + " " + p.User.LastNameAr,
                    Gender = p.User.Gender,
                    Birthday = p.User.Birthday,
                    Mobile = p.User.Mobile,
                    Diploma = p.Diploma,
                    Speciality = p.Speciality,
                    MemberParticipation = p.MemberParticipation

                })
                .FirstOrDefaultAsync();
        }
    }
    
}
