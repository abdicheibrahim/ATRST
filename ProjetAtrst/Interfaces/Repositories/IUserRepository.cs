using ProjetAtrst.Models;
using ProjetAtrst.Repositories;

namespace ProjetAtrst.Interfaces.Repositories
{ 
    public interface IUserRepository : IGenericRepository<ApplicationUser>
    {
        Task<ApplicationUser?> GetUserWithResearcherAsync(string userId);
        

    }

}
