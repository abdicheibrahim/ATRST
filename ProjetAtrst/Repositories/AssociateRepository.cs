using ProjetAtrst.Interfaces.Repositories;

namespace ProjetAtrst.Repositories
{
   
    public class AssociateRepository : GenericRepository<Associate>, IAssociateRepository
    {
        public AssociateRepository(ApplicationDbContext context) : base(context) { }
    }
    
}
