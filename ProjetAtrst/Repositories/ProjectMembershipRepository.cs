using ProjetAtrst.Interfaces.Repositories;
using ProjetAtrst.Models;

namespace ProjetAtrst.Repositories
{
    public class ProjectMembershipRepository : GenericRepository<ProjectMembership>, IProjectMembershipRepository
    {
        public ProjectMembershipRepository (ApplicationDbContext context) : base(context) { }
        public async Task<List<ProjectMember>> GetEligibleForInvitationAsync(int projectId)
        {
            return await _context.ProjectMembers
                .Include(m => m.Researcher).ThenInclude(r => r.User)
                .Where(m =>
                    !m.ProjectMemberships.Any(pm => pm.ProjectId == projectId) &&
                    !m.ReceivedInvitations.Any(i => i.TargetProjectId == projectId))
                .ToListAsync();
        }

    }

}
