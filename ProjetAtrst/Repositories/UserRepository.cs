using ProjetAtrst.DTO;
using ProjetAtrst.Interfaces.Repositories;
using ProjetAtrst.Models;
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

        public async Task<ApplicationUser?> GetUserWithDetailsAsync(string userId)
        {
            return await _context.Users
                .Include(u => u.Researcher)
                .Include(u => u.Partner)
               .Include(u => u.Associate)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }
        public async Task<RoleType> GetRoleAsync(string userId)
        {
            return await _context.Users
                .Where(u => u.Id == userId)
                .Select(u => u.RoleType) 
                .FirstOrDefaultAsync();
        }
    }


}
