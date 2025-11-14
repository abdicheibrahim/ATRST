namespace ProjetAtrst.Interfaces.Repositories
{
    public interface IProjectTaskAssignmentRepository
    {
        Task<ProjectTaskAssignment> GetByIdAsync(int id);
        Task<IEnumerable<ProjectTaskAssignment>> GetByTaskIdAsync(int taskId);
        Task AddAsync(ProjectTaskAssignment entity);
        void Remove(ProjectTaskAssignment entity);
        Task<IEnumerable<ProjectTask>> GetTasksByUserIdAsync(string userId);

    }

}
