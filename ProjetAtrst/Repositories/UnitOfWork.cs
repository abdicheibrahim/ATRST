using ProjetAtrst.Interfaces;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.Interfaces.Repositories;

namespace ProjetAtrst.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IResearcherRepository Researchers { get; }
        public IUserRepository Users {  get; }
        public INotificationRepository Notifications { get; }
        public IProjectRepository Projects { get; }
        public IJoinRequestRepository JoinRequests { get; }
        public IProjectLeaderRepository ProjectLeader { get; }
        public IInvitationRequestRepository InvitationRequest { get; }
        public IProjectMembershipRepository ProjectMembership { get; }

        public UnitOfWork(ApplicationDbContext context, IResearcherRepository researcherRepository, IUserRepository userRepository,
            INotificationRepository NotificationRepository, IProjectRepository projectsRepository, IJoinRequestRepository joinRequestsRepository,
            IProjectLeaderRepository ProjectLeaderRepository, IInvitationRequestRepository invitationRequestRepository, IProjectMembershipRepository projectMembershipRepository)
        {
            _context = context;
            Researchers = researcherRepository;
            Users = userRepository;
            Notifications = NotificationRepository;
            Projects = projectsRepository;
            JoinRequests = joinRequestsRepository;
            ProjectLeader = ProjectLeaderRepository;
            InvitationRequest = invitationRequestRepository;
            ProjectMembership = projectMembershipRepository;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
