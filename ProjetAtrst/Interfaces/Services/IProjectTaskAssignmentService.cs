namespace ProjetAtrst.Interfaces.Services
{
    public interface IProjectTaskAssignmentService
    {
        Task<ProjectTaskAssignment> AssignUserAsync(int taskId, string userId, string role);
        Task<IEnumerable<ProjectTaskAssignment>> GetAssignmentsForTaskAsync(int taskId);
        Task RemoveAssignmentAsync(int assignmentId);
        Task<IEnumerable<ProjectTask>> GetUserTasksAsync(string userId);

    }

}

