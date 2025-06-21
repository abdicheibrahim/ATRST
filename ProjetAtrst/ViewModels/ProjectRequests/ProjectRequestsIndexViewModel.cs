namespace ProjetAtrst.ViewModels.ProjectRequests
{
    public class ProjectRequestsIndexViewModel
    {
        public List<ProjectJoinRequestsGroupViewModel> JoinRequestsToMyProject { get; set; } = new();
        public List<InvitationISentViewModel> InvitationsISent { get; set; } = new();
        public List<JoinRequestISentViewModel> JoinRequestsISent { get; set; } = new();
        public List<InvitationIReceivedViewModel> InvitationsIReceived { get; set; } = new();
    }

}
