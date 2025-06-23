using ProjetAtrst.Models;

namespace ProjetAtrst.Interfaces.Repositories
{
    public interface IProjectRequestRepository
    {
        Task CreateAsync(ProjectRequest request);
        Task<ProjectRequest> GetByIdAsync(int id);
        Task<IEnumerable<ProjectRequest>> GetByReceiverIdAsync(string userId);
        Task<IEnumerable<ProjectRequest>> GetBySenderIdAsync(string senderId);
        Task<IEnumerable<ProjectRequest>> GetByProjectIdAsync(int projectId);
        Task<IEnumerable<ProjectRequest>> GetByUserAndProjectAsync(string userId, int? projectId = null);
        Task SaveChangesAsync();
    }
}
