namespace ProjetAtrst.Models
{
    public class Expert : Person
    {
        public int AdminId { get; set; }
        public Admin Admin { get; set; } = default!;
        public ICollection<ProjectExpert> ProjectExperts { get; set; } = new List<ProjectExpert>();
    }
}
