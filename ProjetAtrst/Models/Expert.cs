namespace ProjetAtrst.Models
{
    public class Expert : Researcher
    {
      
        public ICollection<ProjectEvaluation> Evaluations { get; set; } = new List<ProjectEvaluation>();
       
    }
}
