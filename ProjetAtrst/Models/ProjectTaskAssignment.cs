namespace ProjetAtrst.Models
{
    public class ProjectTaskAssignment
    {
        [Key]
        public int ProjectTaskAssignmentId { get; set; }
        public int TaskId { get; set; }
        public string AssignedUserId { get; set; }
        public string Role { get; set; } // Responsable, Participant…

        public virtual ProjectTask Task { get; set; }
        public virtual ApplicationUser AssignedUser { get; set; }
    }
}
