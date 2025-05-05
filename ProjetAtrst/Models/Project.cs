
namespace ProjetAtrst.Models
{
    public class Project
    {
       
        public string Title { get; set; }= string.Empty;

        public string JoinDate { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; }=string.Empty;

        [Required]
        public bool Status { get; set; } = false;

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public int ExpectId { get; set; }

        [Required]
        public int createdBy { get; set; }
        public int AdminId { get; set; }
        public Admin Admin { get; set; } = default!;
        public ICollection<Researcher> Researchers { get; set; } = new List<Researcher>();
        public ICollection<ProjectExpert> ProjectExperts { get; set; } = new List<ProjectExpert>();

    }
}
