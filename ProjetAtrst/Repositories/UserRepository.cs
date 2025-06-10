using ProjetAtrst.Models;
using ProjetAtrst.Interfaces.Repositories;
namespace ProjetAtrst.Repositories
{
    public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }

        public async Task<ApplicationUser?> GetUserWithResearcherAsync(string userId)
        {
            return await _context.Users
                .Include(u => u.Researcher)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }
       
    }


}
