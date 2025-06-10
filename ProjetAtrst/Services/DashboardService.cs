using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.Interfaces;
using ProjetAtrst.ViewModels;

namespace ProjetAtrst.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DashboardViewModel> GetDashboardDataAsync(string userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            var researcher = await _unitOfWork.Researchers.GetByUserIdAsync(userId);

            var projectsLed = await _unitOfWork.Projects.GetLedProjectsAsync(userId, 2);
            var projectsMember = await _unitOfWork.Projects.GetMemberProjectsAsync(userId, 2);

            var model = new DashboardViewModel
            {
                UserName = user.FirstName+" "+user.LastName,
                ProjectsLedCount = await _unitOfWork.Projects.CountLedByAsync(userId),
                ProjectsMemberCount = await _unitOfWork.Projects.CountMemberInAsync(userId),
                CanCreateProject = await _unitOfWork.Researchers.CanCreateProjectAsync(researcher.Id),
                LedProjects = projectsLed.ToList(),
                MemberProjects = projectsMember.ToList(),
                PendingJoinRequestsCount = await _unitOfWork.JoinRequests.CountPendingForLeaderAsync(userId)
            };

            return model;
        }
    }
}
