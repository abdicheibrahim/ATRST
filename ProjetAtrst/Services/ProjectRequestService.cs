using ProjetAtrst.Interfaces;
using ProjetAtrst.Interfaces.Repositories;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.Models;
using ProjetAtrst.Repositories;
using ProjetAtrst.ViewModels.ProjectRequests;

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
            SenderId = senderId,
            ReceiverId = model.ReceiverId,
            Message = model.Message,
            Type = model.Type,
            Status = RequestStatus.Pending
        };

        await _requestRepository.CreateAsync(request);
        await _requestRepository.SaveChangesAsync();

        // إرسال إشعار للمستقبل
        string senderName = (await _userRepository.GetByIdAsync(senderId))?.FullName ?? "مستخدم";
        string projectTitle = (await _projectRepository.GetByIdAsync(model.ProjectId))?.Title ?? "مشروع";

        string notifTitle = model.Type == RequestType.Join ? "طلب انضمام جديد" : "دعوة جديدة";
        string notifMessage = model.Type == RequestType.Join
            ? $"{senderName} طلب الانضمام إلى مشروعك: {projectTitle}"
            : $"{senderName} دعاك للانضمام إلى مشروع: {projectTitle}";

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

        // أضف العضو رسميًا إلى المشروع
        var membership = new ProjectMembership
        {
            ProjectId = request.ProjectId,
            ResearcherId = request.SenderId,
            Role = Role.Member, // أو أي Enum معرف عندك
            JoinedAt = DateTime.UtcNow
        };
        await _projectMembershipRepository.AddAsync(membership);
        await _unitOfWork.SaveAsync();

        // إرسال إشعار
        await _notificationService.CreateNotificationAsync(
            request.SenderId,
            "تم قبول الطلب",
            $"تم قبول طلبك المرتبط بمشروع: {request.Project?.Title}",
            NotificationType.General,
            request.Id
        );
    }


    public async Task RejectRequestAsync(int requestId)
    {
        var request = await _requestRepository.GetByIdAsync(requestId);
        if (request == null || request.Status != RequestStatus.Pending)
            return;

        request.Status = RequestStatus.Rejected;
        await _requestRepository.SaveChangesAsync();

        await _notificationService.CreateNotificationAsync(
            request.SenderId,
            "تم رفض الطلب",
            $"تم رفض طلبك المرتبط بمشروع: {request.Project?.Title}",
            NotificationType.General,
            request.Id
        );
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
        return await _requestRepository.GetByUserAndProjectAsync(userId, 0); // get all by sender, ignoring project filter
    }
}
