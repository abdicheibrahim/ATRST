using ProjetAtrst.Models;
using ProjetAtrst.Repositories;

namespace ProjetAtrst.Interfaces.Repositories
{
    public interface IResearcherRepository : IGenericRepository<Researcher>
    {
       
        Task<bool> CanCreateProjectAsync(string researcherId);
        Task<Researcher?> GetByUserIdAsync(string userId);
       


    }
}
