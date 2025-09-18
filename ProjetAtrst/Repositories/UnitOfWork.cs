using ProjetAtrst.Interfaces;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.Interfaces.Repositories;

namespace ProjetAtrst.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IResearcherRepository Researchers { get; }
        public IPartnerRepository Partners { get; }
        public IAssociateRepository Associates { get; set; }
        public IUserRepository Users {  get; }
        public INotificationRepository Notifications { get; }
        public IProjectRepository Projects { get; }
        public IProjectMembershipRepository ProjectMemberships { get; }
        public IProjectRequestRepository ProjectRequest { get; }
        public IProjectTaskRepository ProjectTasks { get; set; }
        public UnitOfWork(ApplicationDbContext context, IResearcherRepository researcherRepository, IUserRepository userRepository,
            INotificationRepository NotificationRepository, IProjectRepository projectRepository, IProjectMembershipRepository ProjectMembershipRepository ,
            IProjectRequestRepository ProjectRequestRepository, IPartnerRepository partners, IAssociateRepository associates,IProjectTaskRepository projectTask )
        {
            _context = context;
            Researchers = researcherRepository;
            Users = userRepository;
            Notifications = NotificationRepository;
            Projects = projectRepository;
            ProjectMemberships = ProjectMembershipRepository;
            ProjectRequest = ProjectRequestRepository;
            Partners = partners;
            Associates = associates;
            ProjectTasks = projectTask;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
