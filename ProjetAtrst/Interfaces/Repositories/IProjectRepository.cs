using ProjetAtrst.Models;

namespace ProjetAtrst.Interfaces.Repositories
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        //Task<Project?> GetLeaderProjectAsync(string researcherId);
        Task<Project?> GetByIdAsync(int id);
        Task<List<Project>> GetAvailableProjectsForJoinAsync(string researcherId);


    }
}
