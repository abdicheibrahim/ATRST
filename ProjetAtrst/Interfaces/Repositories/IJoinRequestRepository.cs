using ProjetAtrst.Models;

namespace ProjetAtrst.Interfaces.Repositories
{
    public interface IJoinRequestRepository
    {
        Task<List<JoinRequest>> GetJoinRequestsToMyProjectsAsync(string leaderId);
        Task<List<JoinRequest>> GetJoinRequestsISentAsync(string researcherId);
        Task<List<IGrouping<int, JoinRequest>>> GetGroupedJoinRequestsToMyProjectsAsync(string leaderId);
        //New
        Task<JoinRequest?> GetByProjectAndResearcherAsync(int projectId, string researcherId);
        Task AddAsync(JoinRequest joinRequest);

    }
}
