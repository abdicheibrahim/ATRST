using ProjetAtrst.Repositories;
using ProjetAtrst.Interfaces.Repositories;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.Models;
namespace ProjetAtrst.Interfaces
{
    public interface IUnitOfWork
    {
        IResearcherRepository Researchers { get; }
        IProjectRepository Projects { get; }
        IUserRepository Users { get; }
        INotificationRepository Notifications { get; }
        IProjectMembershipRepository ProjectMemberships { get; }
            Task<int> SaveAsync();
    }
}
