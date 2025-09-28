using ProjetAtrst.Interfaces.Repositories;

namespace ProjetAtrst.Repositories
{
    public class AdminRepository : GenericRepository<Admin>, IAdminRepository
    {
        public AdminRepository(ApplicationDbContext context) : base(context) { }
    }
}
