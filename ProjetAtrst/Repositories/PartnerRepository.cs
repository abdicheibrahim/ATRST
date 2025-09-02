using ProjetAtrst.Interfaces.Repositories;

namespace ProjetAtrst.Repositories
{
    public class PartnerRepository : GenericRepository<Partner>, IPartnerRepository
    {
        public PartnerRepository(ApplicationDbContext context) : base(context) { }
    }
}
