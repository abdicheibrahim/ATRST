using ProjetAtrst.Interfaces.Repositories;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.Interfaces;
using ProjetAtrst.Models;
using ProjetAtrst.ViewModels.Researcher;

namespace ProjetAtrst.Services
{
    public class InvitationRequestService : IInvitationRequestService
    {
        //private readonly IInvitationRequestRepository _invitationRepo;
        //private readonly IProjectMembershipRepository _membershipRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserAccessService _userAccessService;

        public InvitationRequestService(
            //IInvitationRequestRepository invitationRepo,
            //IProjectMembershipRepository membershipRepo,
            IUnitOfWork unitOfWork, IUserAccessService userAccessService)
        {
            //_invitationRepo = invitationRepo;
            //_membershipRepo = membershipRepo;
            _unitOfWork = unitOfWork;
            _userAccessService = userAccessService;
        }

        public async Task<List<InvitationRequest>> GetReceivedInvitationsAsync(string memberId)
            => await _unitOfWork.InvitationRequest.GetPendingReceivedAsync(memberId);

        public async Task<List<InvitationRequest>> GetSentInvitationsAsync(string leaderId)
            => await _unitOfWork.InvitationRequest.GetPendingSentAsync(leaderId);

        public async Task<InvitationRequest?> GetByIdAsync(int id)
            => await _unitOfWork.InvitationRequest.GetByIdWithDetailsAsync(id);

        public async Task SendInvitationAsync(string senderId, string receiverId, int projectId)
        {
            var invitation = new InvitationRequest
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                TargetProjectId = projectId,
                SentAt = DateTime.UtcNow,
                Status = InvitationRequestStatus.Pending
            };
            await _unitOfWork.InvitationRequest.AddAsync(invitation);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> AcceptAsync(int id)
        {
            var invitation = await _unitOfWork.InvitationRequest.GetByIdWithDetailsAsync(id);
            if (invitation is null || invitation.Status != InvitationRequestStatus.Pending) return false;

            invitation.Status = InvitationRequestStatus.Accepted;
            var membership = new ProjectMembership
            {
                ProjectId = invitation.TargetProjectId,
                MemberId = invitation.ReceiverId,
                JoinedAt = DateTime.UtcNow
            };

            await _unitOfWork.ProjectMembership.AddAsync(membership);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> RejectAsync(int id)
        {
            var invitation = await _unitOfWork.InvitationRequest.GetByIdWithDetailsAsync(id);
            if (invitation is null || invitation.Status != InvitationRequestStatus.Pending) return false;

            invitation.Status = InvitationRequestStatus.Rejected;
            await _unitOfWork.SaveAsync();
            return true;
        }
        public async Task<List<ResearcherViewModel>> GetAllEligibleForInvitationAsync(int projectId)
        {
            var eligibleMembers = await _unitOfWork.ProjectMembership.GetEligibleForInvitationAsync(projectId);

            return eligibleMembers.Select(m => new ResearcherViewModel
            {
                MemberId = m.Id,
                FirstName = m.Researcher.User.FirstName,
                LastName = m.Researcher.User.LastName,
                Grade = m.Researcher.Grade,
                Speciality = m.Researcher.Speciality
            }).ToList();
        }
        public async Task SendInvitationAsync(string receiverId, int projectId)
        {
            var senderId = _userAccessService.GetUserId();

            var invitation = new InvitationRequest
            {
                ReceiverId = receiverId,
                TargetProjectId = projectId,
                SenderId = senderId!,
                Status = InvitationRequestStatus.Pending,
                SentAt = DateTime.UtcNow
            };

            await _unitOfWork.InvitationRequest.AddAsync(invitation);
            await _unitOfWork.SaveAsync();
        }


    }
}
