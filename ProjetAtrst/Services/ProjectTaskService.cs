using ProjetAtrst.Interfaces;
using ProjetAtrst.Interfaces.Repositories;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.ViewModels.ProjectTask;

namespace ProjetAtrst.Services
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly IProjectTaskRepository _repo;
        private readonly IUnitOfWork _uow;

        public ProjectTaskService(IProjectTaskRepository repo, IUnitOfWork uow)
        {
            _repo = repo;
            _uow = uow;
        }

        public async Task<ProjectTask> CreateAsync(ProjectTaskViewModel dto)
        {
            
            var task = new ProjectTask
            {
                ProjectId = dto.ProjectId,
                TaskName = dto.TaskName,
                Description = dto.Description,
                Status = string.IsNullOrWhiteSpace(dto.Status) ? "Pending" : dto.Status,
                Priority = dto.Priority,
                StartDate = dto.StartDate ?? DateOnly.FromDateTime(DateTime.UtcNow),
                EndDate = dto.EndDate ?? (dto.StartDate.HasValue ? dto.StartDate.Value.AddDays(7) : DateOnly.FromDateTime(DateTime.UtcNow).AddDays(7)),
                Progress = dto.Progress
            };

            await _repo.AddAsync(task);
            await _uow.SaveAsync();

            return task;
        }
    }

}
