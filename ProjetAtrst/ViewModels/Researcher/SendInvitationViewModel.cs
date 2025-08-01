using ProjetAtrst.Models;

namespace ProjetAtrst.ViewModels.Researcher
{
    public class SendInvitationViewModel
    {
        public List<ResearcherViewModel> Researchers { get; set; }
        public PaginationModel Pagination { get; set; }
        public int CurrentProjectId { get; set; }
    }
}
