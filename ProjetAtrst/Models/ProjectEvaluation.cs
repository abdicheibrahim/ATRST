
namespace ProjetAtrst.Models
{
    public class ProjectEvaluation
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }= default!;
        public string ExpertId { get; set; } = default!;
        public Expert Expert { get; set; } = default!;
        public string? Comment { get; set; } 
        public bool IsAccepted { get; set; }
        public DateTime EvaluatedOn { get; set; }
    }
}
