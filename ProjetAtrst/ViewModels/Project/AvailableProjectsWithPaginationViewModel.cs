using ProjetAtrst.Models;

namespace ProjetAtrst.ViewModels.Project
{
    public class AvailableProjectsWithPaginationViewModel
    {
        public List<AvailableProjectViewModel> Projects { get; set; }
        public PaginationModel Pagination { get; set; }
    }
}
