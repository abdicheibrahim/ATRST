using ProjetAtrst.Models;

namespace ProjetAtrst.Interfaces.Repositories
{
    public interface IProjectRepository:IGenericRepository<Project>
    {
     
       

        Task<int> CountLedByAsync(string userId);
        Task<int> CountMemberInAsync(string userId);
        Task<IEnumerable<Project>> GetLedProjectsAsync(string userId, int takeCount);
        Task<IEnumerable<Project>> GetMemberProjectsAsync(string userId, int takeCount);
        Task<IEnumerable<Project>> GetOpenProjectsForJoiningAsync(string researcherId);
        

    }
}
