using ProjetAtrst.Interfaces;
using ProjetAtrst.Interfaces.Repositories;
using ProjetAtrst.Interfaces.Services;

namespace ProjetAtrst.Services
{
    public class ProjectTaskAssignmentService : IProjectTaskAssignmentService
    {
        private readonly IProjectTaskAssignmentRepository _assignmentRepo;
        private readonly IUnitOfWork _uow;

        public ProjectTaskAssignmentService(IProjectTaskAssignmentRepository repo, IUnitOfWork uow)
        {
            _assignmentRepo = repo;
            _uow = uow;
        }

        public async Task<ProjectTaskAssignment> AssignUserAsync(int taskId, string userId, string role)
        {
            var assignment = new ProjectTaskAssignment
            {
                TaskId = taskId,
                AssignedUserId = userId,
                Role = role
            };

            await _assignmentRepo.AddAsync(assignment);
            await _uow.SaveAsync();
            return assignment;
        }

        public async Task<IEnumerable<ProjectTaskAssignment>> GetAssignmentsForTaskAsync(int taskId) =>
            await _assignmentRepo.GetByTaskIdAsync(taskId);

        public async Task RemoveAssignmentAsync(int assignmentId)
        {
            var assignment = await _assignmentRepo.GetByIdAsync(assignmentId);
            if (assignment != null)
            {
                _assignmentRepo.Remove(assignment);
                await _uow.SaveAsync();
            }
        }
        public async Task<IEnumerable<ProjectTask>> GetUserTasksAsync(string userId)
        {
            return await _assignmentRepo.GetTasksByUserIdAsync(userId);
        }
    }

}
