using Microsoft.EntityFrameworkCore;
using ProjetAtrst.Interfaces;
using ProjetAtrst.Interfaces.Repositories;
using ProjetAtrst.Interfaces.Services;
using ProjetAtrst.ViewModels.ProjectTask;

namespace ProjetAtrst.Services
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProjectTaskRepository _taskRepository;

        public ProjectTaskService(IUnitOfWork unitOfWork, IProjectTaskRepository taskRepository)
        {
            _unitOfWork = unitOfWork;
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<ProjectTask>> GetTasksByProjectIdAsync(int projectId)
        {
            return await _taskRepository.GetByProjectIdAsync(projectId);
        }

        public async Task<ProjectTask?> GetTaskByIdAsync(int Id)
        {
            return await _taskRepository.GetByIdAsync(Id);
        }

        public async Task<bool> CreateAsync(ProjectTaskViewModel taskViewModel)
        {
            try
            {
                var task = new ProjectTask
                {
                    TaskName = taskViewModel.TaskName,
                    Description = taskViewModel.Description,
                    Status = taskViewModel.Status,
                    Priority = taskViewModel.Priority,
                    Progress = taskViewModel.Progress,
                    StartDate = taskViewModel.StartDate,
                    EndDate = taskViewModel.EndDate,
                    ProjectId = taskViewModel.ProjectId
                };

                await _taskRepository.AddAsync(task);
                await _unitOfWork.SaveAsync(); 
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(int taskId, ProjectTaskViewModel taskViewModel)
        {
            try
            {
                var Task = await _taskRepository.GetByIdAsync(taskId);
                if (Task == null)
                    return false;

                Task.TaskName = taskViewModel.TaskName;
                Task.Description = taskViewModel.Description;
                Task.Status = taskViewModel.Status;
                Task.Priority = taskViewModel.Priority;
                Task.Progress = taskViewModel.Progress;
                Task.StartDate = taskViewModel.StartDate;
                Task.EndDate = taskViewModel.EndDate;

                _taskRepository.Update(Task);
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int taskId)
        {
            try
            {
                var task = await _taskRepository.GetByIdAsync(taskId);
                if (task == null)
                    return false;

                _taskRepository.Delete(task);
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
