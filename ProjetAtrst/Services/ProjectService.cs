using ProjetAtrst.Interfaces;
using ProjetAtrst.Models;
using ProjetAtrst.ViewModels;
using ProjetAtrst.Interfaces.Services;
namespace ProjetAtrst.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateProjectAsync(string userId, CreateProjectViewModel model)
        {
            var researcher = await _unitOfWork.Researchers.GetByUserIdAsync(userId);
            var leader = await _unitOfWork.ProjectLeader.GetByIdAsync(userId);

            if (researcher == null || !await _unitOfWork.Researchers.CanCreateProjectAsync(researcher.Id))
                return false;

            if (leader == null)
            {
                leader = new ProjectLeader
                {
                    Id = userId,
                    Researcher = researcher
                };
                await _unitOfWork.ProjectLeader.AddAsync(leader);
                //await _unitOfWork.CompleteAsync(); // Save to get LeaderId
            }
            var project = new Project
            {
                Title = model.Title,
                Description = model.Description,
                ProjectStatus=model.ProjectStatus,
                CreationDate = DateTime.Now,
                LeaderId = researcher.Id,
                LastActivity = DateTime.UtcNow
            };

            await _unitOfWork.Projects.AddAsync(project);
            await _unitOfWork.SaveAsync();

            return true;
        }
        public async Task<IEnumerable<Project>> GetOpenProjectsForJoiningAsync(string userId)
        {
            var researcher = await _unitOfWork.Researchers.GetByUserIdAsync(userId);
            if (researcher == null) return Enumerable.Empty<Project>();

            return await _unitOfWork.Projects.GetOpenProjectsForJoiningAsync(researcher.Id);
        }
        public async Task<ProjectLeader> GetLeaderByResearcherIdAsync(string userId)
        {
            return await _unitOfWork.ProjectLeader.GetProjectLeadersByResearcherIdAsync(userId);
        }
    }
}
