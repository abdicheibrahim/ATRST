using ProjetAtrst.Interfaces.Repositories;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.Models;
using ProjetAtrst.ViewModels.Dashboard;

namespace ProjetAtrst.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IProjectMembershipRepository _projectMembershipRepo;

        public DashboardService(IProjectMembershipRepository projectMembershipRepo)
        {
            _projectMembershipRepo = projectMembershipRepo;
        }

        public async Task<List<MyLeaderProjectsViewModel>> GetMyLeaderProjectsAsync(string researcherId)
        {
            var memberships = await _projectMembershipRepo.GetProjectsByResearcherWithDetailsAsync(researcherId);

            var leaderProjects = memberships
                .Where(pm => pm.Role == Role.Leader)
                .Select(pm => new MyLeaderProjectsViewModel
                {
                    ProjectId = pm.Project.Id,
                    ProjectTitle = pm.Project.Title,
                    MemberCount = pm.Project.ProjectMemberships.Count,
                    RelatedNotificationsCount =
                        pm.Project.ProjectRequests.Count(r => r.Status == RequestStatus.Pending),
                    ImageUrl = null // يمكن إضافة صورة لاحقًا
                })
                .ToList();

            return leaderProjects;
        }

        public async Task<List<MyJoinedProjectsViewModel>> GetMyJoinedProjectsAsync(string researcherId)
        {
            var memberships = await _projectMembershipRepo.GetProjectsByResearcherWithDetailsAsync(researcherId);

            var joinedProjects = memberships
                .Where(pm => pm.Role != Role.Leader)
                .Select(pm => new MyJoinedProjectsViewModel
                {
                    ProjectId = pm.Project.Id,
                    ProjectTitle = pm.Project.Title,
                    LeaderFullName = pm.Project.ProjectMemberships
                        .Where(m => m.Role == Role.Leader)
                        .Select(m => m.Researcher.User.FullName)
                        .FirstOrDefault() ?? "غير معروف",
                    ImageUrl = null
                })
                .ToList();

            return joinedProjects;
        }
    }
}
