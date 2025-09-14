using ProjetAtrst.Enums;
using ProjetAtrst.Interfaces;
using ProjetAtrst.Interfaces.Repositories;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.Models;
using ProjetAtrst.Repositories;
using ProjetAtrst.ViewModels.ProjectRequests;
using System.Data;
using System.Text;

public class ProjectRequestService : IProjectRequestService
{
    private readonly IProjectRequestRepository _requestRepository;
    private readonly INotificationService _notificationService;
    private readonly IUserRepository _userRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectMembershipRepository _projectMembershipRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProjectRequestService(
        IProjectRequestRepository requestRepository,
        INotificationService notificationService,
        IUserRepository userRepository,
        IProjectRepository projectRepository, IProjectMembershipRepository projectMembershipRepository, IUnitOfWork unitOfWork)
    {
        _requestRepository = requestRepository;
        _notificationService = notificationService;
        _userRepository = userRepository;
        _projectRepository = projectRepository;
        _projectMembershipRepository = projectMembershipRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task SendRequestAsync(ProjectRequestCreateViewModel model, string senderId)
    {
        var request = new ProjectRequest
        {
            ProjectId = model.ProjectId,
            ReceiverId = model.ReceiverId,
            SenderId = senderId,
            Message = model.Message,
            Type = model.Type,
            Status = RequestStatus.Pending,
            CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow)
        };

        await _unitOfWork.ProjectRequest.CreateAsync(request);
        await _unitOfWork.SaveAsync();

        
      
        string senderName = (await _userRepository.GetByIdAsync(senderId))?.FullName ?? "Utilisateur";
        string projectTitle = (await _projectRepository.GetByIdAsync(model.ProjectId))?.Title ?? "Projet";



      
        string notifTitle = model.Type == RequestType.Join
            ? "Nouvelle demande d'adhésion"
            : "Nouvelle invitation";

        string notifMessage = model.Type == RequestType.Join
            ? $"{senderName} a demandé à rejoindre votre projet : {projectTitle}"
            : $"{senderName} vous a invité à rejoindre le projet : {projectTitle}";

        await _notificationService.CreateNotificationAsync(
            model.ReceiverId,
            notifTitle,
            notifMessage,
            NotificationType.General,
            request.Id
        );
    }

    public async Task AcceptRequestAsync(int requestId)
    {
        var request = await _requestRepository.GetByIdAsync(requestId);
        if (request == null || request.Status != RequestStatus.Pending)
            return;

        request.Status = RequestStatus.Accepted;
        await _requestRepository.SaveChangesAsync();
       
        //add to Project membership
        var membership = new ProjectMembership
        {
            ProjectId = request.ProjectId,
            UserId = request.SenderId,
            Role =  request.Sender.RoleType switch
            {
                RoleType.Researcher => Role.Member,
                RoleType.Partner => Role.Partner,
                RoleType.Associate => Role.Associate,
                _ => Role.Member
            },
            JoinedAt = DateOnly.FromDateTime(DateTime.UtcNow)
        };


        await _projectMembershipRepository.AddAsync(membership);
        await _unitOfWork.SaveAsync();

        // Send notification
        await _notificationService.CreateNotificationAsync(
            request.SenderId,
            "Demande acceptée",
            $"Votre demande concernant le projet a été acceptée : {request.Project?.Title}",
            NotificationType.General,
            request.Id
        );
    }


    public async Task RejectRequestAsync(int requestId, RejectionType rejectionType)
    {
        var request = await _requestRepository.GetByIdAsync(requestId);
        if (request == null)
            throw new KeyNotFoundException($"Request with ID {requestId} not found.");

        if (request.Status != RequestStatus.Pending)
            throw new InvalidOperationException($"Request is already {request.Status}.");

        request.Status = RequestStatus.Rejected;
        await _requestRepository.SaveChangesAsync();

        var (title, message) = GetRejectionNotificationContent(request, rejectionType);

        var notificationType = rejectionType switch
        {
            RejectionType.JoinRequest => NotificationType.JoinRequestRejected,
            RejectionType.Invitation => NotificationType.InvitationRejected,
            _ => NotificationType.General
        };

        
            await _notificationService.CreateNotificationAsync(
                request.SenderId,
                title,
                message,
                notificationType,
                request.Id
            );
        
        
    }


    private (string Title, string Message) GetRejectionNotificationContent(ProjectRequest request, RejectionType type)
    {
        return type switch
        {
            RejectionType.JoinRequest => (
                "Rejeté",
                $"Votre demande pour rejoindre le projet {request.Project?.Title} a été rejetée."
            ),
            RejectionType.Invitation => (
                "Invitation refusée",
               $" {request.Receiver.FullName} a refusé de rejoindre le projet {request.Project?.Title}."
            ),
            _ => ("", "")
        };
    }


    public async Task<(IEnumerable<ProjectRequest> Incoming, IEnumerable<ProjectRequest> Sent)> GetRequestsForDashboardAsync(string userId)
    {
        var incoming = await _requestRepository.GetByReceiverIdAsync(userId);
        var sent = await _requestRepository.GetBySenderIdAsync(userId);
        // return (incoming, sent.Where(r => r.Status == RequestStatus.Pending)); // فقط الطلبات المعلقة
        return (incoming, sent); // أعد الكل بدون تصفية
    }

    public async Task<IEnumerable<ProjectRequest>> GetOutgoingRequestsAsync(string userId)
    {
        return await _requestRepository.GetBySenderIdAsync(userId); 
    }
    public async Task<ProjectRequest> GetByIdWithRelationsAsync(int userId)
    {
        return await _requestRepository.GetByIdWithRelationsAsync(userId); // get all by sender, ignoring project filter
    }

  
    public async Task<ProjectRequestCreateViewModel> PrepareRequestModelAsync(int projectId, string receiverId, RequestType type)
    {
        var (title, leaderName) = await _projectRepository.GetProjectInfoAsync(projectId);

        if (title == null)
            return null;

        string receiverName = null;
        if (type == RequestType.Invitation)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(receiverId);
            receiverName = user != null ? user.FullName  : "Anonymous";
        }

        return new ProjectRequestCreateViewModel
        {
            ProjectId = projectId,
            ReceiverId = receiverId,
            Type = type,
            ProjectTitle = title,
            LeaderFullName = type == RequestType.Join ? leaderName : receiverName
        };
    }


    public async Task<List<ProjectRequest>> GetSentJoinRequestsAsync(string researcherId)
    {
        return await _requestRepository.GetJoinRequestsBySenderAsync(researcherId);
    }
  

    
    public Task<List<ProjectRequest>> GetMyInvitationsAsync(string userId)
    {
        return _unitOfWork.ProjectRequest.GetInvitationsForUserAsync(userId);
    }

    public async Task SendInvitationAsync(int projectId, string researcherId)
   {
        var invitation = new ProjectRequest
        {
            ProjectId = projectId,
            ReceiverId = researcherId,
            Status = RequestStatus.Pending,
            Type = RequestType.Invitation,
            CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow)
        };

        await _unitOfWork.ProjectRequest.CreateAsync (invitation);
    }

 
}
