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
        public IProjectMembershipRepository ProjectMemberships { get; }
        public UnitOfWork(ApplicationDbContext context, IResearcherRepository researcherRepository, IUserRepository userRepository,
            INotificationRepository NotificationRepository, IProjectRepository projectRepository, IProjectMembershipRepository IProjectMembershipRepository)
        {
            _context = context;
            Researchers = researcherRepository;
            Users = userRepository;
            Notifications = NotificationRepository;
            Projects = projectRepository;
            ProjectMemberships = IProjectMembershipRepository;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
