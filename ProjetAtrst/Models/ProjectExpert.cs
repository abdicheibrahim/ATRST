using System.Security.Principal;

namespace ProjetAtrst.Models
{
    public class ProjectExpert
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }= default!;
        public int ExpertId { get; set; }
        public Expert Expert { get; set; } = default!;
    }
}
