namespace ProjetAtrst.ViewModels.UserAccess
{
    public class UserAccessStatusViewModel
    {
        public  bool IsCompleted { get; set; }
        public  bool IsApproved { get; set; }
        public bool IsLeader => ProjectLeaderId != null;
        public bool IsMember => ProjectMemberId != null;

        public string? ProjectLeaderId { get; set; }
        public string? ProjectMemberId { get; set; }
    }
}
