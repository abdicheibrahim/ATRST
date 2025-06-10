using ProjetAtrst.Repositories;
using ProjetAtrst.Interfaces.Repositories;
using ProjetAtrst.Interfaces.Services;
namespace ProjetAtrst.Interfaces
{
    public interface IUnitOfWork
    {
        IResearcherRepository Researchers { get; }
        
        IUserRepository Users { get; }
        INotificationRepository Notifications { get; }
        IProjectRepository Projects { get; }
        IJoinRequestRepository JoinRequests { get; }
        IProjectLeaderRepository ProjectLeader { get; }
        IInvitationRequestRepository InvitationRequest { get; }
        IProjectMembershipRepository ProjectMembership { get; }
        // أضف المزيد من الـ Repositories هنا مثل:
        // IProjectRepository Projects { get; }
        // ILeaderRepository Leaders { get; }
        // IMemberRepository Members { get; }
        //IUserRepository Users { get; }
        Task<int> SaveAsync();
    }
}
