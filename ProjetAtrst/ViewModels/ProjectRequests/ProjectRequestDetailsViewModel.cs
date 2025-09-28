namespace ProjetAtrst.ViewModels.ProjectRequests
{
    public class ProjectRequestDetailsViewModel
    {
        public string ProjectTitle { get; set; }
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public string Message { get; set; }
        public string? RelatedWorks { get; set; }
        public RequestType Type { get; set; } 
        public RequestStatus Status { get; set; } = RequestStatus.Pending;
        public DateOnly CreatedAt { get; set; }

    }
}
