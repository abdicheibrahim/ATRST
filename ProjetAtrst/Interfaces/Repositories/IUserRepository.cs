using ProjetAtrst.Models;
using ProjetAtrst.Repositories;

namespace ProjetAtrst.Interfaces.Repositories
{ 
    public interface IUserRepository : IGenericRepository<ApplicationUser>
    {
        Task<ApplicationUser?> GetUserWithResearcherAsync(string userId);

        Task<ApplicationUser?> GetUserWithDetailsAsync(string userId);
        Task<RoleType> GetRoleAsync(string userId);
    }

}
