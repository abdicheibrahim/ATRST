using ProjetAtrst.Repositories;
using ProjetAtrst.Interfaces.Repositories;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.Models;
namespace ProjetAtrst.Interfaces
{
    public interface IUnitOfWork
    {
        IResearcherRepository Researchers { get; }
        IPartnerRepository Partners { get; }
        IProjectRepository Projects { get; }
        IUserRepository Users { get; }
        INotificationRepository Notifications { get; }
        IProjectMembershipRepository ProjectMemberships { get; }
        IProjectRequestRepository ProjectRequest { get; }
        IAssociateRepository Associates { get; }
        IAdminRepository Admins { get; }
        Task<int> SaveAsync();
    }
}
