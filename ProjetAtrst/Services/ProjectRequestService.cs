//using ProjetAtrst.Interfaces.Repositories;
//using ProjetAtrst.ViewModels.ProjectRequests;
//using ProjetAtrst.Interfaces.Services;
//using Microsoft.EntityFrameworkCore;
//using ProjetAtrst.Models;
//namespace ProjetAtrst.Services
//{
//    public class ProjectRequestService : IProjectRequestService
//    {
//        private readonly IInvitationRequestRepository _invitationRepository;
//        private readonly IProjectMembershipRepository _projectMembershipRepository;
//        private readonly IJoinRequestRepository _joinRequestRepository;
//        private readonly IResearcherRepository _researcherRepository;

//        public ProjectRequestService(
//            IJoinRequestRepository joinRequestRepository,
//            IResearcherRepository researcherRepository,
//            IInvitationRequestRepository _invitationRepository,
//            IProjectMembershipRepository projectMembershipRepo
//            )
//        {
//            this._joinRequestRepository = joinRequestRepository;
//            this._researcherRepository = researcherRepository;
//            this._invitationRepository = _invitationRepository;
//            this._projectMembershipRepository = projectMembershipRepo;
//        }

//        //public async Task<List<ProjectJoinRequestsGroupViewModel>> GetJoinRequestsGroupedByProjectAsync(string userId)
//        //{
//        //    var researcher = await _researcherRepository.GetByIdAsync(userId);
//        //    if (researcher == null)
//        //        return new List<ProjectJoinRequestsGroupViewModel>();

//        //    var joinRequests = await _joinRequestRepository.GetJoinRequestsToMyProjectsAsync(researcher.Id);

//        //    var grouped = joinRequests
//        //        .GroupBy(j => j.Project)
//        //        .Select(g => new ProjectJoinRequestsGroupViewModel
//        //        {
//        //            ProjectId = g.Key.Id,
//        //            ProjectTitle = g.Key.Title,
//        //            JoinRequests = g.Select(r => new JoinRequestItemViewModel
//        //            {
//        //                RequestId = r.Id,
//        //                RequesterId = r.RequesterId,
//        //                RequesterFullName = $"{r.Requester.User.FirstName} {r.Requester.User.LastName}",
//        //                RequestedAt = r.RequestedAt,
//        //                Status = r.Status
//        //            }).OrderByDescending(r => r.RequestedAt).ToList()
//        //        })
//        //        .OrderBy(p => p.ProjectTitle)
//        //        .ToList();

//        //    return grouped;
//        //}

//        //public async Task<List<InvitationISentViewModel>> GetInvitationsISentAsync(string researcherId)
//        //{
//        //    var invitations = await _invitationRepository.GetInvitationsISentAsync(researcherId);

//        //    return invitations.Select(i => new InvitationISentViewModel
//        //    {
//        //        InvitationId = i.Id,
//        //        ProjectId = i.TargetProjectId,
//        //        ProjectTitle = i.TargetProject.Title,
//        //        ReceiverId = i.ReceiverId,
//        //        ReceiverFullName = i.Receiver.User.FullName,
//        //        SentAt = i.SentAt,
//        //        Status = i.Status
//        //    }).ToList();
//        //}

//        //public async Task<List<JoinRequestISentViewModel>> GetJoinRequestsISentAsync(string researcherId)
//        //{
//        //    var requests = await _joinRequestRepository.GetJoinRequestsISentAsync(researcherId);

//        //    return requests.Select(j => new JoinRequestISentViewModel
//        //    {
//        //        RequestId = j.Id,
//        //        ProjectId = j.ProjectId,
//        //        ProjectTitle = j.Project.Title,
//        //        RequestedAt = j.RequestedAt,
//        //        Status = j.Status
//        //    }).ToList();
//        //}

//        //public async Task<List<InvitationIReceivedViewModel>> GetInvitationsIReceivedAsync(string researcherId)
//        //{
//        //    var invitations = await _invitationRepository.GetInvitationsIReceivedAsync(researcherId);

//        //    return invitations.Select(i => new InvitationIReceivedViewModel
//        //    {
//        //        InvitationId = i.Id,
//        //        ProjectId = i.TargetProjectId,
//        //        ProjectTitle = i.TargetProject.Title,
//        //        SenderLeaderId = i.TargetProject.ProjectMemberships
//        //            .FirstOrDefault(pm => pm.Role == Role.Leader)?.ResearcherId ?? string.Empty,
//        //        SenderLeaderFullName = i.TargetProject.ProjectMemberships
//        //            .Where(pm => pm.Role == Role.Leader)
//        //            .Select(pm => pm.Researcher.User.FullName)
//        //            .FirstOrDefault() ?? "غير معروف",
//        //        SentAt = i.SentAt,
//        //        Status = i.Status
//        //    }).ToList();
//        //}

//        //public async Task<List<ProjectJoinRequestsGroupViewModel>> GetJoinRequestsToMyProjectsGroupedAsync(string leaderId)
//        //{
//        //    var groupedRequests = await _joinRequestRepository.GetGroupedJoinRequestsToMyProjectsAsync(leaderId);

//        //    return groupedRequests.Select(g => new ProjectJoinRequestsGroupViewModel
//        //    {
//        //        ProjectId = g.Key,
//        //        ProjectTitle = g.First().Project.Title,
//        //        JoinRequests = g.Select(j => new JoinRequestItemViewModel
//        //        {
//        //            RequestId = j.Id,
//        //            RequesterId = j.RequesterId,
//        //            RequesterFullName = j.Requester.User.FullName,
//        //            RequestedAt = j.RequestedAt,
//        //            Status = j.Status
//        //        }).ToList()
//        //    }).ToList();
//        //}

//        public async Task SendJoinRequestAsync(int projectId, string researcherId)
//        {
//            // تحقق أولًا إذا سبق وأرسل طلب لنفس المشروع
//            var existingRequest = await _joinRequestRepository.GetByProjectAndResearcherAsync(projectId, researcherId);
//            if (existingRequest != null)
//                throw new InvalidOperationException("لقد سبق وأرسلت طلبًا لهذا المشروع.");

//            // أنشئ الطلب الجديد
//            var joinRequest = new JoinRequest
//            {
//                ProjectId = projectId,
//                ResearcherId = researcherId,
//                CreatedAt = DateTime.UtcNow,
//                Status = JoinRequestStatus.Pending
//            };

//            await _joinRequestRepository.AddAsync(joinRequest);
//        }

//    }

//}
