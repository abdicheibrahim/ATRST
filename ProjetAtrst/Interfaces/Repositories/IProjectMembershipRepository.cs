using ProjetAtrst.Models;

namespace ProjetAtrst.Interfaces.Repositories
{
    public interface IProjectMembershipRepository : IGenericRepository<ProjectMembership>
    {
        Task<List<ProjectMember>> GetEligibleForInvitationAsync(int projectId);

    }
}
