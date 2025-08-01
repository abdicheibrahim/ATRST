using ProjetAtrst.Models;

namespace ProjetAtrst.Interfaces.Repositories
{
    public interface IProjectRequestRepository
    {
        Task CreateAsync(ProjectRequest request);
        Task<ProjectRequest> GetByIdAsync(int id);
        Task<IEnumerable<ProjectRequest>> GetByReceiverIdAsync(string userId);
        Task<IEnumerable<ProjectRequest>> GetBySenderIdAsync(string senderId);
        Task<IEnumerable<ProjectRequest>> GetByProjectIdAsync(int projectId);
        Task<IEnumerable<ProjectRequest>> GetByUserAndProjectAsync(string userId, int? projectId = null);
        Task<List<ProjectRequest>> GetJoinRequestsByProjectIdAsync(int projectId);
        Task<ProjectRequest> GetByIdWithRelationsAsync(int Id);
        Task<List<ProjectRequest>> GetInvitationRequestsByProjectIdAsync(int projectId);
        // Task<List<ProjectRequest>> GetInvitationRequestsByResearcherIdAsync(string researcherId);
        Task<List<ProjectRequest>> GetJoinRequestsBySenderAsync(string researcherId);
        // Task<List<ProjectRequest>> GetInvitationsByLeaderAsync(string leaderId);
        //Task<List<ProjectRequest>> GetInvitationsForUserAsync(string userId);
        Task<List<ProjectRequest>> GetInvitationsForUserAsync(string userId);
          Task SaveChangesAsync();
    }
}
