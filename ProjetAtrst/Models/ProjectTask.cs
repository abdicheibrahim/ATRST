namespace ProjetAtrst.Models
{
    public class ProjectTask
    {
        [Key]
        public int TaskId { get; set; }
        public int ProjectId { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }   
        public string Status { get; set; }
        public int Priority { get; set; }         
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int Progress { get; set; }         

        public virtual Project Project { get; set; }
        public virtual ICollection<ProjectTaskAssignment> Assignments { get; set; }
    }
}
